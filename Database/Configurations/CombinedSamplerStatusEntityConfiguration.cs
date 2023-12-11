using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class CombinedSamplerStatusEntityConfiguration : IEntityTypeConfiguration<CombinedSamplerStatusEntity>
{
    public void Configure(EntityTypeBuilder<CombinedSamplerStatusEntity> builder)
    {
        builder.HasBaseType<CombinedStatusEntity>();
    }
}