using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface IArtistService
{
    Task<IEnumerable<Artist>> GetArtistsAsync();
    Task<Artist> GetById(int id);
    Task<Artist> CreateArtist(Artist artist); //Artist artist TODO add to atribute
    Task<Artist> UpdateArtist(Artist artist);
}