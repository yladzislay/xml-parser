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
        var existingRecord = await context.InstrumentStatuses
            .FirstOrDefaultAsync(x => x.PackageID == instrumentStatus.PackageID);
        if (existingRecord != null) mapper.Map(instrumentStatus, existingRecord);
        else context.InstrumentStatuses.Add(instrumentStatusEntity);
        await context.SaveChangesAsync();
    }
}