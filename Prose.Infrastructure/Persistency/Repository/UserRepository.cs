using Prose.Core.Entities;
using Prose.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prose.Infrastructure.Persistency.Repository
{
    public class UserRepository : IUserRepository
    {
        private ProseDbContext db = new ProseDbContext();

        public User SignInUser(User user)
        {
           return db.User.FirstOrDefault(a => string.Compare(user.Username ,a.Username) == 0
                                        && user.Password.Equals(a.Password)
                                        && a.Enable);
        }

        public User AddUser(User userCreate)
        {
            db.User.Add(userCreate);
            db.SaveChanges();
            return userCreate;
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> filter)
        {
            return db.User.Where(filter);
        }
    }
}
