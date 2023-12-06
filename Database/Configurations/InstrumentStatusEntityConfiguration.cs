using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class InstrumentStatusEntityConfiguration : IEntityTypeConfiguration<InstrumentStatusEntity>
    {
        public void Configure(EntityTypeBuilder<InstrumentStatusEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.PackageID);
            builder.HasMany(e => e.DeviceStatusList).WithOne().HasForeignKey("InstrumentStatusId");
        }
    }
}