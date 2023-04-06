namespace Lang.Dictionary.App.Models.Users
{
    public class UserModel
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string PasswordMD5 { get; private set; } = null!;

        public UserSettings UserSettings { get; private set; } = null!;

        public static string GetPasswordMD5(string email, string password)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password + email.Trim().ToLower());
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static UserModel Create(string name, string email, string password, UserSettings userSettings)
        {
            return new UserModel
            {
                Name = name,
                UserId = Guid.NewGuid(),
                Email = email,
                PasswordMD5 = GetPasswordMD5(email, password),
                UserSettings = userSettings
            };
        }

        public static UserModel RestoreState(Guid userId, string name, string email, string passwordMD5, UserSettings userSettings)
        {
            return new UserModel
            {
                UserId = userId,
                Name = name,
                Email = email,
                PasswordMD5 = passwordMD5,
                UserSettings = userSettings
            };
        }
    }
}
