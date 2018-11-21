using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Tide.Services;
using Tide.ViewModels;

namespace Tide.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }


        [HttpPost]
        [Route("places")]
        public IActionResult Places([DataSourceRequest]DataSourceRequest request)
        {
            try
            {
                var places = _placeService.GetPlaces();
                return Ok(PlaceViewModel.ToList(places).ToDataSourceResult(request));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("place/{id}")]
        public IActionResult GetPlace(int id)
        {
            try
            {
                var place = _placeService.GetPlace(id);
                return Ok(new PlaceViewModel(place));
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
        [Route("place/add")]
        public IActionResult Add([DataSourceRequest]DataSourceRequest request, PlaceViewModel viewModel)
        {
            try
            {
                return Ok(_placeService.AddPlace(viewModel).ToDataSourceResult(request));
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
        [Route("place/update")]
        public IActionResult Update([DataSourceRequest]DataSourceRequest request, PlaceViewModel viewModel)
        {
            try
            {
                return Ok(_placeService.UpdatePlace(viewModel).ToDataSourceResult(request));
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
        [Route("place/delete")]
        public IActionResult Delete([DataSourceRequest]DataSourceRequest request, PlaceViewModel place)
        {
            try
            {
                _placeService.DeletePlace(place.Id);
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
