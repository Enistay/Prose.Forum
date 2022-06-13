using Prose.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Prose.Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        User AddUser(User userCreate);
        User SignInUser(User user);
        IEnumerable<User> Get(Expression<Func<User, bool>> filter);
    }
}
