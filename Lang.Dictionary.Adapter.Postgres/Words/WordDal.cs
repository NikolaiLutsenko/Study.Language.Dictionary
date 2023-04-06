using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.WordLists;

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

        public ICollection<WordToWordListDal>? WordToWordLists { get; set; }
    }

    internal class TranslatedWordDal
    {
        public int LanguageId { get; set; }

        public string Value { get; set; } = null!;

        public string LanguageName { get; set; } = null!;
    }
}
