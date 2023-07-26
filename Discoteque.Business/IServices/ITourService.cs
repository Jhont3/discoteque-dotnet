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
        Task<IEnumerable<Tour>> GetTourName(string tour);
        Task<Tour> GetById(int id);
        Task<Tour> CreateTour(Tour Tour);
        Task<Tour> UpdateTour(Tour Tour);
        Task<string> DeleteById(int id);
    }
}
