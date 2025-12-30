using InternshipMoodle.Application.Students;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class StudentCoursesMenu
    {
        private readonly StudentCourseService _courseService;

        public StudentCoursesMenu(StudentCourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task ShowAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MOJI KOLEGIJI ===");

                var studentId = UserSession.CurrentUser!.Id;
                var courses = await _courseService.GetMyCoursesAsync(studentId);

                if (!courses.Any())
                {
                    Console.WriteLine("Nisi upisan ni na jedan kolegij.");
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
                    await ShowCourseScreen(courses[choice - 1]);
                }
                else
                {
                    Error();
                }
            }
        }

        private async Task ShowCourseScreen(Domain.Entities.Courses.Course course)
        {
            Console.Clear();
            Console.WriteLine($"=== {course.Name} ===");

            Console.WriteLine("\n[Obavijesti]");
            if (course.Notifications.Any())
            {
                foreach (var n in course.Notifications.OrderBy(n => n.CreatedAt))
                {
                    Console.WriteLine($"- {n.Title}: {n.Content}");
                }
            }
            else
            {
                Console.WriteLine("Nema obavijesti.");
            }

            Console.WriteLine("\n[Materijali]");
            if (course.Materials.Any())
            {
                foreach (var m in course.Materials.OrderBy(m => m.CreatedAt))
                {
                    Console.WriteLine($"- {m.Name} ({m.Url})");
                }
            }
            else
            {
                Console.WriteLine("Nema materijala.");
            }

            Pause();
            await Task.CompletedTask;
        }

        private static void Pause()
        {
            Console.WriteLine("\nPritisni ENTER za povratak...");
            Console.ReadLine();
        }

        private static void Error()
        {
            Console.WriteLine("Neispravan unos.");
            Pause();
        }
    }
}
