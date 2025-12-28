using InternshipMoodle.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Common
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
