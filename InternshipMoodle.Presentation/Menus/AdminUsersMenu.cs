using InternshipMoodle.Application.Admin;
using InternshipMoodle.Domain.Enums;

namespace InternshipMoodle.Presentation.Menus
{
    public class AdminUsersMenu
    {
        private readonly AdminUserService _service;

        public AdminUsersMenu(AdminUserService service)
        {
            _service = service;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== UPRAVLJANJE KORISNICIMA ===");
                Console.WriteLine("1. Brisanje korisnika");
                Console.WriteLine("2. Uredi email");
                Console.WriteLine("3. Promijeni rolu");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await DeleteUser();
                        break;
                    case "2":
                        await UpdateEmail();
                        break;
                    case "3":
                        await ChangeRole();
                        break;
                    case "0":
                        return;
                }
            }
        }

        private async Task<List<Domain.Entities.Users.User>> PickUser()
        {
            var users = await _service.GetUsersAsync();

            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{i + 1}. {users[i].Email} ({users[i].Role})");

            Console.Write("Odabir: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
                return [];

            return choice > 0 && choice <= users.Count
                ? new() { users[choice - 1] }
                : [];
        }

        private async Task DeleteUser()
        {
            Console.Clear();
            var selected = await PickUser();
            if (!selected.Any()) return;

            await _service.DeleteUserAsync(selected[0].Id);
            Console.WriteLine("Korisnik obrisan.");
            Console.ReadLine();
        }

        private async Task UpdateEmail()
        {
            Console.Clear();
            var selected = await PickUser();
            if (!selected.Any()) return;

            Console.Write("Novi email: ");
            var email = Console.ReadLine();

            var success = await _service.UpdateEmailAsync(selected[0].Id, email!);
            Console.WriteLine(success ? "Email promijenjen." : "Email već postoji.");
            Console.ReadLine();
        }

        private async Task ChangeRole()
        {
            Console.Clear();
            var selected = await PickUser();
            if (!selected.Any()) return;

            var user = selected[0];
            var newRole = user.Role == UserRole.Student
                ? UserRole.Professor
                : UserRole.Student;

            await _service.ChangeRoleAsync(user.Id, newRole);
            Console.WriteLine($"Nova rola: {newRole}");
            Console.ReadLine();
        }
    }
}
