using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        [Route("users")]
        public IActionResult Users()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(UserViewModel.ToList(users));
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
        public IActionResult Add([FromBody]UserViewModel viewModel)
        {
            try
            {
                _userService.AddUser(viewModel);
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

        [HttpPost]
        [Route("user/update")]
        public IActionResult Update([FromBody]UserViewModel viewModel)
        {
            try
            {
                _userService.UpdateUser(viewModel);
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

        [HttpDelete]
        [Route("user/delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteUser(id);
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