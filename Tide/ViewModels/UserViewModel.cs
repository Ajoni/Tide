using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tide.Models;

namespace Tide.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public UserViewModel()
        { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public static List<UserViewModel> ToList(List<User> users)
        {
            var viewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                viewModels.Add(new UserViewModel(user));
            }

            return viewModels;
        }
    }
}
