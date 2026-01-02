using InternshipMoodle.Application.Chat;
using InternshipMoodle.Application.Professors;
using InternshipMoodle.Application.Students;
using InternshipMoodle.Domain.Enums;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class MainMenu
    {
        private readonly StudentCourseService _studentCourseService;
        private readonly ProfessorCourseService _professorCourseService;
        private readonly ProfessorCourseManagementService _professorCourseManagementService;
        private readonly ChatService _chatService;

        public MainMenu(StudentCourseService studentCourseService,
            ProfessorCourseService professorCourseService,
            ProfessorCourseManagementService professorCourseManagementService,
            ChatService chatService
            )
        {
            _studentCourseService = studentCourseService;
            _professorCourseService = professorCourseService;
            _professorCourseManagementService = professorCourseManagementService;
            _chatService = chatService;
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
            Console.Write("\nOdabir: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var menu = new StudentCoursesMenu(_studentCourseService);
                    await menu.ShowAsync();
                    break;

                case "2":
                    var chatMenu = new ChatMenu(_chatService);
                    await chatMenu.ShowAsync();
                    break;

                case "0":
                    Logout();
                    break;

                default:
                    Error();
                    break;
            }
        }

        private async Task ShowProfessorMenu()
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
                    var coursesMenu = new ProfessorCoursesMenu(_professorCourseService);
                    await coursesMenu.ShowAsync();

                    break;

                case "2":
                    var manageMenu = new ManageCoursesMenu(_professorCourseManagementService);
                    await manageMenu.ShowAsync();

                    break;

                case "3":
                    var chatMenu = new ChatMenu(_chatService);
                    await chatMenu.ShowAsync();
                    break;

                case "0":
                    Logout();
                    break;

                default:
                    Error();
                    break;
            }
        }

        private async Task ShowAdminMenu()
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
                    var chatMenu = new ChatMenu(_chatService);
                    await chatMenu.ShowAsync();
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
