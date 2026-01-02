using InternshipMoodle.Application.Common;
using InternshipMoodle.Domain.Entities.Users;
using InternshipMoodle.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Admin
{
    public class AdminUserService
    {
        private readonly IAppDbContext _context;

        public AdminUserService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users
                .Where(u => u.Role != UserRole.Admin)
                .ToListAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var messages = _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId);

            _context.Messages.RemoveRange(messages);

            var enrollments = _context.Enrollments
                .Where(e => e.StudentId == userId);

            _context.Enrollments.RemoveRange(enrollments);

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
                _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmailAsync(int userId, string newEmail)
        {
            if (await _context.Users.AnyAsync(u => u.Email == newEmail))
                return false;

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Email = newEmail;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ChangeRoleAsync(int userId, UserRole newRole)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            user.Role = newRole;
            await _context.SaveChangesAsync();
        }
    }
}
