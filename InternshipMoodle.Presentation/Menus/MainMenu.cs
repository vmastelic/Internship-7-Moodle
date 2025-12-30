using InternshipMoodle.Application.Students;
using InternshipMoodle.Domain.Enums;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly StudentCourseService _studentCourseService;

        public MainMenu(StudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        public async Task ShowAsync()
        {
            while (UserSession.IsAuthenticated)
            {
                Console.Clear();

                var role = UserSession.CurrentUser!.Role;
                Console.WriteLine($"=== GLAVNI MENI ({role}) ===\n");

                switch (role)
                {
                    case UserRole.Student:
                        await ShowStudentMenu();
                        break;

                    case UserRole.Professor:
                        ShowProfessorMenu();
                        break;

                    case UserRole.Admin:
                        ShowAdminMenu();
                        break;
                }
            }
        }

        private async Task ShowStudentMenu()
        {
            Console.WriteLine("1. Moji kolegiji");
            Console.WriteLine("2. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("\nOdabir: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var menu = new StudentCoursesMenu(_studentCourseService);
                    await menu.ShowAsync();
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
        }

        private void ShowProfessorMenu()
        {
            Console.WriteLine("1. Moji kolegiji");
            Console.WriteLine("2. Upravljanje kolegijima");
            Console.WriteLine("3. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("\nOdabir: ");

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
        }

        private void ShowAdminMenu()
        {
            Console.WriteLine("1. Upravljanje korisnicima");
            Console.WriteLine("2. Privatni chat");
            Console.WriteLine("0. Odjava");
            Console.Write("\nOdabir: ");

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
        }

        private static void Logout()
        {
            UserSession.Logout();
            Console.WriteLine("\nOdjavljen si.");
            Pause();
        }

        private static void Placeholder(string feature)
        {
            Console.WriteLine($"\n[{feature}] – dolazi uskoro 🚧");
            Pause();
        }

        private static void Error()
        {
            Console.WriteLine("\nNeispravan unos.");
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPritisni ENTER za nastavak...");
            Console.ReadLine();
        }
    }
}
