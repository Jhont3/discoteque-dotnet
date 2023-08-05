using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Models;
using Discoteque.Data.Dto;
using System.Net;
using Discoteque.Business.Utils;

namespace Discoteque.Business.Services
{
    public class TourService : ITourService
    {
        private IUnitOfWork _unitOfWork;

        public TourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseMessage<Tour>> CreateTour(Tour tour)
        {
            try
            {
                var artist = await _unitOfWork.ArtistRepository.FindAsync(tour.ArtistId);
                if (tour.Date.Year <= 2021 || artist == null)
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.BAD_REQUEST_400);
                }
            
                await _unitOfWork.TourRepository.AddAsync(tour);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                  return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, BaseMessageStatus.INTERNAL_SERVER_ERROR_500);
            }
            return Utilities.BuildResponse<Tour>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new(){tour});
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
            var tour = await _unitOfWork.TourRepository.FindAsync(id);

            // var newDate = ConvertToIsoDate(tour.Date);
            
            // var newTour = new Tour(){
            //     Name = tour.Name,
            //     IscompletelySold = tour.IscompletelySold,
            //     Date = ConvertDateTimeToString(newDate),
            //     ArtistId = tour.ArtistId,
            //     City = tour.City,
            // };

            return tour;
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
                tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
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
