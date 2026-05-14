using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context     = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager);
        await SeedClassRoomsAsync(context);
        await SeedTeacherAsync(context);
        await SeedSubjectsAsync(context);
        await SeedParentAsync(context);
        await SeedStudentsAsync(context);
    }

    // =========================================================================
    // STEP 1 — ROLES
    // =========================================================================
    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Admin", "Teacher", "Student", "Parent"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"[Seeder] Role created: {role}");
            }
        }
    }

    // =========================================================================
    // STEP 2 — ADMIN USER
    // The ONLY account the seeder creates with a login.
    // IsFirstLogin = false — deployment password is intentional and known.
    // All other accounts are provisioned by Admin through the portal.
    // =========================================================================
    private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@schoolms.com";

        if (await userManager.FindByEmailAsync(adminEmail) is not null)
            return;

        var admin = new ApplicationUser
        {
            UserName       = adminEmail,
            Email          = adminEmail,
            FirstName      = "System",
            LastName       = "Admin",
            IsActive       = true,
            EmailConfirmed = true,
            IsFirstLogin   = false  // exempt from forced password change
        };

        var result = await userManager.CreateAsync(admin, "Admin@12345");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine($"[Seeder] Admin user created: {adminEmail}");
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Console.WriteLine($"[Seeder] ERROR creating admin: {errors}");
        }
    }

    // =========================================================================
    // STEP 3 — CLASSROOMS
    // =========================================================================
    private static async Task SeedClassRoomsAsync(ApplicationDbContext context)
    {
        if (await context.ClassRooms.AnyAsync())
            return;

        var classRooms = new List<ClassRoom>
        {
            new() { Name = "JSS 1", Arm = "A" },
            new() { Name = "JSS 2", Arm = "A" },
            new() { Name = "SS 1",  Arm = "A" }
        };

        await context.ClassRooms.AddRangeAsync(classRooms);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] ClassRooms seeded: {classRooms.Count} records");
    }

    // =========================================================================
    // STEP 4 — TEACHER PROFILE (no login account)
    // DESIGN FIX: Only the profile record is created. UserId = null.
    // Admin provisions the login through the portal — triggers welcome email.
    // =========================================================================
    private static async Task SeedTeacherAsync(ApplicationDbContext context)
    {
        if (await context.Teachers.AnyAsync())
            return;

        var jss1 = await context.ClassRooms
            .FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");

        if (jss1 is null)
        {
            Console.WriteLine("[Seeder] SKIPPED teacher — ClassRoom not found.");
            return;
        }

        var teacher = new Teacher
        {
            UserId         = null,  // login provisioned by Admin later
            StaffNo        = "TCH-001",
            HireDate       = new DateTime(2020, 9, 1),
            BasicSalary    = 250_000.00m,
            Qualifications = "B.Ed Mathematics, University of Lagos"
        };

        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();

        jss1.FormTeacherId = teacher.Id;
        await context.SaveChangesAsync();

        Console.WriteLine("[Seeder] Teacher profile seeded: TCH-001 (no login yet)");
    }

    // =========================================================================
    // STEP 5 — SUBJECTS
    // =========================================================================
    private static async Task SeedSubjectsAsync(ApplicationDbContext context)
    {
        if (await context.Subjects.AnyAsync())
            return;

        var jss1 = await context.ClassRooms
            .FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");

        var teacher = await context.Teachers
            .FirstOrDefaultAsync(t => t.StaffNo == "TCH-001");

        if (jss1 is null || teacher is null)
        {
            Console.WriteLine("[Seeder] SKIPPED subjects — ClassRoom or Teacher not found.");
            return;
        }

        var subjects = new List<Subject>
        {
            new() { Name = "Mathematics",      Code = "MTH101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true },
            new() { Name = "English Language", Code = "ENG101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true },
            new() { Name = "Basic Science",    Code = "BSC101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true }
        };

        await context.Subjects.AddRangeAsync(subjects);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Subjects seeded: {subjects.Count} records");
    }

    // =========================================================================
    // STEP 6 — PARENT PROFILE (no login account)
    // DESIGN FIX: Only the profile record is created. UserId = null.
    // =========================================================================
    private static async Task SeedParentAsync(ApplicationDbContext context)
    {
        if (await context.Parents.AnyAsync())
            return;

        var parent = new Parent
        {
            UserId      = null,  // login provisioned by Admin later
            Occupation  = "Engineer",
            HomeAddress = "14 Adeyemo Alakija Street, Victoria Island, Lagos"
        };

        await context.Parents.AddAsync(parent);
        await context.SaveChangesAsync();
        Console.WriteLine("[Seeder] Parent profile seeded (no login yet)");
    }

    // =========================================================================
    // STEP 7 — STUDENT PROFILES (no login accounts)
    // =========================================================================
    private static async Task SeedStudentsAsync(ApplicationDbContext context)
    {
        if (await context.Students.AnyAsync())
            return;

        var jss1   = await context.ClassRooms.FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");
        var parent = await context.Parents.FirstOrDefaultAsync();

        if (jss1 is null || parent is null)
        {
            Console.WriteLine("[Seeder] SKIPPED students — ClassRoom or Parent not found.");
            return;
        }

        var students = new List<Student>
        {
            new()
            {
                UserId         = null,  // login provisioned by Admin later
                ParentId       = parent.Id,
                ClassRoomId    = jss1.Id,
                AdmissionNo    = "STU-2025-001",
                DateOfBirth    = new DateTime(2012, 3, 15),
                EnrollmentDate = new DateTime(2025, 9, 1)
            },
            new()
            {
                UserId         = null,
                ParentId       = parent.Id,
                ClassRoomId    = jss1.Id,
                AdmissionNo    = "STU-2025-002",
                DateOfBirth    = new DateTime(2013, 7, 22),
                EnrollmentDate = new DateTime(2025, 9, 1)
            }
        };

        await context.Students.AddRangeAsync(students);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Student profiles seeded: {students.Count} records (no logins yet)");
    }
}