using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded);
        Task<IEnumerable<Song>> GetSongsByAlbumName(string song);
        Task<Song> GetById(int id);
        Task<Song> CreateSong(Song Song);
        Task<Song> UpdateSong(Song Song);
        Task DeleteById(int id);
    }
}
