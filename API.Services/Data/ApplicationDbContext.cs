using Microsoft.EntityFrameworkCore;
using Todo.API.Data.Entities;
using Todo.API.Controllers;

namespace Todo.API.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collaborators> Collaborators { get; set; } = null!;
        public virtual DbSet<Notes> Notes { get; set; } = null!;
        public virtual DbSet<Status> Status { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SUDOWCAM\\SQLEXPRESS;Initial Catalog=Todo;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collaborators>(entity =>
            {
                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.NoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collaborators_Notes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collaborators_Users");
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notes_Users");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notes_Status");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
