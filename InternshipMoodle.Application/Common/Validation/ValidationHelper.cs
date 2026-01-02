using System.Text.RegularExpressions;

namespace InternshipMoodle.Application.Common.Validation
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@]{1,}@[^@]{2,}\.[^@]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
