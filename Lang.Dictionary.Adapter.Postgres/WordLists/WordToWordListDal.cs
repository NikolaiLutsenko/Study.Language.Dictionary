using Lang.Dictionary.Adapter.Postgres.Words;

namespace Lang.Dictionary.Adapter.Postgres.WordLists
{
    internal class WordToWordListDal
    {
        public Guid WordId { get; set; }

        public Guid WordListId { get; set; }

        public WordDal? Word { get; set; }

        public WordListDal? WordList { get; set; }
    }
}
