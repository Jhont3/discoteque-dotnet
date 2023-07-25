using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(artists);
        }


        [HttpPost]
        [Route("CreateArtistAsync")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist)
        {
            var result = await _artistsService.CreateArtist(artist);
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdateArtistAsync")]
        public async Task<IActionResult> UpdateArtistAsync(Artist artist)
        {
            return Ok();
        }
    }
}
