using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tide.Data;
using Tide.Helpers;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AccountService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
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

        public async Task Signup(SignupViewModel viewModel)
        {   
            var user = _context.Users.SingleOrDefault(u => u.Email == viewModel.Email);
            if (user != null)
                throw new ArgumentException();

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(viewModel.Password, salt);
            var newUser = new User(viewModel.Email, passwordHash, salt, viewModel.FirstName, viewModel.LastName, null);
            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();
        }
    }
}
