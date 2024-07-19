namespace PowerPuffBE.Data;

using Entities;
using Microsoft.EntityFrameworkCore;

public class PowerPuffDbContext : DbContext
{
    public DbSet<ReactorEntity> Reactors { get; set; }
    public DbSet<ReactorProductionChecksEntity> ReactorProductionChecks { get; set; }
    public DbSet<ImageEntity> Image { get; set; }

    public PowerPuffDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ReactorProductionChecksEntity>().HasOne(prdC => prdC.Reactor)
            .WithMany().HasForeignKey(r => r.ReactorId).OnDelete(DeleteBehavior.NoAction);
    }
}