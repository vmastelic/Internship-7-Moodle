using InternshipMoodle.Application.Professors;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class ProfessorCoursesMenu
    {
        private readonly ProfessorCourseService _courseService;

        public ProfessorCoursesMenu(ProfessorCourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MOJI KOLEGIJI (PROFESOR) ===");

                var professorId = UserSession.CurrentUser!.Id;
                var courses = await _courseService.GetMyCoursesAsync(professorId);

                if (!courses.Any())
                {
                    Console.WriteLine("Nemaš dodijeljenih kolegija.");
                    Pause();
                    return;
                }

                for (int i = 0; i < courses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {courses[i].Name}");
                }

                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                var input = Console.ReadLine();

                if (input == "0")
                    return;

                if (int.TryParse(input, out int choice)
                    && choice > 0
                    && choice <= courses.Count)
                {
                    ShowCourseScreen(courses[choice - 1]);
                }
                else
                {
                    Error();
                }
            }
        }

        private void ShowCourseScreen(Domain.Entities.Courses.Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== {course.Name} ===");

            Console.WriteLine("\nStudenti:");
            foreach (var e in course.Enrollments
                .OrderBy(e => e.Student.Email))
            {
                Console.WriteLine($"- {e.Student.Email}");
            }

            Console.WriteLine("\nObavijesti:");
            foreach (var n in course.Notifications
                .OrderByDescending(n => n.CreatedAt))
            {
                Console.WriteLine($"- {n.Title} ({n.CreatedAt:g})");
            }

            Console.WriteLine("\nMaterijali:");
            foreach (var m in course.Materials
                .OrderByDescending(m => m.CreatedAt))
            {
                Console.WriteLine($"- {m.Name} ({m.Url})");
            }

            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nENTER za povratak...");
            Console.ReadLine();
        }

        private static void Error()
        {
            Console.WriteLine("Neispravan unos.");
            Pause();
        }
    }
}
