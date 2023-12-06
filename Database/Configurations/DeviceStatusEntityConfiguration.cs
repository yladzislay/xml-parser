using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class DeviceStatusEntityConfiguration : IEntityTypeConfiguration<DeviceStatusEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceStatusEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ModuleCategoryID);
            builder.Property(e => e.IndexWithinRole);
            builder.HasOne(e => e.RapidControlStatus).WithOne().HasForeignKey<DeviceStatusEntity>("RapidControlStatusId");
        }
    }
}