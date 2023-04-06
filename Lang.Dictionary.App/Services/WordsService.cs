using Lang.Dictionary.App.Models;
using Lang.Dictionary.App.Models.Words;
using Lang.Dictionary.App.Services.Repositories;
using System.Reflection;

namespace Lang.Dictionary.App.Services
{
    public sealed class WordsService
    {
        private readonly IWordRepository _wordRepository;

        public WordsService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task Add(Word from, Word to)
        {
            var newWord = WordModel.Create(from, to, ownerId: null);

            await _wordRepository.Save(newWord);
        }

        public async Task SaveWord(Guid? id, Word from, Word to, Guid ownerId)
        {
            WordModel word;
            if (id.HasValue)
            {
                word = await _wordRepository.Get(id.Value);
                if (word.OwnerId != ownerId)
                    return;

                word.Update(from, to);
            }
            else
            {
                word = WordModel.Create(from, to, ownerId);
            }
                
            await _wordRepository.Save(word);
        }

        public async Task DeleteWord(Guid id, Guid ownerId)
        {
            await _wordRepository.Delete(id, ownerId);
        }

        public async Task<IEnumerable<WordModel>> GetDictionary(Guid? ownerId = null)
        {
            var words = await _wordRepository.GetByOwnerId(ownerId);
            return words;
        }

        public async Task<Result<WordModel>> GetWord(Guid id, Guid? ownerId = null)
        {
            var word = await _wordRepository.Get(id);
            if (word is null || word.OwnerId != ownerId)
                return Result<WordModel>.CreateError("Слова не існує");

            return new Result<WordModel>(word);
        }
    }
}
