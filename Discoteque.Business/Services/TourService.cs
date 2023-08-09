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

        private DateTime ConvertDateTimeToString(string date){
            return DateTime.Parse(date);
        }

        private string ConvertToIsoDate(DateTime date){
            return date.ToString("yyyy-MM-dd");
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
            catch (Exception)
            {
                return;
            }
        }

        public async Task<BaseMessage<Tour>> GetById(int id)
        {
            var tour = await _unitOfWork.TourRepository.FindAsync(id);                  
            try
            {
                if (tour == null)
                {   
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                
                var newDate = ConvertToIsoDate(tour.Date);
                var newTour = new Tour(){
                    Name = tour.Name,
                    IscompletelySold = tour.IscompletelySold,
                    Date = ConvertDateTimeToString(newDate),
                    ArtistId = tour.ArtistId,
                    City = tour.City,
                };
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Tour>(){newTour}); 
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }

        public async Task<BaseMessage<Tour>> GetToursByArtist(string artist)
        {
            var tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Artist.Name.Equals(artist), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);     
            try
            {
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }

        public async Task<BaseMessage<Tour>> GetToursAsync(bool areReferencesLoaded)
        {
            if(areReferencesLoaded)
            {
                var tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
            }
            else
            {
                var tours = await _unitOfWork.TourRepository.GetAllAsync();
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
            } 
        }

        public async Task<BaseMessage<Tour>> UpdateTour(Tour tour)
        {
            try
            {
                var album = await _unitOfWork.ArtistRepository.FindAsync(tour.ArtistId);
                if (album == null)
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                await _unitOfWork.TourRepository.Update(tour);
                await _unitOfWork.SaveAsync();
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Tour>(){tour});

            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}"); 
            }
        }

        public async Task<BaseMessage<Tour>> GetToursByCity(string city)
        {
            var tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Equals(city));    
            try
            {
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }

        public async Task<BaseMessage<Tour>> GetToursByYear(int year)
        {
            var tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Date.Year == year);     
            try
            {
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
        }
    }
}
