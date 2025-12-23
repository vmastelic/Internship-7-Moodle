using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Domain.Entities.Notifications
{
    public class Notification
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int ProfessorId { get; set; }
        public User Professor { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
