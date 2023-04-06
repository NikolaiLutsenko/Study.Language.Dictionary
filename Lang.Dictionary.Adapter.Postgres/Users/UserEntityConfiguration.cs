using Lang.Dictionary.Adapter.Postgres.Words;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lang.Dictionary.Adapter.Postgres.Users
{
    internal class UserEntityConfiguration : BaseEntityBoxConfiguration<UserBox, Guid, UserDal>
    {
        protected override string TableName => "user";
    }
}
