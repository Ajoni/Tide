using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tide.Models;

namespace Tide.ViewModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlaceViewModel()
        { }

        public PlaceViewModel(Place place)
        {
            Id = place.Id;
            Name = place.Name;
        }

        public static List<PlaceViewModel> ToList(List<Place> places)
        {
            var viewModels = new List<PlaceViewModel>();
            foreach (var place in places)
            {
                viewModels.Add(new PlaceViewModel(place));
            }

            return viewModels;
        }
    }
}
