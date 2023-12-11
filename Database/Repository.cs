using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Structures;

namespace Database;

public class Repository(DatabaseContext context, IMapper mapper)
{
    public async Task SaveOrUpdateInstrumentStatusAsync(InstrumentStatus instrumentStatus)
    {
        var existingInstrumentStatus = await context.InstrumentStatuses
            .Include(statusEntity => statusEntity.DeviceStatuses)
            .ThenInclude(deviceStatusEntity => deviceStatusEntity.RapidControlStatus)
            .ThenInclude(rapidControlStatusEntity => rapidControlStatusEntity.CombinedStatus)
            .FirstOrDefaultAsync(statusEntity => statusEntity.PackageID == instrumentStatus.PackageID);

        if (existingInstrumentStatus != null)
        {
            foreach (var incomingDeviceStatus in instrumentStatus.DeviceStatuses)
            {
                var existedDeviceStatus = existingInstrumentStatus.DeviceStatuses
                    .FirstOrDefault(deviceStatus => deviceStatus.ModuleCategoryID == incomingDeviceStatus.ModuleCategoryID);

                if (existedDeviceStatus != null)
                {
                    var mappedCombinedStatus = mapper.Map(
                        incomingDeviceStatus.RapidControlStatus.CombinedStatus, 
                        existedDeviceStatus.RapidControlStatus.CombinedStatus);
                    var mappedRapidControlStatus = mapper.Map(
                        incomingDeviceStatus.RapidControlStatus, 
                        existedDeviceStatus.RapidControlStatus);
                    existedDeviceStatus = mapper.Map(incomingDeviceStatus, existedDeviceStatus);
                    existedDeviceStatus.RapidControlStatus = mappedRapidControlStatus;
                    existedDeviceStatus.RapidControlStatus.CombinedStatus = mappedCombinedStatus;
                }
                else existingInstrumentStatus.DeviceStatuses.Add(mapper.Map<DeviceStatusEntity>(incomingDeviceStatus));
            }
        }
        else context.InstrumentStatuses.Add(mapper.Map<InstrumentStatusEntity>(instrumentStatus));
        await context.SaveChangesAsync();
    }
}