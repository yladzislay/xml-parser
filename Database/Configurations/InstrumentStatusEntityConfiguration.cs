using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class InstrumentStatusEntityConfiguration : IEntityTypeConfiguration<InstrumentStatusEntity>
{
    public void Configure(EntityTypeBuilder<InstrumentStatusEntity> builder)
    {
        builder.HasKey(instrumentStatusEntity => instrumentStatusEntity.PackageID);
        builder.HasMany(instrumentStatusEntity => instrumentStatusEntity.DeviceStatuses);
    }
}