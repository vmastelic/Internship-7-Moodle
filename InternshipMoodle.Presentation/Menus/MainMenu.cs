using InternshipMoodle.Domain.Enums;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class MainMenu
    {
        public async Task ShowAsync()
        {
            while (UserSession.IsAuthenticated)
            {
                Console.Clear();

                var role = UserSession.CurrentUser!.Role;

                Console.WriteLine($"=== GLAVNI MENI ({role}) ===");

                switch (role)
                {
                    case UserRole.Student:
                        await ShowStudentMenu();
                        break;

                    case UserRole.Professor:
                        await ShowProfessorMenu();
                        break;

                    case UserRole.Admin:
                        await ShowAdminMenu();
                        break;
                }
            }
        }


        private async Task ShowStudentMenu()
        {
            Console.WriteLine("1. Moji kolegiji");
            Console.WriteLine("2. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("Odabir: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Placeholder("Moji kolegiji (Student)");
                    break;

                case "2":
                    Placeholder("Privatni chat");
                    break;

                case "0":
                    Logout();
                    break;

                default:
                    Error();
                    break;
            }

            await Task.CompletedTask;
        }


        private async Task ShowProfessorMenu()
        {
            Console.WriteLine("1. Moji kolegiji");
            Console.WriteLine("2. Upravljanje kolegijima");
            Console.WriteLine("3. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("Odabir: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Placeholder("Moji kolegiji (Profesor)");
                    break;

                case "2":
                    Placeholder("Upravljanje kolegijima");
                    break;

                case "3":
                    Placeholder("Privatni chat");
                    break;

                case "0":
                    Logout();
                    break;

                default:
                    Error();
                    break;
            }

            await Task.CompletedTask;
        }


        private async Task ShowAdminMenu()
        {
            Console.WriteLine("1. Upravljanje korisnicima");
            Console.WriteLine("2. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("Odabir: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Placeholder("Upravljanje korisnicima");
                    break;

                case "2":
                    Placeholder("Privatni chat");
                    break;

                case "0":
                    Logout();
                    break;

                default:
                    Error();
                    break;
            }

            await Task.CompletedTask;
        }


        private static void Logout()
        {
            UserSession.Logout();
            Console.WriteLine("Odjavljen si.");
            Pause();
        }

        private static void Placeholder(string feature)
        {
            Console.WriteLine($"\n[{feature}]");
            Pause();
        }

        private static void Error()
        {
            Console.WriteLine("Neispravan unos.");
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPritisni ENTER za nastavak...");
            Console.ReadLine();
        }
    }
}
