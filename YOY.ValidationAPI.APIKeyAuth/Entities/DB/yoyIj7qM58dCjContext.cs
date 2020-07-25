using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YOY.ValidationAPI.APIKeyAuth.Entities.DB
{
    public partial class yoyIj7qM58dCjContext : DbContext
    {
        private string _connString;

        public yoyIj7qM58dCjContext(string connString)
        {
            _connString = connString;
        }

        public yoyIj7qM58dCjContext(DbContextOptions<yoyIj7qM58dCjContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apikeys> Apikeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apikeys>(entity =>
            {
                entity.ToTable("APIKeys");

                entity.HasIndex(e => new { e.HashedKey, e.Discriminator })
                    .HasName("IX_APIKeys_HashedValue")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresUtcdate)
                    .HasColumnName("ExpiresUTCDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.HashedKey)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.IssuedUtcdate)
                    .HasColumnName("IssuedUTCDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastUsageDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
