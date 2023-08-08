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
                    return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
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
            catch (Exception)
            {
                return;
            }
            
        }
        
        public async Task<BaseMessage<SongDTO>> GetById(int id)
        {
            var song = await _unitOfWork.SongRepository.FindAsync(id);    
            try
            {
                if (song == null)
                {
                    return Utilities.BuildResponse<SongDTO>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<SongDTO>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }

            // TODO: 
            var formattedDuration = Utilities.GetLengthInMinuteNotation(song.Duration);
            var newSong = new SongDTO
            {
                Name = song.Name,
                Duration = formattedDuration,
                AlbumId = song.AlbumId
            };
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<SongDTO>(){newSong});    
        }

        public async Task<BaseMessage<Song>> GetSongsByAlbumName(string song)
        {
            var songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Name.Equals(song), x => x.OrderBy(x => x.Id), new Album().GetType().Name);           
            try
            {
                if (songs  == null || !songs.Any())
                {
                    return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs.ToList());
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }

        public async Task<BaseMessage<Song>> GetSongsAsync(bool areReferencesLoaded)
        {
            try
            {
                if(areReferencesLoaded)
                {
                    var songs = await _unitOfWork.SongRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
                    if (songs  == null || !songs.Any())
                    {
                        return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                    }
                    return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs.ToList());
                }
                else
                {
                    var songs = await _unitOfWork.SongRepository.GetAllAsync();
                    if (songs  == null || !songs.Any())
                    {
                        return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                    }
                    return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs.ToList());
                } 

            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }

        public async Task<BaseMessage<Song>> UpdateSong(Song song)
        {
            try
            {
                var album = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
                if (album == null)
                {
                    return Utilities.BuildResponse<Song>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
                }
                await _unitOfWork.SongRepository.Update(song);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}"); 
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Song>(){song});
        }

        public async Task<BaseMessage<Song>> GetSongsByYear(int year)
        {
            try
            {
                var songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Year == year, includeProperties: new Album().GetType().Name);
                if (songs  == null || !songs.Any())
                {
                    return Utilities.BuildResponse<Song>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, songs.ToList());;
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Song>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}"); 
            }
            
        }
    }
}