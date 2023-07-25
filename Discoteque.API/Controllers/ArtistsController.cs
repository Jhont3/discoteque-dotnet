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
        private readonly IArtistService _artistService;  // dependency injection used from project.cs

        public ArtistsController(IArtistService artistService) 
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Route("GetAllArtistsAsync")]
        public async Task<IActionResult> GetAllArtistsAsync()
        {
            var artists = await _artistService.GetArtistsAsync();
            return Ok(artists);
        }


        // TODO: IMplementame correctamnete
        /* tarea 3
            Implementar un nuevo endpoint donde se cree un usuario utilizando
            _artistsService.CreateArtist
        */

        [HttpPost]
        public async Task<IActionResult> Post(Artist artist)
        {
            var createdArtist = await _artistService.CreateArtist(artist);
            return Ok(createdArtist);
        }
        
    }
}
