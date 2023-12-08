using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class DeviceStatusEntityConfiguration : IEntityTypeConfiguration<DeviceStatusEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceStatusEntity> builder)
        {
            builder.HasKey(deviceStatusEntity => deviceStatusEntity.ModuleCategoryID);
            builder.Property(deviceStatusEntity => deviceStatusEntity.IndexWithinRole);
            builder.HasOne(deviceStatusEntity => deviceStatusEntity.RapidControlStatus)
                .WithOne()
                .HasForeignKey<DeviceStatusEntity>("RapidControlStatusId");
        }
    }
}