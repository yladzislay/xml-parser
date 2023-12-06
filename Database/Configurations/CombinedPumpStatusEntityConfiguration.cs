using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedPumpStatusEntityConfiguration : IEntityTypeConfiguration<CombinedPumpStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedPumpStatusEntity> builder)
        {
            builder.Property(e => e.Mode);
            builder.Property(e => e.Flow);
            builder.Property(e => e.PercentB);
            builder.Property(e => e.PercentC);
            builder.Property(e => e.PercentD);
            builder.Property(e => e.MinimumPressureLimit);
            builder.Property(e => e.MaximumPressureLimit);
            builder.Property(e => e.Pressure);
            builder.Property(e => e.PumpOn);
            builder.Property(e => e.Channel);
        }
    }
}