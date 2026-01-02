using InternshipMoodle.Application.Common;
using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Enrollments;
using InternshipMoodle.Domain.Entities.Materials;
using InternshipMoodle.Domain.Entities.Notifications;
using InternshipMoodle.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Professors
{
    public class ProfessorCourseManagementService
    {
        private readonly IAppDbContext _context;

        public ProfessorCourseManagementService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetMyCoursesAsync(int professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();
        }

        public async Task<List<User>> GetAllStudentsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == Domain.Enums.UserRole.Student)
                .ToListAsync();
        }

        public async Task<bool> AddStudentAsync(int courseId, int studentId)
        {
            var exists = await _context.Enrollments
                .AnyAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            if (exists) return false;

            _context.Enrollments.Add(new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddNotificationAsync(int courseId, int professorId, string title, string content)
        {
            _context.Notifications.Add(new Notification
            {
                CourseId = courseId,
                ProfessorId = professorId,
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task AddMaterialAsync(int courseId, string name, string url)
        {
            _context.Materials.Add(new Material
            {
                CourseId = courseId,
                Name = name,
                Url = url,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }
}
