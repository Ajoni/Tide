using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tide.Services;
using Tide.ViewModels;

namespace Tide.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("users")]
        public IActionResult Users([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(UserViewModel.ToList(users).ToDataSourceResult(request));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return Ok(new UserViewModel(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("user/add")]
        public IActionResult Add([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            try
            {
                return Ok(_userService.AddUser(viewModel).ToDataSourceResult(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("user/update")]
        public IActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel viewModel)
        {
            try
            {
                return Ok(_userService.UpdateUser(viewModel).ToDataSourceResult(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("user/delete")]
        public IActionResult Delete([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            try
            {
                _userService.DeleteUser(user.Id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}