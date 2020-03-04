using System;
using Lunabank.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lunabank.Data.Entities
{
    public partial class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }



        //        protected override void OnModelCreating(ModelBuilder modelBuilder)
        //        {
        //            base.OnModelCreating(modelBuilder);
        //            modelBuilder.Entity<Accounts>(entity =>
        //            {
        //                entity.Property(e => e.AccountId).ValueGeneratedNever();
        //
        //                entity.Property(e => e.AccountNumber).IsUnicode(false);
        //
        //                entity.Property(e => e.AccountType).IsUnicode(false);
        //
        //                entity.Property(e => e.Status).IsUnicode(false);
        //
        //                entity.HasOne(d => d.User)
        //                    .WithMany(p => p.Accounts)
        //                    .HasForeignKey(d => d.UserId)
        //                    .OnDelete(DeleteBehavior.ClientSetNull)
        //                    .HasConstraintName("FK_Accounts_Accounts");
        //            });
        //
        //            modelBuilder.Entity<AspNetRoleClaims>(entity =>
        //            {
        //                entity.HasIndex(e => e.RoleId);
        //            });
        //
        //            modelBuilder.Entity<AspNetRoles>(entity =>
        //            {
        //                entity.HasIndex(e => e.NormalizedName)
        //                    .HasName("RoleNameIndex")
        //                    .IsUnique()
        //                    .HasFilter("([NormalizedName] IS NOT NULL)");
        //            });
        //
        //            modelBuilder.Entity<AspNetUserClaims>(entity =>
        //            {
        //                entity.HasIndex(e => e.UserId);
        //            });
        //
        //            modelBuilder.Entity<AspNetUserLogins>(entity =>
        //            {
        //                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        //
        //                entity.HasIndex(e => e.UserId);
        //            });
        //
        //            modelBuilder.Entity<AspNetUserRoles>(entity =>
        //            {
        //                entity.HasKey(e => new { e.UserId, e.RoleId });
        //
        //                entity.HasIndex(e => e.RoleId);
        //            });
        //
        //            modelBuilder.Entity<AspNetUserTokens>(entity =>
        //            {
        //                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        //            });
        //
        //            modelBuilder.Entity<AspNetUsers>(entity =>
        //            {
        //                entity.HasIndex(e => e.NormalizedEmail)
        //                    .HasName("EmailIndex");
        //
        //                entity.HasIndex(e => e.NormalizedUserName)
        //                    .HasName("UserNameIndex")
        //                    .IsUnique()
        //                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
        //            });
        //
        //            modelBuilder.Entity<Transactions>(entity =>
        //            {
        //                entity.Property(e => e.TransactionId).ValueGeneratedNever();
        //
        //                entity.Property(e => e.AccountNumber).IsUnicode(false);
        //
        //                entity.Property(e => e.TransactionType).IsUnicode(false);
        //            });
        //
        //            OnModelCreatingPartial(modelBuilder);
        //        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
