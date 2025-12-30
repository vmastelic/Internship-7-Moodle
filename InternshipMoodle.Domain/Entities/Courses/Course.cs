using InternshipMoodle.Domain.Entities.Enrollments;
using InternshipMoodle.Domain.Entities.Materials;
using InternshipMoodle.Domain.Entities.Notifications;
using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Domain.Entities.Courses
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}
