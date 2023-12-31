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
using AutoMapper;

namespace Discoteque.Business.Services
{
    public class TourService : ITourService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TourService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<BaseMessage<TourDTO>> GetById(int id)
        {
            var tour = await _unitOfWork.TourRepository.FindAsync(id);                  
            try
            {
                if (tour == null)
                {   
                    return Utilities.BuildResponse<TourDTO>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<TourDTO>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }

            var newDate = Utilities.ConvertToIsoDate(tour.Date);
            var newTour = new TourDTO(){
                Name = tour.Name,
                IscompletelySold = tour.IscompletelySold,
                Date = newDate,
                ArtistId = tour.ArtistId,
                City = tour.City,
            };

            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<TourDTO>(){newTour}); 
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
            }
            catch (Exception ex)
            {
                 return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, tours.ToList());
        }

        public async Task<BaseMessage<TourDTO>> GetToursAsync(bool areReferencesLoaded)
        {
            if(areReferencesLoaded)
            {
                var tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<TourDTO>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, _mapper.Map<List<TourDTO>>(tours));
            }
            else
            {
                var tours = await _unitOfWork.TourRepository.GetAllAsync();
                if (!tours.Any())
                {
                    return Utilities.BuildResponse<TourDTO>(HttpStatusCode.NotFound, BaseMessageStatus.ELEMENT_NOT_FOUND);
                }
                return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, _mapper.Map<List<TourDTO>>(tours));
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
