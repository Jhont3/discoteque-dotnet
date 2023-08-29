using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Data.Models;
using Discoteque.Data.Dto;

namespace Discoteque.Business.IServices
{
    public interface ITourService
    {
        Task<BaseMessage<TourDTO>> GetToursAsync(bool areReferencesLoaded);
        Task<BaseMessage<Tour>> GetToursByArtist(string artist);
        Task<BaseMessage<TourDTO>> GetById(int id);
        Task<BaseMessage<Tour>> CreateTour(Tour tour);
        Task<BaseMessage<Tour>> UpdateTour(Tour tour);
        Task<BaseMessage<Tour>> GetToursByYear(int year);
        Task<BaseMessage<Tour>> GetToursByCity(string city);    
        Task DeleteById(int id);
    }
}
