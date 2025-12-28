using InternshipMoodle.Application.Common;
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
    }
}
