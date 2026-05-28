using GestionEmploiTemps.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class CreneauController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreneauController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //  LIST PAGE
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("API");

            try
            {
                var res = await client.GetAsync("api/Creneau");

                var json = await res.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<List<Creneau>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(data);
            }
            catch
            {
                TempData["Error"] = "Erreur chargement créneaux";
                return View(new List<Creneau>());
            }
        }

        //CREATE

        [HttpPost]
        public async Task<IActionResult> Create(Creneau c)
        {
            var client = _httpClientFactory.CreateClient("API");

            try
            {
                var json = JsonSerializer.Serialize(c);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                var res = await client.PostAsync("api/Creneau", content);

                // 🔥 Lire la vraie réponse API
                var responseMessage = await res.Content.ReadAsStringAsync();

                if (res.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Créneau ajouté avec succès";
                }
                else
                {
                    TempData["Error"] =
                        $"Erreur API : {responseMessage}";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    $"Erreur serveur : {ex.Message}";

                return RedirectToAction("Index");
            }
        }
        // EDIT PAGE
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("API");

            try
            {
                var res = await client.GetAsync($"api/Creneau/{id}");

                if (!res.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Creneau introuvable";
                    return RedirectToAction("Index");
                }

                var json = await res.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<Creneau>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(data);
            }
            catch
            {
                TempData["Error"] = "Erreur chargement édition";
                return RedirectToAction("Index");
            }
        }

        //  UPDATE
        [HttpPost]
        public async Task<IActionResult> Edit(Creneau c)
        {
            var client = _httpClientFactory.CreateClient("API");

            try
            {
                var json = JsonSerializer.Serialize(c);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PutAsync($"api/Creneau/{c.IdCreneau}", content);

                TempData[res.IsSuccessStatusCode ? "Success" : "Error"] =
                    res.IsSuccessStatusCode ? "Creneau modifie" : "Erreur modification";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Erreur serveur";
                return RedirectToAction("Index");
            }
        }

        // DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("API");

            try
            {
                var res = await client.DeleteAsync($"api/Creneau/{id}");

                TempData[res.IsSuccessStatusCode ? "Success" : "Error"] =
                    res.IsSuccessStatusCode ? "Creneau supprimee" : "Erreur suppression";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Erreur serveur";
                return RedirectToAction("Index");
            }
        }
    }
}