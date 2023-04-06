using Lang.Dictionary.App.Models;
using Lang.Dictionary.App.Models.Users;
using Lang.Dictionary.App.Services.Repositories;
using Lang.Dictionary.App.Settings;
using Microsoft.Extensions.Options;

namespace Lang.Dictionary.App.Services
{
    public sealed class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly LanguageSettings _languageOptions;

        public UserService(IUserRepository userRepository, IOptions<LanguageSettings> languageOptions)
        {
            _userRepository = userRepository;
            _languageOptions = languageOptions.Value;
        }

        public async Task<Result<UserModel>> GetUser(string email, string password)
        {
            var passwordMD5 = UserModel.GetPasswordMD5(email, password);
            var maybeUser = await _userRepository.GetForLogin(email, passwordMD5);
            if (maybeUser is null)
                return Result<UserModel>.CreateError("Пошта або пароль не вірні");

            return new Result<UserModel>(maybeUser);
        }

        public async Task<Result<UserModel>> CreateUser(string name, string email, string password, int baseLanguageId, int studyLanguageId)
        {
            if (await _userRepository.Exists(email))
                Result<UserModel>.CreateError("User already exists");

            var user = UserModel.Create(name, email, password, new UserSettings(
                _languageOptions.Get(baseLanguageId),
                _languageOptions.Get(studyLanguageId)));

            await _userRepository.Save(user);

            return new Result<UserModel>(user);
        }
    }
}
