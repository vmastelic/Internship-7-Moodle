using InternshipMoodle.Application.Chat;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class ChatMenu
    {
        private readonly ChatService _chatService;

        public ChatMenu(ChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PRIVATNI CHAT ===");
                Console.WriteLine("1. Nova poruka");
                Console.WriteLine("2. Moji razgovori");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await NewChat();
                        break;
                    case "2":
                        await ExistingChats();
                        break;
                    case "0":
                        return;
                }
            }
        }

        private async Task NewChat()
        {
            var users = await _chatService.GetUsersWithoutChatAsync(UserSession.CurrentUser!.Id);

            Console.Clear();
            if (!users.Any())
            {
                Console.WriteLine("Nema novih korisnika.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{i + 1}. {users[i].Email}");

            Console.Write("Odabir: ");
            if (!int.TryParse(Console.ReadLine(), out int choice)) return;

            await OpenChat(users[choice - 1]);
        }

        private async Task ExistingChats()
        {
            var users = await _chatService.GetChatPartnersAsync(UserSession.CurrentUser!.Id);

            Console.Clear();
            if (!users.Any())
            {
                Console.WriteLine("Nema razgovora.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{i + 1}. {users[i].Email}");

            Console.Write("Odabir: ");
            if (!int.TryParse(Console.ReadLine(), out int choice)) return;

            await OpenChat(users[choice - 1]);
        }

        private async Task OpenChat(Domain.Entities.Users.User partner)
        {
            while (true)
            {
                Console.Clear();
                var messages = await _chatService.GetMessagesAsync(
                    UserSession.CurrentUser!.Id,
                    partner.Id
                );

                foreach (var m in messages)
                {
                    var sender = m.SenderId == UserSession.CurrentUser.Id ? "Ti" : partner.Email;
                    Console.WriteLine($"[{m.CreatedAt:t}] {sender}: {m.Content}");
                }

                Console.WriteLine("\nUpiši poruku (/exit za izlaz):");
                var input = Console.ReadLine();

                if (input == "/exit")
                    return;

                await _chatService.SendMessageAsync(
                    UserSession.CurrentUser.Id,
                    partner.Id,
                    input!
                );
            }
        }
    }
}
