using System;
using ChemsoftTest.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemsoftTest.Core.Database;

public class AppDbContext : DbContext
{
    public AppDbContext() { }

    static AppDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public virtual DbSet<PersonEntity> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Host=localhost;Database=ChemsoftTest;Username=sonikku;Password=12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity
                .HasKey(e => e.Id)
                .HasName("Person_pkey");

            entity.ToTable("Person");

            entity
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
    }
}