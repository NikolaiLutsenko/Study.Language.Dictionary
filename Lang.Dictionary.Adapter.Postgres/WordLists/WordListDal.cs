using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.Words;

namespace Lang.Dictionary.Adapter.Postgres.WordLists
{
    internal class WordListDal
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid? OwnerId { get; set; }

        public IEnumerable<WordToWordListDal> WordsToWordList { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public UserBox? Owner { get; set; }
    }
}
