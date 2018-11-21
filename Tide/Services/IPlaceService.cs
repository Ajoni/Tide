using System.Collections.Generic;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public interface IPlaceService
    {
        List<Place> GetPlaces();
        Place GetPlace(int id);
        List<Place> AddPlace(PlaceViewModel viewModel);
        List<Place> UpdatePlace(PlaceViewModel viewModel);
        void DeletePlace(int id);
    }
}
