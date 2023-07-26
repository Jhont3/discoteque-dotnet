using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data;
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
        public async Task<Song> CreateSong(Song Song)
        {
            await _unitOfWork.SongRepository.AddAsync(Song);
            await _unitOfWork.SaveAsync();
            return Song;
        }

        // TODO: Verifies functionality
        public async Task<string> DeleteById(int id)
        {
            var song = await _unitOfWork.SongRepository.FindAsync(id);
            if (song != null)
            {
                await _unitOfWork.SongRepository.Delete(id);
                return "Song with Id: " + song.Id + " was deleted successfully";
            }
            return "Something went wrong, contact the administrator";
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