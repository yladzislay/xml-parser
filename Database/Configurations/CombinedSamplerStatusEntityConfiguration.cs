using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedSamplerStatusEntityConfiguration : IEntityTypeConfiguration<CombinedSamplerStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedSamplerStatusEntity> builder)
        {
            builder.Property(e => e.Status);
            builder.Property(e => e.Vial);
            builder.Property(e => e.Volume);
            builder.Property(e => e.MaximumInjectionVolume);
            builder.Property(e => e.RackL);
            builder.Property(e => e.RackR);
            builder.Property(e => e.RackInf);
            builder.Property(e => e.Buzzer);
        }
    }
}