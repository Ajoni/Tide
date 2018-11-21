using System;
using System.Collections.Generic;
using System.Linq;
using Tide.Data;
using Tide.Helpers;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public List<User> AddUser(UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var salt = AuthHelper.CreateSalt(128);
            var list = new List<User>();
            var user = new User(viewModel.Email, AuthHelper.CreateHash(viewModel.Password, salt), salt, viewModel.FirstName, viewModel.LastName, viewModel.FavPlaceId);
            list.Add(user);
            _context.Users.Add(user);
            _context.SaveChanges();

            return list;
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"User with id: {id} not found");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"User with id: {id} not found");

            return user;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> UpdateUser(UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var user = _context.Users.SingleOrDefault(u => u.Id == viewModel.Id);
            var list = new List<User>();
            if (user == null)
                throw new ArgumentException($"User with id: {viewModel.Id} not found");

            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.FavPlaceId = viewModel.FavPlaceId;
            if (!string.IsNullOrEmpty(viewModel.Password))
                user.PasswordHash = AuthHelper.CreateHash(viewModel.Password, user.PasswordSalt);

            list.Add(user);
            _context.SaveChanges();

            return list;
        }

    }
}
