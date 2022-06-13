using Prose.Application.ViewModels;
using Prose.Core.Entities;

namespace Prose.Application.Interfaces
{
    public interface IUserService
    {
        User AddUser(UserCreateInput userCreateInput);
        User SignIn(UserCreateInput userCreateInput);
        bool ValidUsernameIsUnic(string username);
        int GetUserIdByName(string username);
    }
}
