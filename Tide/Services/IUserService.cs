using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(UserViewModel viewModel);
        void UpdateUser(UserViewModel viewModel);
        void DeleteUser(int id);
    }
}
