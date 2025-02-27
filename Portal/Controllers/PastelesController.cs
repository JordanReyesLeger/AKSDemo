using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Entities;

namespace Portal.Controllers
{
    public class PastelesController : Controller
    {
        private readonly string _apiUrl;
        private readonly string _welcomeMessageSettings;
        private readonly string _welcomeMessageKeyVault;
        private readonly string _welcomeMessageKeyVaultWebApp;

        public PastelesController(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
            _welcomeMessageSettings = configuration["Messages:MessageSettings"];
            _welcomeMessageKeyVault = configuration["Messages:MessageKeyVault"];
            _welcomeMessageKeyVaultWebApp = configuration["Messages:KeyVaultWebApp"];
        }

        // Acción para mostrar la lista de pasteles
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.WelcomeMessageSettings = _welcomeMessageSettings;
                ViewBag.WelcomeMessageKeyVault = _welcomeMessageKeyVault;
                ViewBag.WelcomeMessageKeyVaultWebApp = _welcomeMessageKeyVaultWebApp;
                using var client = new HttpClient();
                var response = await client.GetStringAsync(_apiUrl);
                var pasteles = JsonSerializer.Deserialize<List<Pastel>>(response);
                return View(pasteles);
            }
            catch (Exception ex)
            {
                // Manejo de errores para mostrar un mensaje amigable
                ViewBag.ErrorMessage = "No se pudieron cargar los pasteles. Intenta nuevamente más tarde.";
                return View(new List<Pastel>());
            }
        }

        // Acción para mostrar el formulario de creación
        public IActionResult Crear()
        {
            var pastel = new Pastel(); // Asegúrate de que el modelo esté inicializado
            return View(pastel);
        }

        // Acción para manejar el envío del formulario de creación
        [HttpPost]
        public async Task<IActionResult> Crear(Pastel pastel)
        {
            if (!ModelState.IsValid)
            {
                return View(pastel);
            }

            try
            {
                using var client = new HttpClient();
                var json = JsonSerializer.Serialize(pastel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Pastel creado exitosamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Hubo un problema al crear el pastel. Intenta de nuevo.");
                return View(pastel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "No se pudo conectar con el servidor. Intenta más tarde.");
                return View(pastel);
            }
        }

        // Acción para eliminar un pastel
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.DeleteAsync($"{_apiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Pastel eliminado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar el pastel. Intenta de nuevo.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Hubo un problema al conectar con el servidor. Intenta más tarde.";
            }

            return RedirectToAction("Index");
        }
    }
}
