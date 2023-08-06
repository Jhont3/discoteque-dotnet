using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices
{
    public interface ISongService
    {
        Task<BaseMessage<Song>> GetSongsAsync(bool areReferencesLoaded);
        Task<BaseMessage<Song>> GetSongsByAlbumName(string song);
        Task<BaseMessage<Song>> GetSongsByYear(int year);
        Task<BaseMessage<Song>> GetById(int id);
        Task<BaseMessage<Song>> CreateSong(Song Song);
        Task<BaseMessage<Song>> CreateSongsInBatch(List<Song> Songs);
        Task<BaseMessage<Song>> UpdateSong(Song Song);
        Task DeleteById(int id);
    }
}
