using InternshipMoodle.Application.Common;
using InternshipMoodle.Application.Common.Validation;
using InternshipMoodle.Domain.Entities.Users;
using InternshipMoodle.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InternshipMoodle.Application.Auth
{
    public class AuthService
    {
        private readonly IAppDbContext _context;

        public AuthService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return AuthResult.Fail("Ne postoji korisnik s tim emailom.");

            if (user.PasswordHash != password)
                return AuthResult.Fail("Pogrešna lozinka.");

            return AuthResult.Ok(user);
        }

        public async Task<AuthResult> RegisterAsync(
            string email,
            string password,
            string confirmPassword)
        {
            if (!ValidationHelper.IsValidEmail(email))
                return AuthResult.Fail("Email format nije ispravan.");

            if (await _context.Users.AnyAsync(u => u.Email == email))
                return AuthResult.Fail("Email već postoji.");

            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("Lozinka ne smije biti prazna.");

            if (password != confirmPassword)
                return AuthResult.Fail("Lozinke se ne poklapaju.");

            var user = new User
            {
                Email = email,
                PasswordHash = password, 
                Role = UserRole.Student,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return AuthResult.Ok(user);
        }
    }
}
