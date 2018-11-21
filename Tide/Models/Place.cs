using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tide.Models
{
    public class Place
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public Place()
        {
        }

        public Place(string name)
        {
            Name = name;
        }
    }
}
