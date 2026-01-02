using InternshipMoodle.Application.Auth;
using InternshipMoodle.Presentation.Session;
using InternshipMoodle.Presentation.Menus;


namespace InternshipMoodle.Presentation.Menus
{
    public class AuthMenu
    {
        private readonly AuthService _authService;

        public AuthMenu(AuthService authService)
        {
            _authService = authService;
        }

        public async Task ShowAsync()
        {
            while (!UserSession.IsAuthenticated)
            {
                Console.Clear();
                Console.WriteLine("=== MOODLE ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registration");
                Console.WriteLine("0. Izlaz");
                Console.Write("Odabir: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await LoginAsync();
                        break;

                    case "2":
                        var registerMenu = new RegisterMenu(_authService);
                        await registerMenu.ShowAsync();
                        break;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Neispravan unos.");
                        Pause();
                        break;
                }
            }
        }

        private async Task LoginAsync()
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===");

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Lozinka: ");
            var password = ReadPassword();

            var result = await _authService.LoginAsync(email!, password);

            if (result.Success)
            {
                UserSession.Login(result.User!);
                Console.WriteLine("\nLogin uspješan!");
                Pause();
            }
            else
            {
                Console.WriteLine($"\nGreška: {result.Error}");
                Pause();
            }
        }
        private static void Pause()
        {
            Console.WriteLine("\nPritisni ENTER za nastavak...");
            Console.ReadLine();
        }

        private static string ReadPassword()
        {
            var password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }
            while (key != ConsoleKey.Enter);

            return password;
        }
    }
}
