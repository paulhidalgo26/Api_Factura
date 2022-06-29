using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiFactura.Models
{
    public partial class carShopinContext : DbContext
    {
        public carShopinContext()
        {
        }

        public carShopinContext(DbContextOptions<carShopinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<ShoppingCartDetail> ShoppingCartDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SQL5109.site4now.net;Initial Catalog=db_a8210f_athanasia;User Id=db_a8210f_athanasia_admin;Password=ratchet663");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IduserClient).HasColumnName("IDUserClient");
            });

            modelBuilder.Entity<ShoppingCartDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.IdshoppingCart).HasColumnName("IDShoppingCart");

                entity.HasOne(d => d.IdshoppingCartNavigation)
                    .WithMany(p => p.ShoppingCartDetails)
                    .HasForeignKey(d => d.IdshoppingCart)
                    .HasConstraintName("FK_ShoppingCartDetails_ShoppingCart");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
