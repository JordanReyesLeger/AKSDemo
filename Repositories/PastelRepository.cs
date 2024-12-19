using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PastelRepository : IPastelRepository
    {
        private static List<Pastel> _pasteles;

        public PastelRepository()
        {
            _pasteles = new List<Pastel>();
        }

        public Task<Pastel> GetPastelByIdAsync(int id)
        {
            return Task.FromResult(_pasteles.FirstOrDefault(p => p.Id == id));
        }

        public Task<List<Pastel>> GetAllPastelesAsync()
        {
            return Task.FromResult(_pasteles);
        }

        public Task AddPastelAsync(Pastel pastel)
        {
            pastel.Id = _pasteles.Count + 1;
            _pasteles.Add(pastel);
            return Task.CompletedTask;
        }

        public Task DeletePastelAsync(int id)
        {
            var pastel = _pasteles.FirstOrDefault(p => p.Id == id);
            if (pastel != null)
            {
                _pasteles.Remove(pastel);
            }
            return Task.CompletedTask;
        }
    }
}
