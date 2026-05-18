using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.Assessment;
using MultiPortalSchoolSys.Domain.Entities.Calendar;
using MultiPortalSchoolSys.Domain.Entities.Content;
using MultiPortalSchoolSys.Domain.Entities.Finance;
using MultiPortalSchoolSys.Domain.Entities.HR;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Infrastructure.Identity;

namespace MultiPortalSchoolSys.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // ── People ────────────────────────────────────────────────────────────────
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Parent> Parents { get; set; }

    // ── Academic ──────────────────────────────────────────────────────────────
    public DbSet<ClassRoom> ClassRooms { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentResult> StudentResults { get; set; }
    public DbSet<StudentAttendance> StudentAttendances { get; set; }

    // ── Assessment ────────────────────────────────────────────────────────────
    public DbSet<CbtExam> CbtExams { get; set; }
    public DbSet<CbtQuestion> CbtQuestions { get; set; }
    public DbSet<TheoryExam> TheoryExams { get; set; }
    public DbSet<TheoryQuestion> TheoryQuestions { get; set; }
    public DbSet<PrintableExam> PrintableExams { get; set; }
    public DbSet<StudentCbtAttempt> StudentCbtAttempts { get; set; }
    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    // ── Finance ───────────────────────────────────────────────────────────────
    public DbSet<FeeInvoice> FeeInvoices { get; set; }
    public DbSet<PaymentReceipt> PaymentReceipts { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }

    // ── HR ────────────────────────────────────────────────────────────────────
    public DbSet<StaffAttendance> StaffAttendances { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<Sanction> Sanctions { get; set; }

    // ── Calendar ──────────────────────────────────────────────────────────────
    public DbSet<AcademicTerm> AcademicTerms { get; set; }
    public DbSet<SchoolEvent> SchoolEvents { get; set; }

    // ── Content ───────────────────────────────────────────────────────────────
    public DbSet<Material> Materials { get; set; }
    public DbSet<LessonNote> LessonNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    
        builder.Entity<Parent>()
            .HasMany(p => p.Children)
            .WithOne(s => s.Parent)
            .HasForeignKey(s => s.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── StudentAnswer FK rules ───────────────────────────────────────────
        builder.Entity<StudentAnswer>()
            .HasOne(sa => sa.Exam)
            .WithMany()
            .HasForeignKey(sa => sa.ExamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<StudentAnswer>()
            .HasOne(sa => sa.Question)
            .WithMany()
            .HasForeignKey(sa => sa.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── CbtExam approval FK (no cascade — Admin user must not be deleted) ─
        builder.Entity<CbtExam>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.ApprovedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TheoryExam>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.ApprovedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<PrintableExam>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.ApprovedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── LeaveRequest Admin FK ────────────────────────────────────────────
        builder.Entity<LeaveRequest>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(l => l.ReviewedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Sanction FK rules ────────────────────────────────────────────────
        builder.Entity<Sanction>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(s => s.IssuedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Sanction>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(s => s.IssuedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── SchoolEvent Admin FK ─────────────────────────────────────────────
        builder.Entity<SchoolEvent>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.CreatedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── Unique constraints ───────────────────────────────────────────────
        builder.Entity<Student>()
            .HasIndex(s => s.AdmissionNo)
            .IsUnique();

        builder.Entity<Teacher>()
            .HasIndex(t => t.StaffNo)
            .IsUnique();

        builder.Entity<Subject>()
            .HasIndex(s => s.Code)
            .IsUnique();
    }
}