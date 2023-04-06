namespace Lang.Dictionary.App.Settings
{
    public sealed class LanguageSettings
    {
        public LanguageDefinition[] Languages { get; set; } = Array.Empty<LanguageDefinition>();

        public const string Section = nameof(LanguageSettings);

        public LanguageDefinition Get(int id) => Languages.First(x => x.Id == id);
    }

    public sealed class LanguageDefinition
    {
        public string Name { get; set; } = null!;

        public int Id { get; set; }
    }
}
