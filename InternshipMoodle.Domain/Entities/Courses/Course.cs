using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Domain.Entities.Courses
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;
    }
}
