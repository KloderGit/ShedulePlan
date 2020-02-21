using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Service.DomainDatabaseEF;
using System;

namespace DomainDatabaseManagerEF
{
    public class DomainDatabaseContext : DbContext
    {
        public DomainDatabaseContext()
        {
        }

        public DomainDatabaseContext(DbContextOptions<DomainDatabaseContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Anotherdb> Anotherdb { get; set; }

        public virtual DbSet<Node> UnitLayouts { get; set; }
        public virtual DbSet<Param> UnitParams { get; set; }
        public virtual DbSet<Unit> Units { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DomainBase;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Anotherdb>(entity =>
            {
                entity.ToTable("Anotherdb", "SecondScheme");
                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Units", "Domain");
                entity.Property(e => e.Created).HasColumnType("timestamp with time zone");
                entity.Property(e => e.Modified).HasColumnType("timestamp with time zone");
            });


            modelBuilder.Entity<Node>(entity =>
            {
                entity.ToTable("UnitLayouts", "Domain");
                entity.Property(e => e.Created).HasColumnType("timestamp with time zone");
                entity.Property(e => e.Modified).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.ParentId);
                entity.HasIndex(e => e.ParentId);

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Layouts)
                    .HasForeignKey(d => d.UnitId);
                entity.HasIndex(e => e.UnitId);
            });

            modelBuilder.Entity<Param>(entity =>
            {
                entity.ToTable("UnitParams", "Domain");
                entity.Property(e => e.Created).HasColumnType("timestamp with time zone");
                entity.Property(e => e.Modified).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Params)
                    .HasForeignKey(d => d.UnitId);
                entity.HasIndex(e => e.UnitId);

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.Params)
                    .HasForeignKey(d => d.UnitLayoutId);
                entity.HasIndex(e => e.UnitLayoutId);
            });

        }

    }
}
