using Lang.Dictionary.App.Models.WordLists;
using Lang.Dictionary.App.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lang.Dictionary.Adapter.Postgres.WordLists
{
    internal class WordListRepository : IWordListRepository
    {
        private readonly LangContext _context;

        public WordListRepository(LangContext context)
        {
            _context = context;
        }

        public async Task<WordListModel[]> GetLists(Guid? ownerId)
        {
            var dals = await _context.WordsList
                .Include(x => x.WordsToWordList)
                .ThenInclude(wordToWordList => wordToWordList.Word)
                .Where(x => x.OwnerId == ownerId)
                .ToArrayAsync();

            return dals.Select(dal => ToDomain(dal)).ToArray();
        }

        private WordListModel ToDomain(WordListDal dal)
        {
            return WordListModel.RestoreState(dal.Id, dal.Name, dal.OwnerId, dal.CreatedAt);
        }
    }
}
