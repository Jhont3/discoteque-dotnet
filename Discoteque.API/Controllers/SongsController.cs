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
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songsService;

        public SongsController(ISongService artistsService)
        {
            _songsService = artistsService;
        }

        [HttpGet]
        [Route("GetSongs")]
        public async Task<IActionResult> GetSongs(bool areReferencesLoaded = false)
        {
            var songs = await _songsService.GetSongsAsync(areReferencesLoaded);
            return Ok(songs);
        }

        [HttpGet]
        [Route("GetSongById")]
        public async Task<IActionResult> GetById(int id)
        {
            var song = await _songsService.GetById(id);
            return Ok(song);
        }

        [HttpGet]
        [Route("GetSongsByAlbumName")]
        public async Task<IActionResult> GetSongsByAlbumName(string album)
        {
            var songs = await _songsService.GetSongsByAlbumName(album);
            return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound, "There was no songs by that name");
        }

        [HttpPost]
        [Route("CreateSongAsync")]
        public async Task<IActionResult> CreateSongAsync(Song song)
        {
            var result = await _songsService.CreateSong(song);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateSongAsync")]
        public async Task<IActionResult> UpdateSongAsync(Song song)
        {
            var result = await _songsService.UpdateSong(song);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSong")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var result = await _songsService.DeleteById(id);
            return Ok(result);
        }
        
    }
}
