using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Enrollments;
using InternshipMoodle.Domain.Entities.Materials;
using InternshipMoodle.Domain.Entities.Messages;
using InternshipMoodle.Domain.Entities.Notifications;
using InternshipMoodle.Domain.Entities.Users;
using InternshipMoodle.Domain.Enums;

namespace InternshipMoodle.Infrastructure.Persistence.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Users.Any())
                return;

            await SeedUsers(context);
            await SeedCourses(context);
            await SeedEnrollments(context);
            await SeedNotifications(context);
            await SeedMaterials(context);
            await SeedMessages(context);
        }

        private static async Task SeedUsers(AppDbContext context)
        {
            var users = new List<User>
            {
                new() { Email = "admin@moodle.test", PasswordHash = "admin123", Role = UserRole.Admin },

                new() { Email = "prof1@moodle.test", PasswordHash = "prof123", Role = UserRole.Professor },
                new() { Email = "prof2@moodle.test", PasswordHash = "prof123", Role = UserRole.Professor },

                new() { Email = "student1@moodle.test", PasswordHash = "student123", Role = UserRole.Student },
                new() { Email = "student2@moodle.test", PasswordHash = "student123", Role = UserRole.Student },
                new() { Email = "student3@moodle.test", PasswordHash = "student123", Role = UserRole.Student },
                new() { Email = "student4@moodle.test", PasswordHash = "student123", Role = UserRole.Student },
                new() { Email = "student5@moodle.test", PasswordHash = "student123", Role = UserRole.Student }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCourses(AppDbContext context)
        {
            var professors = context.Users
                .Where(u => u.Role == UserRole.Professor)
                .ToList();

            var courses = new List<Course>
            {
                new() { Name = "Programiranje 1", ProfessorId = professors[0].Id },
                new() { Name = "Baze podataka", ProfessorId = professors[1].Id },
                new() { Name = "Objektno orijentirano programiranje", ProfessorId = professors[0].Id }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }


        private static async Task SeedEnrollments(AppDbContext context)
        {
            var students = context.Users
                .Where(u => u.Role == UserRole.Student)
                .ToList();

            var courses = context.Courses.ToList();

            var enrollments = new List<Enrollment>
            {
                new() { StudentId = students[0].Id, CourseId = courses[0].Id },
                new() { StudentId = students[1].Id, CourseId = courses[0].Id },
                new() { StudentId = students[2].Id, CourseId = courses[1].Id },
                new() { StudentId = students[3].Id, CourseId = courses[1].Id },
                new() { StudentId = students[4].Id, CourseId = courses[2].Id }
            };

            context.Enrollments.AddRange(enrollments);
            await context.SaveChangesAsync();
        }


        private static async Task SeedNotifications(AppDbContext context)
        {
            var courses = context.Courses.ToList();

            var notifications = new List<Notification>
            {
                new()
                {
                    CourseId = courses[0].Id,
                    ProfessorId = courses[0].ProfessorId,
                    Title = "Dobrodošli",
                    Content = "Dobrodošli na kolegij Programiranje 1!"
                },
                new()
                {
                    CourseId = courses[1].Id,
                    ProfessorId = courses[1].ProfessorId,
                    Title = "Prva predavanja",
                    Content = "Materijali za prva predavanja su objavljeni."
                }
            };

            context.Notifications.AddRange(notifications);
            await context.SaveChangesAsync();
        }

        private static async Task SeedMaterials(AppDbContext context)
        {
            var courses = context.Courses.ToList();

            var materials = new List<Material>
            {
                new()
                {
                    CourseId = courses[0].Id,
                    Name = "Uvod u C#",
                    Url = "https://example.com/csharp-intro"
                },
                new()
                {
                    CourseId = courses[1].Id,
                    Name = "SQL osnove",
                    Url = "https://example.com/sql-basics"
                }
            };

            context.Materials.AddRange(materials);
            await context.SaveChangesAsync();
        }


        private static async Task SeedMessages(AppDbContext context)
        {
            var users = context.Users.ToList();

            var messages = new List<Message>
            {
                new()
                {
                    SenderId = users[1].Id,
                    ReceiverId = users[3].Id,
                    Content = "Pozdrav, jesi li razumio zadatak?"
                },
                new()
                {
                    SenderId = users[3].Id,
                    ReceiverId = users[1].Id,
                    Content = "Jesam, hvala! Sve jasno."
                },
                new()
                {
                    SenderId = users[2].Id,
                    ReceiverId = users[4].Id,
                    Content = "Vidimo se na predavanju."
                }
            };

            context.Messages.AddRange(messages);
            await context.SaveChangesAsync();
        }
    }
}
