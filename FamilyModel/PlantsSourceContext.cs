using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace FamilyModel;

public partial class PlantsSourceContext : DbContext
{
    public PlantsSourceContext()
    {
    }

    public PlantsSourceContext(DbContextOptions<PlantsSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<Genu> Genus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(e => e.FamilyId).HasName("PK__Table__41D82F6B84CD1217");
        });

        modelBuilder.Entity<Genu>(entity =>
        {
            entity.HasKey(e => e.GenusId).HasName("PK__Table__110363700F6A1A72");

            entity.HasOne(d => d.Family).WithMany(p => p.Genus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Genus_Family");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
