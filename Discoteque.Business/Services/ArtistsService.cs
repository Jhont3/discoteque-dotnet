using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;
using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class ArtistsService : IArtistsService
{
    private IUnitOfWork _unitOfWork;

    public ArtistsService(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    public async Task<BaseMessage<Artist>> CreateArtist(Artist artist)
    {
        try
        {   
            if (artist.Name.Length > 99)
            {
                return Utilities.BuildResponse<Artist>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
            }
            await _unitOfWork.ArtistRepository.AddAsync(artist);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Artist>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        } 
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Artist>(){artist});   
    }

    public async Task<BaseMessage<Artist>> GetArtistsAsync()
    {
        try
        {
            var artists = await _unitOfWork.ArtistRepository.GetAllAsync();
            if (!artists.Any())
            {
                return Utilities.BuildResponse<Artist>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, artists.ToList());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Artist>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    public async Task<BaseMessage<Artist>> GetById(int id)
    {
        try
        {
            var artist =  await _unitOfWork.ArtistRepository.FindAsync(id);
            if (artist  == null)
            {
                return Utilities.BuildResponse<Artist>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Artist>(){artist});
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Artist>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    public async Task<BaseMessage<Artist>> UpdateArtist(Artist artist)
    {
        try
        {   
            await _unitOfWork.ArtistRepository.Update(artist);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Artist>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        } 
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Artist>(){artist});   
    }

    public async Task<BaseMessage<Artist>> CreateArtistsInBatch(List<Artist> artists)
    {
        try
        {
            foreach (var item in artists)
            {
                if(item.Name.Length <= 100)
                {
                    await _unitOfWork.ArtistRepository.AddAsync(item);
                }
            }
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {   
            return Utilities.BuildResponse<Artist>(HttpStatusCode.BadRequest, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, artists);        
    }

}
