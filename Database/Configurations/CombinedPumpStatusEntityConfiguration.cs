using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedPumpStatusEntityConfiguration : IEntityTypeConfiguration<CombinedPumpStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedPumpStatusEntity> builder)
        {
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.Mode);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.Flow);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.PercentB);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.PercentC);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.PercentD);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.MinimumPressureLimit);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.MaximumPressureLimit);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.Pressure);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.PumpOn);
            builder.Property(combinedPumpStatusEntity => combinedPumpStatusEntity.Channel);
            
            builder.HasBaseType<CombinedStatusEntity>();
        }
    }
}