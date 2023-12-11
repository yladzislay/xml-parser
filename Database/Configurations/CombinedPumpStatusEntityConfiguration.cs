using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class CombinedPumpStatusEntityConfiguration : IEntityTypeConfiguration<CombinedPumpStatusEntity>
{
    public void Configure(EntityTypeBuilder<CombinedPumpStatusEntity> builder)
    {
        builder.HasBaseType<CombinedStatusEntity>();
    }
}