using Lang.Dictionary.App.Models.Users;
using Lang.Dictionary.App.Services.Repositories;
using Lang.Dictionary.App.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Lang.Dictionary.Adapter.Postgres.Users
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly LangContext _context;
        private readonly LanguageSettings _languageOptions;

        public UserRepository(LangContext context, IOptions<LanguageSettings> languageOptions)
        {
            _context = context;
            _languageOptions = languageOptions.Value;
        }

        public async Task<UserModel?> GetForLogin(string email, string passwordMD5)
        {
            var json = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                [nameof(UserDal.Email)] = email,
                [nameof(UserDal.PasswordMD5)] = passwordMD5,
            });
            var userDal = await _context.Users.FirstOrDefaultAsync(x => EF.Functions.JsonContains(x.Content, json));

            return ToDomain(userDal);
        }

        public async Task Save(UserModel entity)
        {
            var dal = await _context.Users.FindAsync(entity.UserId);
            if (dal != null)
                _context.Users.Update(ToDal(dal, entity));
            else
                await _context.Users.AddAsync(ToDal(dal, entity));

            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string email)
        {
            email = email.Trim().ToLowerInvariant();

            var exists = await _context.Users.AnyAsync(x => x.Content.Email == email);

            return exists;
        }

        private static UserBox ToDal(UserBox existedDal, UserModel user)
        {
            if (existedDal is null)
                existedDal = new UserBox { Id = user.UserId, Content = new UserDal { UserId = user.UserId } };

            existedDal.Content.Name = user.Name;
            existedDal.Content.Email = user.Email;
            existedDal.Content.PasswordMD5 = user.PasswordMD5;
            existedDal.Content.UserSettings = new UserSettingsDal
            {
                BaseLanguageId = user.UserSettings.BaseLanguage.Id,
                StudyLanguageId = user.UserSettings.StudyLanguage.Id,
            };

            return existedDal;
        }

        private UserModel? ToDomain(UserBox? userBox)
        {
            if (userBox is null)
                return null;

            return UserModel.RestoreState(
                userBox.Id,
                userBox.Content.Name,
                userBox.Content.Email,
                userBox.Content.PasswordMD5,
                new UserSettings(
                    _languageOptions.Get(userBox.Content.UserSettings.BaseLanguageId),
                    _languageOptions.Get(userBox.Content.UserSettings.StudyLanguageId)));
        }
    }
}
