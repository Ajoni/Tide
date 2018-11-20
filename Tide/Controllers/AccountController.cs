using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tide.Services;
using Tide.ViewModels;

namespace Tide.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            try
            {
                var result = await _accountService.Login(viewModel);
                return result;
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel viewModel)
        {
            try
            {
                await _accountService.Signup(viewModel);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}