using Lang.Dictionary.App.Settings;

namespace Lang.Dictionary.App.Models.Words
{
    public sealed class WordModel
    {
        private WordModel() { }

        public Guid Id { get; private set; }

        public Word From { get; private set; } = null!;

        public Word To { get; private set; } = null!;

        public Guid? OwnerId { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public static WordModel Create(Word from, Word to, Guid? ownerId)
        {
            return new WordModel
            {
                Id = Guid.NewGuid(),
                From = from,
                To = to,
                OwnerId = ownerId,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        public static WordModel RestoreState(Guid id, Word from, Word to, Guid? ownerId, DateTimeOffset createdAt)
        {
            return new WordModel
            {
                Id = id,
                From = from,
                To = to,
                OwnerId = ownerId,
                CreatedAt = createdAt
            };
        }

        public void Update(Word from, Word to)
        {
            From = from;
            To = to;
        }
    }

    public record Word(LanguageDefinition Language, string Value);
}