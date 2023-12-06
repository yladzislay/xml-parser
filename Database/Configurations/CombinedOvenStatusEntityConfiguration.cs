using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedOvenStatusEntityConfiguration : IEntityTypeConfiguration<CombinedOvenStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedOvenStatusEntity> builder)
        {
            builder.Property(e => e.UseTemperatureControl);
            builder.Property(e => e.OvenOn);
            builder.Property(e => e.Temperature_Actual);
            builder.Property(e => e.Temperature_Room);
            builder.Property(e => e.MaximumTemperatureLimit);
            builder.Property(e => e.Valve_Position);
            builder.Property(e => e.Valve_Rotations);
            builder.Property(e => e.Buzzer);
        }
    }
}