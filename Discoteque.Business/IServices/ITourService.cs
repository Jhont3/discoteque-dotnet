using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Data.Models;

namespace Discoteque.Business.IServices
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded);
        Task<IEnumerable<Tour>> GetToursByArtist(string artist);
        Task<Tour> GetById(int id);
        Task<Tour> CreateTour(Tour Tour);
        Task<Tour> UpdateTour(Tour Tour);
        Task DeleteById(int id);
    }
}
