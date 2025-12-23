using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Domain.Entities.Enrollments
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int StudentId { get; set; }
        public User Student { get; set; } = null!;
    }
}
