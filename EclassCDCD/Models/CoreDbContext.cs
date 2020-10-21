using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EclassCDCD.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<AnswerDetails> AnswerDetails { get; set; }
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Faculties> Faculties { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Plans> Plans { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<UserRequests> UserRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS;Database=EclassCDCD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Accounts_Departments");
            });

            modelBuilder.Entity<AnswerDetails>(entity =>
            {
                entity.HasKey(e => new { e.AnswerId, e.QuestionId });

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.AnswerDetails)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_AnswerDetails_Answers");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AnswerDetails)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_AnswerDetails_Questions");
            });

            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK_Answers_1");

                entity.Property(e => e.AnswerId).ValueGeneratedNever();

                entity.Property(e => e.StudentId).IsUnicode(false);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Plans");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.CateId).IsUnicode(false);
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.FacultyId).IsUnicode(false);

                entity.Property(e => e.Period).IsUnicode(false);

                entity.Property(e => e.Year)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Classes_Employees");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.FacultyId).IsUnicode(false);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_Departments_Faculties");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.Birthday).IsUnicode(false);

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employees_Departments");
            });

            modelBuilder.Entity<Faculties>(entity =>
            {
                entity.Property(e => e.FacultyId).IsUnicode(false);
            });

            modelBuilder.Entity<Options>(entity =>
            {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Options)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Options_Questions");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.FacultyId).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Permissio__Facul__6EF57B66");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Permissio__Usern__6FE99F9F");
            });

            modelBuilder.Entity<Plans>(entity =>
            {
                entity.Property(e => e.CateId).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.HasMark).HasDefaultValueSql("((0))");

                entity.Property(e => e.MarkCode).IsUnicode(false);

                entity.Property(e => e.SubjectId).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plans__CateID__4BAC3F29");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Plans_Classes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Plans_Employees");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Plans_Subjects");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.Property(e => e.QuestionId).ValueGeneratedNever();

                entity.Property(e => e.CateId).IsUnicode(false);

                entity.Property(e => e.TypeId).IsUnicode(false);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Categories");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Types");
            });

            modelBuilder.Entity<Scores>(entity =>
            {
                entity.Property(e => e.StudentId).IsUnicode(false);

                entity.Property(e => e.SubjectId).IsUnicode(false);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Scores__StudentI__29221CFB");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Scores__SubjectI__2A164134");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.StudentId).IsUnicode(false);

                entity.Property(e => e.Birthday).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Students_Classes");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.Property(e => e.SubjectId).IsUnicode(false);

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subjects_Departments");
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.Property(e => e.TypeId).IsUnicode(false);
            });

            modelBuilder.Entity<UserRequests>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.IsFromStudent).HasDefaultValueSql("((1))");

                entity.Property(e => e.RequestTime).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
