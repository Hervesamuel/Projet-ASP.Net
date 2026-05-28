using GestionEmploiTemps.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class NiveauController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NiveauController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync("api/Niveau");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Erreur chargement niveaux";
                    return View(new List<Niveau>());
                }

                var json = await response.Content.ReadAsStringAsync();

                var niveaux = JsonSerializer.Deserialize<List<Niveau>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(niveaux);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Niveau>());
            }
        }

        // ================= CREATE =================
        [HttpPost]
        public async Task<IActionResult> Create(Niveau model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Niveau", content);

                TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                    response.IsSuccessStatusCode
                        ? "Niveau ajouté"
                        : "Erreur ajout";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ================= EDIT GET =================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync($"api/Niveau/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Niveau introuvable";
                    return RedirectToAction("Index");
                }

                var json = await response.Content.ReadAsStringAsync();

                var niveau = JsonSerializer.Deserialize<Niveau>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(niveau);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ================= EDIT POST =================
        [HttpPost]
        public async Task<IActionResult> Edit(Niveau model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Niveau/{model.IdNiveau}", content);

                TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                    response.IsSuccessStatusCode
                        ? "Modification réussie"
                        : "Erreur modification";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ================= DELETE =================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.DeleteAsync($"api/Niveau/{id}");

                TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                    response.IsSuccessStatusCode
                        ? "Niveau supprimé"
                        : "Erreur suppression";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}