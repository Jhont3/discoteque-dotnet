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
            return songs.StatusCode == HttpStatusCode.OK ? Ok(songs) : StatusCode((int)songs.StatusCode, songs);

        }

        [HttpGet]
        [Route("GetSongById")]
        public async Task<IActionResult> GetById(int id)
        {
            var song = await _songsService.GetById(id);
            return song.StatusCode == HttpStatusCode.OK ? Ok(song) : StatusCode((int)song.StatusCode, song);
        }

        [HttpGet]
        [Route("GetSongsByAlbumName")]
        public async Task<IActionResult> GetSongsByAlbumName(string album)
        {
            var songs = await _songsService.GetSongsByAlbumName(album);
            return songs.StatusCode == HttpStatusCode.OK ? Ok(songs) : StatusCode((int)songs.StatusCode, songs);
        }

        [HttpPost]
        [Route("CreateSongAsync")]
        public async Task<IActionResult> CreateSongAsync(Song song)
        {
            var newSong = await _songsService.CreateSong(song);
            return newSong.StatusCode == HttpStatusCode.OK ? Ok(newSong) : StatusCode((int)newSong.StatusCode, newSong);
        }

        [HttpPost]
        [Route("CreateSongs")]
        public async Task<IActionResult> CreateSongs(List<Song> songs)
        {
            var newSongs = await _songsService.CreateSongsInBatch(songs);
            return newSongs.StatusCode == HttpStatusCode.OK ? Ok(newSongs) : StatusCode((int)newSongs.StatusCode, newSongs);
        }

        [HttpPut]
        [Route("UpdateSongAsync")]
        public async Task<IActionResult> UpdateSongAsync(Song song)
        {
            var result = await _songsService.UpdateSong(song);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete]
        [Route("DeleteSong")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songsService.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        [Route("GetSongsByYear")]
        public async Task<IActionResult> GetSongsByYear(int year)
        {
            var songs = await _songsService.GetSongsByYear(year);
            return songs.StatusCode == HttpStatusCode.OK ? Ok(songs) : StatusCode((int)songs.StatusCode, songs);
        }
        
    }
}
