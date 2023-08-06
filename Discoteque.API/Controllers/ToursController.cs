using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            return tours.StatusCode == HttpStatusCode.OK ? Ok(tours) : StatusCode((int)tours.StatusCode, tours);
        }

        [HttpGet]
        [Route("GetToursById")]
        public async Task<IActionResult> GetById(int id)
        {
            var tour = await _toursService.GetById(id);
            return tour.StatusCode == HttpStatusCode.OK ? Ok(tour) : StatusCode((int)tour.StatusCode, tour);        
        }

        [HttpGet]
        [Route("GetToursByArtistName")]
        public async Task<IActionResult> GetToursByArtistName(string artist)
        {
            var tours = await _toursService.GetToursByArtist(artist);
            return tours.StatusCode == HttpStatusCode.OK ? Ok(tours) : StatusCode((int)tours.StatusCode, tours);
        }

        [HttpPost]
        [Route("CreateTourAsync")]
        public async Task<IActionResult> CreateTourAsync(Tour tour)
        {
            var result = await _toursService.CreateTour(tour);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateTourAsync")]
        public async Task<IActionResult> UpdateTourAsync(Tour tour)
        {
            var result = await _toursService.UpdateTour(tour);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteTour")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            await _toursService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        [Route("GetToursByYear")]
        public async Task<IActionResult> GetToursByYear(int year)
        {
            var tours = await _toursService.GetToursByYear(year);
            return tours.StatusCode == HttpStatusCode.OK ? Ok(tours) : StatusCode((int)tours.StatusCode, tours);
        }

        [HttpGet]
        [Route("GetToursByCity")]
        public async Task<IActionResult> GetToursByCity(string city)
        {
            var tours = await _toursService.GetToursByCity(city);
            return tours.StatusCode == HttpStatusCode.OK ? Ok(tours) : StatusCode((int)tours.StatusCode, tours);
        }
    }
}
