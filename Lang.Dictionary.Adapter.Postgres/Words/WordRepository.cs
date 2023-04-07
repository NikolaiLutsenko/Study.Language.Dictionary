using Lang.Dictionary.App.Models.Words;
using Lang.Dictionary.App.Services.Repositories;
using Lang.Dictionary.App.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lang.Dictionary.Adapter.Postgres.Words
{
    internal class WordRepository : IWordRepository
    {
        private LangContext _context;
        private LanguageSettings _languageOptions;

        public WordRepository(LangContext context, IOptions<LanguageSettings> languageOptions)
        {
            _context = context;
            _languageOptions = languageOptions.Value;
        }

        public async Task<WordModel> Get(Guid id)
        {
            var word = await _context.Words.FindAsync(id);
            return ToDomain(word);
        }

        public async Task<WordModel[]> Get(Guid[] ids)
        {
            var dals = await _context.Words.Where(x => ids.Contains(x.Id)).ToArrayAsync();

            return dals.Select(dal => ToDomain(dal)).ToArray();
        }

        public async Task Save(WordModel entity)
        {
            var dal = await _context.Words
                .FirstOrDefaultAsync(x => (x.Id == entity.Id || entity.From.Value == x.From.Value)
                    && x.OwnerId == entity.OwnerId);

            if (dal != null)
                _context.Words.Update(ToDal(dal, entity));
            else
                await _context.Words.AddAsync(ToDal(dal, entity));

            await _context.SaveChangesAsync();
        }

        public async Task<WordModel[]> GetByOwnerId(Guid? ownerId)
        {
            var dals = await _context.Users
                .Include(user => user.Words)
                .Where(user => user.Id == ownerId)
                .SelectMany(x => x.Words)
                .ToArrayAsync();

            return dals.Select(dal => ToDomain(dal)).ToArray();
        }

        public async Task Delete(Guid id, Guid ownerId)
        {
            var dal = await _context.Words.FindAsync(id);
            if (dal is null || dal.OwnerId != ownerId)
                return;

            _context.Words.Remove(dal);

            await _context.SaveChangesAsync();
        }

        private WordModel ToDomain(WordDal dal)
        {
            return WordModel.RestoreState(
                dal.Id,
                ToDomain(dal.From),
                ToDomain(dal.To),
                dal.OwnerId,
                dal.CreatedAt);

            Word ToDomain(TranslatedWordDal translatedWord)
            {
                return new Word(
                    _languageOptions.Get(translatedWord.LanguageId),
                    translatedWord.Value);
            }
        }

        private WordDal ToDal(WordDal existedDal, WordModel model)
        {
            if (existedDal == null)
                existedDal = new WordDal { Id = model.Id };

            existedDal.From = ToDal(model.From);
            existedDal.To = ToDal(model.To);
            existedDal.CreatedAt = model.CreatedAt;
            existedDal.OwnerId = model.OwnerId;

            return existedDal;

            TranslatedWordDal ToDal(Word word)
            {
                return new TranslatedWordDal
                {
                    LanguageId = word.Language.Id,
                    LanguageName = word.Language.Name,
                    Value = word.Value,
                };
            }
        }
    }
}
