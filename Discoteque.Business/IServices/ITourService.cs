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
        Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded);
        Task<IEnumerable<Tour>> GetToursByArtist(string artist);
        Task<Tour> GetById(int id);
        Task<TourMessage> CreateTour(Tour tour);
        Task<Tour> UpdateTour(Tour tour);
        Task DeleteById(int id);
    }
}
