using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class CombinedStatusEntityConfiguration : IEntityTypeConfiguration<CombinedStatusEntity>
{
    public void Configure(EntityTypeBuilder<CombinedStatusEntity> builder)
    {
        builder.ToTable("CombinedStatuses");
        
        builder.HasKey(combinedStatusEntity => combinedStatusEntity.Id);
        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<CombinedSamplerStatusEntity>("CombinedSamplerStatus")
            .HasValue<CombinedPumpStatusEntity>("CombinedPumpStatus")
            .HasValue<CombinedOvenStatusEntity>("CombinedOvenStatus");

        builder.Property(combinedStatusEntity => combinedStatusEntity.Discriminator).HasColumnName("Discriminator");
        builder.Property(combinedStatusEntity => combinedStatusEntity.ModuleState).HasColumnName("ModuleState");
        builder.Property(combinedStatusEntity => combinedStatusEntity.IsBusy).HasColumnName("IsBusy");
        builder.Property(combinedStatusEntity => combinedStatusEntity.IsReady).HasColumnName("IsReady");
        builder.Property(combinedStatusEntity => combinedStatusEntity.IsError).HasColumnName("IsError");
        builder.Property(combinedStatusEntity => combinedStatusEntity.KeyLock).HasColumnName("KeyLock");
    }
}