using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class CombinedStatusEntityConfiguration : IEntityTypeConfiguration<CombinedStatusEntity>
{
    public void Configure(EntityTypeBuilder<CombinedStatusEntity> builder)
    {
        builder.HasKey(combinedStatusEntity => combinedStatusEntity.Id);
    }
}