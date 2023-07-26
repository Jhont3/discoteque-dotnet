using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services
{
    public class TourService : ITourService
    {
        public Task<Tour> CreateTour(Tour Tour)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> GetTourName(string tour)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded)
        {
            throw new NotImplementedException();
        }

        public Task<Tour> UpdateTour(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}