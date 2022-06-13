using AutoMapper;
using Prose.Application.Extensions;
using Prose.Application.Interfaces;
using Prose.Application.ViewModels;
using Prose.Core.Entities;
using Prose.Core.Interfaces.Repository;
using System;
using System.Linq;

namespace Prose.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        IMapper _mapper { get; }
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User AddUser(UserCreateInput userCreateInput)
        {
            return _userRepository.AddUser(new User
            {
                Enable = true,
                Password = userCreateInput.Password.CriptografarSha256(),
                Registration = DateTime.Now,
                Username = userCreateInput.Username
            });
        }

        public User SignIn(UserCreateInput userCreateInput)
        {
            return _userRepository.SignInUser(new User
            {
                Password = userCreateInput.Password.CriptografarSha256(),
                Username = userCreateInput.Username
            });
        }

        public bool ValidUsernameIsUnic(string username)
        {
            return _userRepository.Get(u => u.Username == username).ToList().Count.Equals(0);
        }

        public int GetUserIdByName(string username)
        {
            return _userRepository.Get(u => u.Username == username).FirstOrDefault().UserId;
        }
    }
}
