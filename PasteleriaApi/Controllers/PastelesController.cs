using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace PasteleriaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastelesController : ControllerBase
    {
        private readonly PasteleriaService _pasteleriaService;

        public PastelesController(PasteleriaService pasteleriaService)
        {
            _pasteleriaService = pasteleriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Pastel> pasteles = await _pasteleriaService.ObtenerTodosLosPastelesAsync();
            return Ok(pasteles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pastel = await _pasteleriaService.ObtenerPastelPorIdAsync(id);
            if (pastel == null)
                return NotFound();

            return Ok(pastel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pastel pastel)
        {
            await _pasteleriaService.CrearPastelAsync(pastel);
            return CreatedAtAction(nameof(Get), new { id = pastel.Id }, pastel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pasteleriaService.EliminarPastelAsync(id);
            return NoContent();
        }
    }
}
