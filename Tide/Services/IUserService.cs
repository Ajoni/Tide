using Microsoft.AspNetCore.Mvc;
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
        List<User> AddUser(UserViewModel viewModel);
        List<User> UpdateUser(UserViewModel viewModel);
        void DeleteUser(int id);

        Task<ObjectResult> Login(LoginViewModel viewModel);
    }
}
