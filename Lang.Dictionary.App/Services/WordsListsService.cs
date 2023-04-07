using Lang.Dictionary.App.Models.WordLists;
using Lang.Dictionary.App.Services.Repositories;

namespace Lang.Dictionary.App.Services
{
    public sealed class WordsListsService
    {
        private readonly IWordListRepository _wordListRepository;

        public WordsListsService(IWordListRepository wordListRepository)
        {
            _wordListRepository = wordListRepository;
        }

        public Task<WordListModel[]> GetLists(Guid? ownerId)
        {
            return _wordListRepository.GetLists(ownerId);
        }
    }
}
