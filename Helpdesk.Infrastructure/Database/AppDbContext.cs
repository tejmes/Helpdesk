using Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskMessage> TaskMessages { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(x => x.Id);

                b.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TaskItem>(b =>
            {
                b.ToTable("TaskItems");
                b.HasKey(x => x.Id);

                b.Property(x => x.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                b.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(4000);

                b.Property(x => x.ReportedAt).IsRequired();
                b.Property(x => x.DueAt).IsRequired();

                b.Property(x => x.Priority).IsRequired();
                b.Property(x => x.Status).IsRequired();
                b.Property(x => x.ResolvedAt).IsRequired(false);

                b.HasOne(x => x.Reporter)
                    .WithMany()
                    .HasForeignKey(x => x.ReporterId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Solver)
                    .WithMany()
                    .HasForeignKey(x => x.SolverId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => x.Status);
                b.HasIndex(x => x.DueAt);
                b.HasIndex(x => x.ReporterId);
                b.HasIndex(x => x.SolverId);
            });

            modelBuilder.Entity<TaskMessage>(b =>
            {
                b.ToTable("TaskMessages");
                b.HasKey(x => x.Id);

                b.Property(x => x.Message)
                    .IsRequired()
                    .HasMaxLength(2000);

                b.Property(x => x.CreatedAt).IsRequired();

                b.HasOne(x => x.TaskItem)
                    .WithMany(t => t.Messages)
                    .HasForeignKey(x => x.TaskItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.Author)
                    .WithMany()
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => x.TaskItemId);
                b.HasIndex(x => x.AuthorId);
                b.HasIndex(x => x.CreatedAt);
            });

            modelBuilder.Entity<ChecklistItem>(b =>
            {
                b.ToTable("ChecklistItems");
                b.HasKey(x => x.Id);

                b.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(300);

                b.Property(x => x.IsDone).IsRequired();
                b.Property(x => x.DoneAt).IsRequired(false);
                b.Property(x => x.DueAt).IsRequired(false);

                b.HasOne(x => x.TaskItem)
                    .WithMany(t => t.ChecklistItems)
                    .HasForeignKey(x => x.TaskItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.DueAt);
                b.HasIndex(x => x.TaskItemId);
                b.HasIndex(x => x.IsDone);
            });

            modelBuilder.Entity<TaskAttachment>(b =>
            {
                b.ToTable("TaskAttachments");
                b.HasKey(x => x.Id);

                b.Property(x => x.OriginalFileName)
                    .IsRequired()
                    .HasMaxLength(260);

                b.Property(x => x.StoredFileName)
                    .IsRequired()
                    .HasMaxLength(260);

                b.Property(x => x.ContentType)
                    .IsRequired()
                    .HasMaxLength(200);

                b.Property(x => x.Size).IsRequired();
                b.Property(x => x.UploadedAt).IsRequired();

                b.HasOne(x => x.TaskItem)
                    .WithMany(t => t.Attachments)
                    .HasForeignKey(x => x.TaskItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.TaskItemId);
            });

            var dbInit = new DatabaseInit();

            modelBuilder.Entity<User>().HasData(dbInit.GetUsers());
            modelBuilder.Entity<TaskItem>().HasData(dbInit.GetTasks());
            modelBuilder.Entity<TaskMessage>().HasData(dbInit.GetMessages());
            modelBuilder.Entity<ChecklistItem>().HasData(dbInit.GetChecklistItems());
        }
    }
}
