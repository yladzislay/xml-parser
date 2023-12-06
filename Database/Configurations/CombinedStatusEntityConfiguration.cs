using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CombinedStatusEntityConfiguration : IEntityTypeConfiguration<CombinedStatusEntity>
    {
        public void Configure(EntityTypeBuilder<CombinedStatusEntity> builder)
        {
            builder.ToTable("CombinedStatuses");

            builder.HasDiscriminator<string>("ModuleState")
                .HasValue<CombinedSamplerStatusEntity>("CombinedSamplerStatus")
                .HasValue<CombinedPumpStatusEntity>("CombinedPumpStatus")
                .HasValue<CombinedOvenStatusEntity>("CombinedOvenStatus");

            builder.Property(e => e.ModuleState).HasColumnName("ModuleState");
            builder.Property(e => e.IsBusy).HasColumnName("IsBusy");
            builder.Property(e => e.IsReady).HasColumnName("IsReady");
            builder.Property(e => e.IsError).HasColumnName("IsError");
            builder.Property(e => e.KeyLock).HasColumnName("KeyLock");
        }
    }
}