using InternshipMoodle.Domain.Entities.Users;

namespace InternshipMoodle.Presentation.Session
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsAuthenticated => CurrentUser != null;

        public static void Login(User user)
        {
            CurrentUser = user;
        }
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
