using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Registering our new Core Profile tables
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<CbtExam> CbtExams { get; set; }
        public DbSet<CbtQuestion> CbtQuestions { get; set; }
        public DbSet<StudentCbtAttempt> StudentCbtAttempts { get; set; }
        public DbSet<StaffAttendance> StaffAttendances { get; set; }
        public DbSet<FeeInvoice> FeeInvoices { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Optional: Configure strict delete behavior so deleting a parent doesn't wipe out the student
            builder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}