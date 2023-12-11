using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class RapidControlStatusEntityConfiguration : IEntityTypeConfiguration<RapidControlStatusEntity>
{
    public void Configure(EntityTypeBuilder<RapidControlStatusEntity> builder)
    {
        builder.HasKey(rapidControlStatusEntity => rapidControlStatusEntity.Id);
        builder.HasOne(rapidControlStatusEntity => rapidControlStatusEntity.CombinedStatus);
    }
}