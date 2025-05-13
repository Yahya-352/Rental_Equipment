namespace myproject_Library
{
    public class CurrentUserService
    {
        private static int? _currentUserId;
        private static string _currentUsername;

        public static void SetCurrentUser(int userId, string username)
        {
            _currentUserId = userId;
            _currentUsername = username;
        }

        public static void ClearCurrentUser()
        {
            _currentUserId = null;
            _currentUsername = null;
        }

        public static int? GetCurrentUserId()
        {
            return _currentUserId;
        }

        public static string GetCurrentUsername()
        {
            return _currentUsername;
        }
    }
}
