using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FamilyModel;

public partial class PlantsSourceContext : IdentityDbContext<PlantGeneUser>
{
    public PlantsSourceContext()
    {
    }

    public PlantsSourceContext(DbContextOptions<PlantsSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Family> Family { get; set; }

    public virtual DbSet<Gene> Gene { get; set; }

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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(e => e.FamilyId).HasName("PK__Table__41D82F6B84CD1217");
        });

        modelBuilder.Entity<Gene>(entity =>
        {
            entity.HasKey(e => e.GeneId).HasName("PK__Table__110363700F6A1A72");

            entity.HasOne(d => d.Family).WithMany(p => p.Gene)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Genus_Family");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
