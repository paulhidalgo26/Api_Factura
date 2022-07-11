using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiFactura.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SaleDetail> SaleDetails { get; set; } = null!;
        public virtual DbSet<UserAdmin> UserAdmins { get; set; } = null!;
        public virtual DbSet<UserClient> UserClients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //source=PAUL\SQLEXPRESS;initial catalog=BDFactura;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
                optionsBuilder.UseSqlServer("Data Source=SQL8002.site4now.net;Initial Catalog=db_a88d98_appmivil;User Id=db_a88d98_appmivil_admin;Password=p.hidalgo12;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Genre).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                //entity.Property(e => e.Quantity).HasColumnName("Quantity");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IduserClient).HasColumnName("IDUserClient");

                entity.Property(e => e.Total).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.IduserClientNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.IduserClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_UserClient");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.ToTable("SaleDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Idsale).HasColumnName("IDSale");

                entity.HasOne(d => d.IdproductNavigation)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.Idproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleDetail_Product");

                entity.HasOne(d => d.IdsaleNavigation)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.Idsale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleDetail_Sale");
            });

            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.ToTable("UserAdmin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cedula).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(64);
            });

            modelBuilder.Entity<UserClient>(entity =>
            {
                entity.ToTable("UserClient");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cedula).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
