using Lang.Dictionary.App.Settings;

namespace Lang.Dictionary.App.Models.Users
{
    public record UserSettings(LanguageDefinition BaseLanguage, LanguageDefinition StudyLanguage);
}
