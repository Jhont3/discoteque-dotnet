using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services
{
    public class TourService : ITourService
    {
        private IUnitOfWork _unitOfWork;

        public TourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tour> CreateTour(Tour tour)
        {
            await _unitOfWork.TourRepository.AddAsync(tour);
            await _unitOfWork.SaveAsync();
            return tour;
        }

        public async Task DeleteById(int id)
        {
            try
            {
                var tour = await _unitOfWork.TourRepository.FindAsync(id);
                if (tour != null)
                {
                    await _unitOfWork.TourRepository.Delete(tour);
                    await _unitOfWork.SaveAsync();
                    return;
                }  
            }
            catch (System.Exception)
            {
                return;
            }
        }

        public async Task<Tour> GetById(int id)
        {
            var song = await _unitOfWork.TourRepository.FindAsync(id);
            return song;
        }

        public async Task<IEnumerable<Tour>> GetToursByArtist(string artist)
        {
            IEnumerable<Tour> tours;        
            tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Artist.Name.Equals(artist), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
            return tours;
        }

        public async Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded)
        {
            IEnumerable<Tour> tours;
            if(areReferencesLoaded)
            {
                tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
            }
            else
            {
                tours = await _unitOfWork.TourRepository.GetAllAsync();
            }   
            return tours;
        }

        public async Task<Tour> UpdateTour(Tour tour)
        {
            await _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveAsync();
            return tour;
        }
    }
}