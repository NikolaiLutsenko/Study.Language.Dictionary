namespace Lang.Dictionary.App.Models.WordLists
{
    public sealed class WordListModel
    {
        private WordListModel() { }

        public Guid Id { get; init; }

        public string Name { get; private set; }

        public Guid? OwnerId { get; private set; }

        public DateTimeOffset CreatedAt { get; init; }

        public static WordListModel Create(string name, Guid? ownerId)
        {
            return new WordListModel { Id = Guid.NewGuid(), Name = name, OwnerId = ownerId, CreatedAt = DateTimeOffset.UtcNow };
        }

        public static WordListModel RestoreState(Guid id, string name, Guid? ownerId, DateTimeOffset createdAt)
        {
            return new WordListModel
            {
                Id = id,
                Name = name,
                OwnerId = ownerId,
                CreatedAt = createdAt,
            };
        }
    }
}
