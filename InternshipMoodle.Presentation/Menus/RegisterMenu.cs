using InternshipMoodle.Application.Auth;

namespace InternshipMoodle.Presentation.Menus
{
    public class RegisterMenu
    {
        private readonly AuthService _authService;

        public RegisterMenu(AuthService authService)
        {
            _authService = authService;
        }

        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRACIJA ===");

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Lozinka: ");
            var password = Console.ReadLine();

            Console.Write("Potvrdi lozinku: ");
            var confirmPassword = Console.ReadLine();

            var captcha = GenerateCaptcha();
            Console.WriteLine($"Captcha: {captcha}");
            Console.Write("Upiši captcha: ");
            var captchaInput = Console.ReadLine();

            if (captchaInput != captcha)
            {
                Console.WriteLine("Captcha nije točna.");
                Pause();
                return;
            }

            var result = await _authService.RegisterAsync(
                email!,
                password!,
                confirmPassword!
            );

            if (!result.Success)
            {
                Console.WriteLine(result.Error);
                Pause();
                return;
            }

            Console.WriteLine("Registracija uspješna! Možeš se prijaviti.");
            Pause();
        }

        private static string GenerateCaptcha()
        {
            var random = new Random();
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            return $"{letters[random.Next(letters.Length)]}{numbers[random.Next(numbers.Length)]}";
        }

        private static void Pause()
        {
            Console.WriteLine("\nPritisni ENTER za nastavak...");
            Console.ReadLine();
        }
    }
}
