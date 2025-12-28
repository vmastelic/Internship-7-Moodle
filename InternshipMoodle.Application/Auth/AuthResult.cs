using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Application.Auth
{
    public class AuthResult
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }
        public User? User { get; private set; }

        private AuthResult() { }

        public static AuthResult Fail(string error)
        {
            return new AuthResult
            {
                Success = false,
                Error = error
            };
        }

        public static AuthResult Ok(User user)
        {
            return new AuthResult
            {
                Success = true,
                User = user
            };
        }
    }
}
