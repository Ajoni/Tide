using System;
using System.Collections.Generic;
using System.Linq;
using Tide.Data;
using Tide.Models;
using Tide.ViewModels;

namespace Tide.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationDbContext _context;

        public PlaceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Place> AddPlace(PlaceViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var list = new List<Place>();
            var place = new Place(viewModel.Name);
            list.Add(place);
            _context.Places.Add(place);
            _context.SaveChanges();

            return list;
        }

        public void DeletePlace(int id)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);
            if (place == null)
                throw new ArgumentException($"Place with id: {id} not found");

            _context.Places.Remove(place);
            _context.SaveChanges();
        }

        public Place GetPlace(int id)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);
            if (place == null)
                throw new ArgumentException($"Place with id: {id} not found");

            return place;
        }

        public List<Place> GetPlaces()
        {
            return _context.Places.ToList();
        }

        public List<Place> UpdatePlace(PlaceViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var place = _context.Places.SingleOrDefault(p => p.Id == viewModel.Id);
            var list = new List<Place>();
            if (place == null)
                throw new ArgumentException($"Place with id: {viewModel.Id} not found");

            place.Name = viewModel.Name;
            list.Add(place);
            _context.SaveChanges();

            return list;
        }
    }
}
