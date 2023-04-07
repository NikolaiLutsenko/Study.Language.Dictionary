using Lang.Dictionary.App.Models.WordLists;

namespace Lang.Dictionary.App.Services.Repositories
{
    public interface IWordListRepository
    {
        Task<WordListModel[]> GetLists(Guid? ownerId);
    }
}
