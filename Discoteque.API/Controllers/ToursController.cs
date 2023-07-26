using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _toursService;

        public ToursController(ITourService tourService)
        {
            _toursService = tourService;
        }

        [HttpGet]
        [Route("GetTours")]
        public async Task<IActionResult> GetTours(bool areReferencesLoaded = false)
        {
            var tours = await _toursService.GetToursAsync(areReferencesLoaded);
            return Ok(tours);
        }

        [HttpGet]
        [Route("GetToursById")]
        public async Task<IActionResult> GetById(int id)
        {
            var tour = await _toursService.GetById(id);
            return Ok(tour);
        }

        [HttpGet]
        [Route("GetToursByAlbumName")]
        public async Task<IActionResult> GetToursByArtistName(string artist)
        {
            var tours = await _toursService.GetToursByArtist(artist);
            return tours.Any() ? Ok(tours) : StatusCode(StatusCodes.Status404NotFound, "There was no tours by that name");
        }

        [HttpPost]
        [Route("CreateTourAsync")]
        public async Task<IActionResult> CreateTourAsync(Tour tour)
        {
            var result = await _toursService.CreateTour(tour);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateTourAsync")]
        public async Task<IActionResult> UpdateTourAsync(Tour tour)
        {
            var result = await _toursService.UpdateTour(tour);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteTour")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await _toursService.DeleteById(id);
            return Ok();
        }
        

    }
}