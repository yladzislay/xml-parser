using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedOvenStatusEntityConfiguration : IEntityTypeConfiguration<CombinedOvenStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedOvenStatusEntity> builder)
        {
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.UseTemperatureControl);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.OvenOn);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.Temperature_Actual);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.Temperature_Room);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.MaximumTemperatureLimit);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.Valve_Position);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.Valve_Rotations);
            builder.Property(combinedOvenStatusEntity => combinedOvenStatusEntity.Buzzer);
            
            builder.HasBaseType<CombinedStatusEntity>();
        }
    }
}