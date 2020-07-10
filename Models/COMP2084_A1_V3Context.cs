using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace COMP2084___A1.Models
{
    public partial class COMP2084_A1_V3Context : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public COMP2084_A1_V3Context()
        {
        }

        public COMP2084_A1_V3Context(DbContextOptions<COMP2084_A1_V3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<EcoScore> EcoScore { get; set; }
        public virtual DbSet<UserSelection> UserSelection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=wkirschn\\sqlserver2019;Initial Catalog=COMP2084_A1_V3;Integrated Security=True");*/
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EcoScore>(entity =>
            {
                entity.Property(e => e.EcoscoreId).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ObjectName).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.Recycle).IsUnicode(false);

                entity.Property(e => e.Reduce).IsUnicode(false);

                entity.Property(e => e.Reuse).IsUnicode(false);
            });

            modelBuilder.Entity<UserSelection>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.ObjectName).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.Property(e => e.Recycle).IsUnicode(false);

                entity.Property(e => e.Reduce).IsUnicode(false);

                entity.Property(e => e.Reuse).IsUnicode(false);

                entity.HasOne(d => d.EcoscoreNavigation)
                    .WithMany(p => p.UserSelection)
                    .HasForeignKey(d => d.EcoscoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSelection_EcoScore");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
