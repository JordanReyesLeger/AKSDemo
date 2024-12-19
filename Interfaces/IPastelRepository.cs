using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPastelRepository
    {
        Task<Pastel> GetPastelByIdAsync(int id);
        Task<List<Pastel>> GetAllPastelesAsync();
        Task AddPastelAsync(Pastel pastel);
        Task DeletePastelAsync(int id);
    }
}
