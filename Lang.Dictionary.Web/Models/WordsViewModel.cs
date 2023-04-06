using Lang.Dictionary.App.Models.Words;

namespace Lang.Dictionary.Web.Models
{
    public class WordsViewModel
    {
        public IEnumerable<WordModel> Words { get; set; } = null!;
    }
}
