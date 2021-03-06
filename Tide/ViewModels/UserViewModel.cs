﻿using System.Collections.Generic;
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
        public int? FavPlaceId { get; set; }

        public UserViewModel()
        { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FavPlaceId = user.FavPlaceId;
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
