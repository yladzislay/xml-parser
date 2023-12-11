using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class CombinedOvenStatusEntityConfiguration : IEntityTypeConfiguration<CombinedOvenStatusEntity>
{
    public void Configure(EntityTypeBuilder<CombinedOvenStatusEntity> builder)
    {
        builder.HasBaseType<CombinedStatusEntity>();
    }
}