using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tide.Data;
using Tide.Helpers;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var salt = AuthHelper.CreateSalt(128);
            var user = new User(viewModel.Email, AuthHelper.CreateHash(viewModel.Password, salt), salt, viewModel.FirstName, viewModel.LastName);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            if(id < 1)
                throw new ArgumentException();

            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"User with id: {id} not found");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            if (id < 1)
                throw new ArgumentException();

            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"User with id: {id} not found");

            return user;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void UpdateUser(UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var user = _context.Users.SingleOrDefault(u => u.Id == viewModel.Id);
            if (user == null)
                throw new ArgumentException($"User with id: {viewModel.Id} not found");

            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            if (!string.IsNullOrEmpty(viewModel.Password))
                user.PasswordHash = AuthHelper.CreateHash(viewModel.Password, user.PasswordSalt);

            _context.SaveChanges();
        }
    }
}
