using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        // public AlbumService(IUnitOfWork unitOfWork)
        // {
        //     _unitOfWork = unitOfWork;
        // }
        public async Task<Song> CreateSong(Song song)
        {
            var album = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
            if (album == null)
            {
                return null;
            }
            
            // TODO: consider return album
            // var newSong = new Song{
            // Name = album.Name,
            // Album = album,
            // Genre = album.Genre,
            // Year = album.Year
            // };

            await _unitOfWork.SongRepository.AddAsync(song);
            await _unitOfWork.SaveAsync();
            return song;
        }

        public async Task<List<Song>> CreateSong(List<Song> songs)
        {
            // TODO: implement validations and correct operation
            foreach (var song in songs)
            {
                var album = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
                if (album == null) { return null; }
            }
            foreach (var song in songs)
            {
                await _unitOfWork.SongRepository.AddAsync(song);
                await _unitOfWork.SaveAsync();
            }
            return songs;
    
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