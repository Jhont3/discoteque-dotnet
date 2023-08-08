using System.Data.SqlTypes;
using System.Net;
using Discoteque.Data.Models;
using Discoteque.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    [Route("GetAlbums")]
    public async Task<IActionResult> GetAlbums(bool areReferencesLoaded = false)
    {
        var albums = await _albumService.GetAlbumsAsync(areReferencesLoaded);
        return albums.StatusCode == HttpStatusCode.OK ? Ok(albums) : StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("GetAlbumById")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.GetById(id);
        return album.StatusCode == HttpStatusCode.OK ? Ok(album) : StatusCode((int)album.StatusCode, album);
    }

    [HttpGet]
    [Route("GetAlbumsByYear")]
    public async Task<IActionResult> GetAlbumsByYear(int year)
    {
        var albums = await _albumService.GetAlbumsByYear(year);
        return albums.StatusCode == HttpStatusCode.OK ? Ok(albums) : StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("GetAlbumsByYearRange")]
    public async Task<IActionResult> GetAlbumsByYearRange(int initialYear, int maxYear)
    {
        var albums = await _albumService.GetAlbumsByYearRange(initialYear, maxYear);
        return albums.StatusCode == HttpStatusCode.OK ? Ok(albums) : StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("GetAlbumsByGenre")]
    public async Task<IActionResult> GetAlbumsByGenre(Genres genre)
    {
        var albums = await _albumService.GetAlbumsByGenre(genre);
        return albums.StatusCode == HttpStatusCode.OK ? Ok(albums) : StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("GetAlbumsByArtist")]
    public async Task<IActionResult> GetAlbumsByArtist(string artist)
    {
        var albums = await _albumService.GetAlbumsByArtist(artist);
        return albums.StatusCode == HttpStatusCode.OK ? Ok(albums) : StatusCode((int)albums.StatusCode, albums);
    }

    [HttpPost]
    [Route("CreateAlbum")]
    public async Task<IActionResult> CreateAlbumsAsync(Album album)
    {
        var result = await _albumService.CreateAlbum(album);
        return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
    }
}
