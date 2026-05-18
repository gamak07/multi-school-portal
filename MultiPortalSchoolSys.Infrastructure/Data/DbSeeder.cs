using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Domain.Entities.Academic;
using MultiPortalSchoolSys.Domain.Entities.People;
using MultiPortalSchoolSys.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using MultiPortalSchoolSys.Infrastructure.Identity;

namespace MultiPortalSchoolSys.Infrastructure.Data;

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
        await SeedTeacherAsync(context, userManager);
        await SeedSubjectsAsync(context);
        await SeedParentAndStudentsAsync(context, userManager);
    }

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

    private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
    {
        const string email = "admin@schoolms.com";
        if (await userManager.FindByEmailAsync(email) is not null) return;

        var admin = new ApplicationUser
        {
            UserName       = email,
            Email          = email,
            FirstName      = "System",
            LastName       = "Admin",
            IsActive       = true,
            EmailConfirmed = true,
            Status         = UserStatus.Active,
            IsFirstLogin   = false  // deployment password is intentional
        };

        var result = await userManager.CreateAsync(admin, "Admin@12345");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine($"[Seeder] Admin created: {email}");
        }
        else
        {
            Console.WriteLine($"[Seeder] ERROR: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    private static async Task SeedClassRoomsAsync(ApplicationDbContext context)
    {
        if (await context.ClassRooms.AnyAsync()) return;

        var classRooms = new List<ClassRoom>
        {
            new() { Name = "JSS 1", Arm = "A" },
            new() { Name = "JSS 2", Arm = "A" },
            new() { Name = "SS 1",  Arm = "A" }
        };

        await context.ClassRooms.AddRangeAsync(classRooms);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] ClassRooms seeded: {classRooms.Count}");
    }

    private static async Task SeedTeacherAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        if (await context.Teachers.AnyAsync()) return;

        const string email = "john.adeyemi@schoolms.com";
        if (await userManager.FindByEmailAsync(email) is not null) return;

        // Create login account first — UserId is required immediately
        var user = new ApplicationUser
        {
            UserName       = email,
            Email          = email,
            FirstName      = "John",
            LastName       = "Adeyemi",
            IsActive       = true,
            EmailConfirmed = true,
            Status         = UserStatus.PendingActivation,
            IsFirstLogin   = true
        };

        var result = await userManager.CreateAsync(user, "Teacher@12345");
        if (!result.Succeeded)
        {
            Console.WriteLine($"[Seeder] ERROR creating teacher: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            return;
        }

        await userManager.AddToRoleAsync(user, "Teacher");

        var teacher = new Teacher
        {
            UserId         = user.Id,
            StaffNo        = "TCH-001",
            HireDate       = new DateTime(2020, 9, 1),
            BasicSalary    = 250_000.00m,
            Qualifications = "B.Ed Mathematics, University of Lagos"
        };

        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();

        var jss1 = await context.ClassRooms.FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");
        if (jss1 is not null)
        {
            jss1.FormTeacherId = teacher.Id;
            await context.SaveChangesAsync();
        }

        Console.WriteLine($"[Seeder] Teacher seeded: {email}");
    }

    private static async Task SeedSubjectsAsync(ApplicationDbContext context)
    {
        if (await context.Subjects.AnyAsync()) return;

        var jss1    = await context.ClassRooms.FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");
        var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.StaffNo == "TCH-001");

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
        Console.WriteLine($"[Seeder] Subjects seeded: {subjects.Count}");
    }

    private static async Task SeedParentAndStudentsAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        if (await context.Parents.AnyAsync()) return;

        var jss1 = await context.ClassRooms.FirstOrDefaultAsync(c => c.Name == "JSS 1" && c.Arm == "A");
        if (jss1 is null) return;

        // Parent account
        const string parentEmail = "emeka.okafor@schoolms.com";
        var parentUser = new ApplicationUser
        {
            UserName       = parentEmail,
            Email          = parentEmail,
            FirstName      = "Emeka",
            LastName       = "Okafor",
            IsActive       = true,
            EmailConfirmed = true,
            Status         = UserStatus.PendingActivation,
            IsFirstLogin   = true
        };

        var parentResult = await userManager.CreateAsync(parentUser, "Parent@12345");
        if (!parentResult.Succeeded) return;

        await userManager.AddToRoleAsync(parentUser, "Parent");

        var parent = new Parent
        {
            UserId      = parentUser.Id,
            Occupation  = "Engineer",
            HomeAddress = "14 Adeyemo Alakija Street, Victoria Island, Lagos"
        };

        await context.Parents.AddAsync(parent);
        await context.SaveChangesAsync();

        // Student 1 account
        var stu1User = new ApplicationUser
        {
            UserName       = "stu001@schoolms.com",
            Email          = "stu001@schoolms.com",
            FirstName      = "Chidi",
            LastName       = "Okafor",
            IsActive       = true,
            EmailConfirmed = true,
            Status         = UserStatus.PendingActivation,
            IsFirstLogin   = true
        };
        await userManager.CreateAsync(stu1User, "Student@12345");
        await userManager.AddToRoleAsync(stu1User, "Student");

        // Student 2 account
        var stu2User = new ApplicationUser
        {
            UserName       = "stu002@schoolms.com",
            Email          = "stu002@schoolms.com",
            FirstName      = "Amaka",
            LastName       = "Okafor",
            IsActive       = true,
            EmailConfirmed = true,
            Status         = UserStatus.PendingActivation,
            IsFirstLogin   = true
        };
        await userManager.CreateAsync(stu2User, "Student@12345");
        await userManager.AddToRoleAsync(stu2User, "Student");

        var students = new List<Student>
        {
            new()
            {
                UserId         = stu1User.Id,
                ParentId       = parent.Id,
                ClassRoomId    = jss1.Id,
                AdmissionNo    = "STU-2025-001",
                DateOfBirth    = new DateTime(2012, 3, 15),
                EnrollmentDate = new DateTime(2025, 9, 1)
            },
            new()
            {
                UserId         = stu2User.Id,
                ParentId       = parent.Id,
                ClassRoomId    = jss1.Id,
                AdmissionNo    = "STU-2025-002",
                DateOfBirth    = new DateTime(2013, 7, 22),
                EnrollmentDate = new DateTime(2025, 9, 1)
            }
        };

        await context.Students.AddRangeAsync(students);
        await context.SaveChangesAsync();
        Console.WriteLine($"[Seeder] Parent + {students.Count} students seeded");
    }
}