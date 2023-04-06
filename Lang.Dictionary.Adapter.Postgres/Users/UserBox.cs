using Lang.Dictionary.Adapter.Postgres.Words;

namespace Lang.Dictionary.Adapter.Postgres.Users
{
    internal class UserBox : EntityBox<Guid, UserDal>
    {
        public IEnumerable<WordDal> Words { get; set; }
    }

    internal class UserDal
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordMD5 { get; set; } = null!;

        public UserSettingsDal UserSettings { get; set; } = null!;
    }

    internal class UserSettingsDal
    {
        public int BaseLanguageId { get; set; }

        public int StudyLanguageId { get; set; }
    }
}
