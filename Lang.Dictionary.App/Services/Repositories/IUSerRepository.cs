using Lang.Dictionary.App.Models.Users;

namespace Lang.Dictionary.App.Services.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetForLogin(string email, string passwordMD5);

        Task Save(UserModel entity);

        Task<bool> Exists(string email);
    }
}
