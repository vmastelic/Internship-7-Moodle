using InternshipMoodle.Application.Common;
using InternshipMoodle.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Professors
{
    public class ProfessorCourseService
    {
        private readonly IAppDbContext _context;

        public ProfessorCourseService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetMyCoursesAsync(int professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .Include(c => c.Notifications)
                .Include(c => c.Materials)
                .ToListAsync();
        }
    }
}
