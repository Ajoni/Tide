using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tide.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required] public string Email { get; set; }
        [Required] public byte[] PasswordHash { get; set; }
        [Required] public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }


        public User()
        {
        }

        public User(string email, byte[] passwordHash, byte[] passwordSalt,
            string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
