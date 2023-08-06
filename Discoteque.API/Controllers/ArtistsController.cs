using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")] // [controller] makes that you need just put artirst
    [ApiController]
    public class ArtistsController : ControllerBase
 {
        private readonly IArtistsService _artistsService;

        public ArtistsController(IArtistsService artistsService)
        {
            _artistsService = artistsService;
        }

        [HttpGet]
        [Route("GetAllArtistsAsync")]
        public async Task<IActionResult> GetAllArtistsAsync()
        {
            var artists = await _artistsService.GetArtistsAsync();
            return artists.StatusCode == HttpStatusCode.OK ? Ok(artists) : StatusCode((int)artists.StatusCode, artists);
        }

        [HttpGet]
        [Route("GetArtistById")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artists = await _artistsService.GetById(id);
            return artists.StatusCode == HttpStatusCode.OK ? Ok(artists) : StatusCode((int)artists.StatusCode, artists);
        }

        [HttpPost]
        [Route("CreateArtists")]
        public async Task<IActionResult> CreateArtistsAsync(List<Artist> artists)
        {
            var result = await _artistsService.CreateArtistsInBatch(artists);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Route("CreateArtistAsync")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist)
        {
            var result = await _artistsService.CreateArtist(artist);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        [Route("UpdateArtistAsync")]
        public async Task<IActionResult> UpdateArtistAsync(Artist artist)
        {
            var result = await _artistsService.UpdateArtist(artist);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }
    }
}
