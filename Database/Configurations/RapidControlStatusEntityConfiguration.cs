using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class RapidControlStatusEntityConfiguration : IEntityTypeConfiguration<RapidControlStatusEntity>
{
    public void Configure(EntityTypeBuilder<RapidControlStatusEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.IsBusy);
        builder.Property(e => e.IsReady);
        builder.Property(e => e.IsError);
        builder.Property(e => e.KeyLock);
        builder.HasOne(e => e.CombinedStatus)
            .WithMany()
            .HasForeignKey("CombinedStatusId")
            ;
    }
}