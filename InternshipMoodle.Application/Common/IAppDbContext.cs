using InternshipMoodle.Domain.Entities.Users;
using InternshipMoodle.Domain.Entities.Courses;
using InternshipMoodle.Domain.Entities.Enrollments;
using InternshipMoodle.Domain.Entities.Notifications;
using InternshipMoodle.Domain.Entities.Materials;
using InternshipMoodle.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Common
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Course> Courses { get; }
        DbSet<Enrollment> Enrollments { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Material> Materials { get; }
        DbSet<Message> Messages { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
