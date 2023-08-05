using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services
{
    public class SongService : ISongService
    {
        private IUnitOfWork _unitOfWork;

        public SongService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseMessage<Song>> CreateSong(Song newSong)
        {
            try
            {   
                var album = await _unitOfWork.AlbumRepository.FindAsync(newSong.AlbumId);
                if (album == null)
                {
                    return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ALBUM_NOT_FOUND);
                }
                await _unitOfWork.SongRepository.AddAsync(newSong);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, ex.Message);
            } 

            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Song>(){newSong});   
        }

        public async Task<BaseMessage<Song>> CreateSongsInBatch(List<Song> songs)
        {
            try
            {

                foreach (var song in songs)
                {
                    var album = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
                    if (album == null) { return Utilities.BuildResponse<Song>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);; }
                }

                foreach (var song in songs)
                {
                  await _unitOfWork.SongRepository.AddAsync(song);
                }
                await _unitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs);
    
        }

        public async Task DeleteById(int id)
        {
            try
            {
                var song = await _unitOfWork.SongRepository.FindAsync(id);
                if (song != null)
                {
                    await _unitOfWork.SongRepository.Delete(song);
                    await _unitOfWork.SaveAsync();
                    return;
                }  
            }
            catch (System.Exception)
            {
                return;
            }
            
        }
        
        public async Task<Song> GetById(int id)
        {
            var song = await _unitOfWork.SongRepository.FindAsync(id);
            return song;
        }

        public async Task<IEnumerable<Song>> GetSongsByAlbumName(string song)
        {
            IEnumerable<Song> songs;        
            songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Name.Equals(song), x => x.OrderBy(x => x.Id), new Album().GetType().Name);
            return songs;
        }

        public async Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded)
        {
            IEnumerable<Song> songs;
            if(areReferencesLoaded)
            {
                songs = await _unitOfWork.SongRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
            }
            else
            {
                songs = await _unitOfWork.SongRepository.GetAllAsync();
            }   
            return songs;
        }

        public async Task<Song> UpdateSong(Song song)
        {
            await _unitOfWork.SongRepository.Update(song);
            await _unitOfWork.SaveAsync();
            return song;
        }

    }
}