using InternshipMoodle.Application.Common;
using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Enrollments;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Students
{
    public class StudentCourseService
    {
        private readonly IAppDbContext _context;

        public StudentCourseService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetMyCoursesAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Professor)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Notifications)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Materials)
                .Select(e => e.Course)
                .ToListAsync();
        }

    }
}
