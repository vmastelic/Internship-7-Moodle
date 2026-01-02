using InternshipMoodle.Application.Professors;
using InternshipMoodle.Presentation.Session;

namespace InternshipMoodle.Presentation.Menus
{
    public class ManageCoursesMenu
    {
        private readonly ProfessorCourseManagementService _service;

        public ManageCoursesMenu(ProfessorCourseManagementService service)
        {
            _service = service;
        }

        public async Task ShowAsync()
        {
            var professorId = UserSession.CurrentUser!.Id;
            var courses = await _service.GetMyCoursesAsync(professorId);

            Console.Clear();
            Console.WriteLine("=== UPRAVLJANJE KOLEGIJIMA ===");

            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i + 1}. {courses[i].Name}");

            Console.WriteLine("0. Nazad");
            Console.Write("Odabir: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > courses.Count)
                return;

            var course = courses[choice - 1];
            await ManageSingleCourse(course.Id);
        }

        private async Task ManageSingleCourse(int courseId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Dodaj studenta");
                Console.WriteLine("2. Objavi obavijest");
                Console.WriteLine("3. Dodaj materijal");
                Console.WriteLine("0. Nazad");
                Console.Write("Odabir: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await AddStudent(courseId);
                        break;
                    case "2":
                        await AddNotification(courseId);
                        break;
                    case "3":
                        await AddMaterial(courseId);
                        break;
                    case "0":
                        return;
                }
            }
        }

        private async Task AddStudent(int courseId)
        {
            var students = await _service.GetAllStudentsAsync();

            Console.Clear();
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}. {students[i].Email}");

            Console.Write("Odaberi studenta: ");
            if (!int.TryParse(Console.ReadLine(), out int choice)) return;

            var success = await _service.AddStudentAsync(courseId, students[choice - 1].Id);
            Console.WriteLine(success ? "Student dodan." : "Student je već upisan.");
            Console.ReadLine();
        }

        private async Task AddNotification(int courseId)
        {
            Console.Write("Naslov: ");
            var title = Console.ReadLine();
            Console.Write("Tekst: ");
            var content = Console.ReadLine();

            await _service.AddNotificationAsync(courseId, UserSession.CurrentUser!.Id, title!, content!);
            Console.WriteLine("Obavijest dodana.");
            Console.ReadLine();
        }

        private async Task AddMaterial(int courseId)
        {
            Console.Write("Naziv: ");
            var name = Console.ReadLine();
            Console.Write("URL: ");
            var url = Console.ReadLine();

            await _service.AddMaterialAsync(courseId, name!, url!);
            Console.WriteLine("Materijal dodan.");
            Console.ReadLine();
        }
    }
}
