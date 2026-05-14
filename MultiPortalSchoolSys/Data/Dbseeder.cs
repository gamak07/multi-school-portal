using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context     = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Apply any pending migrations automatically on startup
        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager);
        await SeedClassRoomsAsync(context);
        await SeedTeacherAsync(context, userManager);
        await SeedSubjectsAsync(context);
        await SeedParentAsync(context, userManager);
        await SeedStudentsAsync(context);
    }

    // =========================================================================
    // STEP 1 — ROLES
    // Must exist before ANY user can be assigned a role.
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
    // Root user. Not linked to a Teacher/Parent/Student profile.
    // Change the password in appsettings before deploying to production.
    // =========================================================================
    private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@schoolms.com";

        if (await userManager.FindByEmailAsync(adminEmail) is not null)
            return;

        var admin = new ApplicationUser
        {
            UserName  = adminEmail,
            Email     = adminEmail,
            FirstName = "System",
            LastName  = "Admin",
            IsActive  = true,
            EmailConfirmed = true
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
    // Must exist before Teachers (FormTeacher FK) and Students (ClassRoom FK).
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
    // STEP 4 — TEACHER
    // Creates ApplicationUser first, then the Teacher profile,
    // then assigns the Teacher as FormTeacher of JSS 1A.
    // =========================================================================
    private static async Task SeedTeacherAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        const string teacherEmail = "john.adeyemi@schoolms.com";

        if (await userManager.FindByEmailAsync(teacherEmail) is not null)
            return;

        // 1. Create the Identity login
        var teacherUser = new ApplicationUser
        {
            UserName  = teacherEmail,
            Email     = teacherEmail,
            FirstName = "John",
            LastName  = "Adeyemi",
            IsActive  = true,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(teacherUser, "Teacher@12345");

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Console.WriteLine($"[Seeder] ERROR creating teacher user: {errors}");
            return;
        }

        await userManager.AddToRoleAsync(teacherUser, "Teacher");

        // 2. Create the Teacher profile record
        var teacher = new Teacher
        {
            UserId         = teacherUser.Id,
            StaffNo        = "TCH-001",
            HireDate       = new DateTime(2020, 9, 1),
            BasicSalary    = 250_000.00m,
            Qualifications = "B.Ed Mathematics, University of Lagos"
        };

        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();

        // 3. Assign as FormTeacher of JSS 1A
        var jss1 = await context.ClassRooms
            .FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");

        if (jss1 is not null)
        {
            jss1.FormTeacherId = teacher.Id;
            await context.SaveChangesAsync();
        }

        Console.WriteLine($"[Seeder] Teacher created: {teacherEmail}");
    }

    // =========================================================================
    // STEP 5 — SUBJECTS
    // Linked to JSS 1A and the seeded Teacher.
    // Must run after SeedTeacherAsync and SeedClassRoomsAsync.
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
            new() { Name = "Mathematics",     Code = "MTH101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true },
            new() { Name = "English Language", Code = "ENG101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true },
            new() { Name = "Basic Science",   Code = "BSC101", ClassId = jss1.Id, TeacherId = teacher.Id, IsActive = true }
        };

        await context.Subjects.AddRangeAsync(subjects);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Subjects seeded: {subjects.Count} records");
    }

    // =========================================================================
    // STEP 6 — PARENT
    // Creates ApplicationUser first, then the Parent profile.
    // Must exist before Students (ParentId FK).
    // =========================================================================
    private static async Task SeedParentAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        const string parentEmail = "emeka.okafor@schoolms.com";

        if (await userManager.FindByEmailAsync(parentEmail) is not null)
            return;

        // 1. Create the Identity login
        var parentUser = new ApplicationUser
        {
            UserName  = parentEmail,
            Email     = parentEmail,
            FirstName = "Emeka",
            LastName  = "Okafor",
            IsActive  = true,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(parentUser, "Parent@12345");

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Console.WriteLine($"[Seeder] ERROR creating parent user: {errors}");
            return;
        }

        await userManager.AddToRoleAsync(parentUser, "Parent");

        // 2. Create the Parent profile record
        var parent = new Parent
        {
            UserId      = parentUser.Id,
            Occupation  = "Engineer",
            HomeAddress = "14 Adeyemo Alakija Street, Victoria Island, Lagos"
        };

        await context.Parents.AddAsync(parent);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Parent created: {parentEmail}");
    }

    // =========================================================================
    // STEP 7 — STUDENTS
    // Each student needs an ApplicationUser (for portal login),
    // a Parent (FK), and a ClassRoom (FK).
    // Must run last — depends on all previous steps.
    // =========================================================================
    private static async Task SeedStudentsAsync(ApplicationDbContext context)
    {
        if (await context.Students.AnyAsync())
            return;

        var jss1 = await context.ClassRooms
            .FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");

        var parent = await context.Parents
            .FirstOrDefaultAsync();

        if (jss1 is null || parent is null)
        {
            Console.WriteLine("[Seeder] SKIPPED students — ClassRoom or Parent not found.");
            return;
        }

        var students = new List<Student>
        {
            new()
            {
                ParentId        = parent.Id,
                ClassRoomId     = jss1.Id,
                AdmissionNo     = "STU-2025-001",
                DateOfBirth     = new DateTime(2012, 3, 15),
                EnrollmentDate  = new DateTime(2025, 9, 1)
            },
            new()
            {
                ParentId        = parent.Id,
                ClassRoomId     = jss1.Id,
                AdmissionNo     = "STU-2025-002",
                DateOfBirth     = new DateTime(2013, 7, 22),
                EnrollmentDate  = new DateTime(2025, 9, 1)
            }
        };

        await context.Students.AddRangeAsync(students);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Students seeded: {students.Count} records");
    }
}