using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var user = new User(viewModel.Email, AuthHelper.CreateHash(viewModel.Password, salt), salt, viewModel.FirstName, viewModel.LastName);
            list.Add(user);
            _context.Users.Add(user);
            _context.SaveChanges();

            return list;
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
            if (!string.IsNullOrEmpty(viewModel.Password))
                user.PasswordHash = AuthHelper.CreateHash(viewModel.Password, user.PasswordSalt);

            list.Add(user);
            _context.SaveChanges();

            return list;
        }

        public async Task<ObjectResult> Login(LoginViewModel viewModel)
        {
            var user = AuthHelper.Authenticate(viewModel.Email, viewModel.Password, _context);
            if (user == null)
                throw new ArgumentException();

            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var jwtToken = _tokenService.GenerateAccessToken(userClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _context.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken
            });
        }
    }
}
