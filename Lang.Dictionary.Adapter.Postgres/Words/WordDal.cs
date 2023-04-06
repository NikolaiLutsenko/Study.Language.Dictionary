using Lang.Dictionary.Adapter.Postgres.Users;

namespace Lang.Dictionary.Adapter.Postgres.Words
{
    internal class WordDal
    {
        public Guid Id { get; set; }

        public TranslatedWordDal From { get; set; } = null!;

        public TranslatedWordDal To { get; set; } = null!;

        public Guid? OwnerId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public UserBox? Owner { get; set; }
    }

    internal class TranslatedWordDal
    {
        public int LanguageId { get; set; }

        public string Value { get; set; } = null!;

        public string LanguageName { get; set; } = null!;
    }
}
