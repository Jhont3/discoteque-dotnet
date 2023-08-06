using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface IArtistsService
{
    Task<BaseMessage<Artist>> GetArtistsAsync();
    Task<BaseMessage<Artist>> GetById(int id);
    Task<BaseMessage<Artist>> CreateArtist(Artist artist); 
    Task<BaseMessage<Artist>> UpdateArtist(Artist artist);
    Task<BaseMessage<Artist>> CreateArtistsInBatch(List<Artist> artists);
}
