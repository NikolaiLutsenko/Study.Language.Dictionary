namespace Lang.Dictionary.Adapter.Postgres
{
    public abstract class EntityBox<TKey, TContent>
        where TContent : class
    {
        public TKey Id { get; set; }

        public TContent Content { get; set; } = null!;
    }
}
