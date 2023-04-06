using Lang.Dictionary.App.Models.Words;

namespace Lang.Dictionary.App.Services.Repositories
{
    public interface IWordRepository
    {
        Task Delete(Guid id, Guid ownerId);
        Task<WordModel> Get(Guid id);
        Task<WordModel[]> Get(Guid[] ids);
        Task<WordModel[]> GetByOwnerId(Guid? ownerId);
        Task Save(WordModel entity);
    }
}