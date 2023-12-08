using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Structures;

namespace Database;

public class Repository(DatabaseContext context, IMapper mapper)
{
    public async Task SaveOrUpdateInstrumentStatusAsync(InstrumentStatus instrumentStatus)
    {
        var instrumentStatusEntity = mapper.Map<InstrumentStatusEntity>(instrumentStatus);
        var existingInstrumentStatus = await context.InstrumentStatuses
            .Include(statusEntity => statusEntity.DeviceStatusList)
            .FirstOrDefaultAsync(statusEntity => statusEntity.PackageID == instrumentStatus.PackageID);
        if (existingInstrumentStatus != null) mapper.Map(instrumentStatus, existingInstrumentStatus);
        else context.InstrumentStatuses.Add(instrumentStatusEntity);
        await context.SaveChangesAsync();
    }
}