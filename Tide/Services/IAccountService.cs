using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tide.ViewModels;

namespace Tide.Services
{
    public interface IAccountService
    {
        Task<ObjectResult> Login(LoginViewModel viewModel);
        Task Signup(SignupViewModel viewModel);

    }
}
