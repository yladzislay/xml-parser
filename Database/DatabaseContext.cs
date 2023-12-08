using Database.Configurations;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<InstrumentStatusEntity> InstrumentStatuses { get; set; }
    public DbSet<RapidControlStatusEntity> RapidControlStatuses { get; set; }
    public DbSet<DeviceStatusEntity> DeviceStatuses { get; set; }
    public DbSet<CombinedStatusEntity> CombinedStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CombinedOvenStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CombinedPumpStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CombinedSamplerStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CombinedStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RapidControlStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceStatusEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstrumentStatusEntityConfiguration());
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=instrument_statuses.db");
        }
    }
}