using System.Net;
using System.Security.Cryptography.X509Certificates;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using Discoteque.Data.Services;

namespace Discoteque.Business.Services;

/// <summary>
/// This is a Album service implementation of <see cref="IAlbumService"/> 
/// </summary>
public class AlbumService : IAlbumService
{
    private IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Album"/> entity in Database. 
    /// </summary>
    /// <param name="album">A new album entity</param>
    /// <returns>The created album with an Id assigned</returns>
    public async Task<BaseMessage<Album>> CreateAlbum(Album album)
    {   
        var newAlbum = new Album{
            Name = album.Name,
            ArtistId = album.ArtistId,
            Genre = album.Genre,
            Year = album.Year,
            Cost = album.Cost
        };

        try
        {   
            var artist = await _unitOfWork.ArtistRepository.FindAsync(album.ArtistId);
            if(artist == null || album.Cost < 0 || album.Year < 1905 || album.Year > 2023 || Utilities.AreForbiddenWordsContained(album.Name))
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
            }
            
            await _unitOfWork.AlbumRepository.AddAsync(album);
            await _unitOfWork.SaveAsync();    
            
        }
        catch (Exception ex)
        {
             return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }

        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Album>(){album});

    }

    /// <summary>
    /// Finds all albums in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per album if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetAlbumsAsync(bool areReferencesLoaded)
    {    
        try
        {
            if(areReferencesLoaded)
            {
                var albums = await _unitOfWork.AlbumRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
                if (albums  == null || !albums.Any())
                {
                    return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
            }
            else
            {
                var albums = await _unitOfWork.AlbumRepository.GetAllAsync();
                if (albums  == null || !albums.Any())
                {
                    return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
            } 
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }

    }

    /// <summary>
    /// A list of albums released by a <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetAlbumsByArtist(string artist)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Artist.Name.Equals(artist), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);           
        try
        {
            if (albums  == null || !albums.Any())
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
        }
        catch (Exception ex)
        {
                return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }        
    }

    /// <summary>
    /// Returns all albums with the assigned genre
    /// </summary>
    /// <param name="genre">A genre from the <see cref="Genres"/> list</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetAlbumsByGenre(Genres genre)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Genre.Equals(genre), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);           
        try
        {
            if (albums  == null || !albums.Any())
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
        }
        catch (Exception ex)
        {
                return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }   
    }

    /// <summary>
    /// Returns all albums published in the year.
    /// </summary>
    /// <param name="year">A gregorian year between 1900 and current year</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetAlbumsByYear(int year)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year == year , x => x.OrderBy(x => x.Id), new Artist().GetType().Name);         
        try
        {
            if (albums  == null || !albums.Any())
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }           
    }

    /// <summary>
    /// returns all albums released from initial to max year
    /// </summary>
    /// <param name="initialYear">The initial year, min value 1900</param>
    /// <param name="maxYear">the latest year, max value 2025</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetAlbumsByYearRange(int initialYear, int maxYear)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year >= initialYear && x.Year <= maxYear , x => x.OrderBy(x => x.Id), new Artist().GetType().Name);      
        try
        {
            if (!albums.Any())
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, albums.ToList());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }          
    }

    /// <summary>
    /// Get an album by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Album"/> </returns>
    public async Task<BaseMessage<Album>> GetById(int id)
    {
            var album = await _unitOfWork.AlbumRepository.FindAsync(id);                    
            try
            {
                if (album == null)
                {
                    return Utilities.BuildResponse<Album>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Album>(){album});    
    }

    /// <summary>
    /// Updates the <see cref="Album"/> entity in EF DB
    /// </summary>
    /// <param name="album">The Album entity to update</param>
    /// <returns>The new album with updated fields if successful</returns>
    public async Task<BaseMessage<Album>> UpdateAlbum(Album album)
    {
            try
            {
                var artist = await _unitOfWork.ArtistRepository.FindAsync(album.ArtistId);
                if (artist == null)
                {
                    return Utilities.BuildResponse<Album>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
                }
                await _unitOfWork.AlbumRepository.Update(album);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Album>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}"); 
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Album>(){album});
    }
}
