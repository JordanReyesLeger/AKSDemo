using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PasteleriaService
    {
        private readonly IPastelRepository _pastelRepository;

        public PasteleriaService(IPastelRepository pastelRepository)
        {
            _pastelRepository = pastelRepository;
        }

        public async Task<List<Pastel>> ObtenerTodosLosPastelesAsync()
        {
            return await _pastelRepository.GetAllPastelesAsync();
        }

        public async Task<Pastel> ObtenerPastelPorIdAsync(int id)
        {
            return await _pastelRepository.GetPastelByIdAsync(id);
        }

        public async Task CrearPastelAsync(Pastel pastel)
        {
            if (string.IsNullOrEmpty(pastel.Nombre))
                throw new ArgumentException("El nombre del pastel es obligatorio.");

            if (pastel.Tamaño <= 0)
                throw new ArgumentException("El tamaño debe ser mayor a cero.");

            await _pastelRepository.AddPastelAsync(pastel);
        }

        public async Task EliminarPastelAsync(int id)
        {
            await _pastelRepository.DeletePastelAsync(id);
        }
    }
}
