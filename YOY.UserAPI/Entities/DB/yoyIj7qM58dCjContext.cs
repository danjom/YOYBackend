using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace YOY.UserAPI.Entities.DB
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

        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiresUtc)
                    .HasColumnName("ExpiresUTC")
                    .HasColumnType("datetime");

                entity.Property(e => e.HashedValue).HasMaxLength(2048);

                entity.Property(e => e.IssuedUtc)
                    .HasColumnName("IssuedUTC")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
