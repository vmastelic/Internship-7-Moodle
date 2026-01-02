using InternshipMoodle.Application.Common;
using InternshipMoodle.Domain.Entities.Messages;
using InternshipMoodle.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Chat
{
    public class ChatService
    {
        private readonly IAppDbContext _context;

        public ChatService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersWithoutChatAsync(int currentUserId)
        {
            var chattedUserIds = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .Select(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .ToListAsync();

            return await _context.Users
                .Where(u => u.Id != currentUserId && !chattedUserIds.Contains(u.Id))
                .ToListAsync();
        }

        public async Task<List<User>> GetChatPartnersAsync(int currentUserId)
        {
            var partnerIds = await _context.Messages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .Select(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .ToListAsync();

            return await _context.Users
                .Where(u => partnerIds.Contains(u.Id))
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(int user1, int user2)
        {
            return await _context.Messages
                .Where(m =>
                    (m.SenderId == user1 && m.ReceiverId == user2) ||
                    (m.SenderId == user2 && m.ReceiverId == user1))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task SendMessageAsync(int senderId, int receiverId, string content)
        {
            _context.Messages.Add(new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }
    }


}
