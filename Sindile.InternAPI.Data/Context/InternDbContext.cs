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

        public virtual DbSet<Intern> Interns { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskLog> TaskLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WINPROCLEAN;Initial Catalog=InternAdminDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Intern>(entity =>
            {
                entity.ToTable("Intern");

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

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Interns)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Intern__RoleId__286302EC");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Role1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Role");

                entity.Property(e => e.HourlyRate).HasColumnType("double");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaskLog>(entity =>
            {
                //entity.HasKey(e => new { e.InternId, e.TaskId })
                //    .HasName("PK_InternTask");

                entity.ToTable("TaskLog");

                entity.HasOne(d => d.Intern)
                    .WithMany(p => p.TaskLogs)
                    .HasForeignKey(d => d.InternId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskLog__InternI__2D27B809");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskLogs)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskLog__TaskId__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
