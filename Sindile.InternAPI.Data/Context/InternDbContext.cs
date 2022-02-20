using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sindile.InternAPI.Data
{
    public partial class InternDbContext : DbContext
    {
        public InternDbContext()
        {
        }

        public InternDbContext(DbContextOptions<InternDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Occupation> Occupations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Task> Task { get; set; } = null!;
        public virtual DbSet<TaskLog> TaskLog { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.8.100,1433;Initial Catalog=InternAdminDB; User Id=SA;Password=Sin@sql2;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupation>(entity =>
            {
                entity.ToTable("Occupation");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SignOnDate).HasColumnType("datetime");

                entity.HasOne(d => d.Occupation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OccupationId)
                    .HasConstraintName("FK__Users__Occupatio__398D8EEE");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
