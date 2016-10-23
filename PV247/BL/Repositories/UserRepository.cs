using System.Diagnostics;
using System.Linq;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    // Will probably be removed if Identity is used
    public class UserRepository : EntityFrameworkRepository<User, int>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider) { }

        public User GetUserByEmail(string email)
        {
            var users = Context.Set<User>();
            var user = users.FirstOrDefault(usr => usr.Email.Equals(email));
            if (user == null)
            {
                Debug.WriteLine($"User with email {email} does not exists in the DB!");
            }
            return user;
        }
    }
}
