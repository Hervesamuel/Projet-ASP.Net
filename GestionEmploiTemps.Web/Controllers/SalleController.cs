using GestionEmploiTemps.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class SalleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SalleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync("api/Salle");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        "Erreur chargement salles";

                    return View(new List<Salle>());
                }

                var json =
                    await response.Content.ReadAsStringAsync();

                var salles =
                    JsonSerializer.Deserialize<List<Salle>>
                    (
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                return View(salles);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(new List<Salle>());
            }
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Salle model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(
                    "api/Salle",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Salle ajoutee avec succes";
                }
                else
                {
                    TempData["Error"] =
                        "Erreur lors de l'ajout";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index");
            }
        }

        // GET-EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response =
                    await client.GetAsync($"api/Salle/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        "Salle introuvable";

                    return RedirectToAction("Index");
                }

                var json =
                    await response.Content.ReadAsStringAsync();

                var salle =
                    JsonSerializer.Deserialize<Salle>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return View(salle);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index");
            }
        }

        // EDIT-POST
        [HttpPost]
        public async Task<IActionResult> Edit(Salle model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PutAsync(
                    $"api/Salle/{model.IdSalle}",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Modification reussie";
                }
                else
                {
                    TempData["Error"] =
                        "Erreur modification";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction("Index");
            }
        }

        // DELETE 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response =
                    await client.DeleteAsync($"api/Salle/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Salle supprimee";
                }
                else
                {
                    TempData["Error"] =
                        "Erreur suppression";
                }

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