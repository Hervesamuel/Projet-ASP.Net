using GestionEmploiTemps.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class EnseignantController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EnseignantController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync("api/Enseignant");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Erreur API lors du chargement des enseignants");
                }

                var json = await response.Content.ReadAsStringAsync();

                var enseignants = JsonSerializer.Deserialize<List<Enseignant>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(enseignants);
            }
            catch (Exception ex)
            {
                return Content($"Erreur Web : {ex.Message}");
            }
        }


        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Enseignant model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.Nom))
                {
                    return Content("Nom obligatoire");
                }

                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Enseignant", content);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Enseignant ajouté avec succès";
                }
                else
                {
                    TempData["Error"] = "Erreur lors de l'ajout";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Erreur Web : {ex.Message}");
            }
        }

        //GET-EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync($"api/Enseignant/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Enseignant introuvable");
                }

                var json = await response.Content.ReadAsStringAsync();

                var enseignant = JsonSerializer.Deserialize<Enseignant>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(enseignant);
            }
            catch (Exception ex)
            {
                return Content($"Erreur : {ex.Message}");
            }
        }

        //POST-EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(Enseignant model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var json = JsonSerializer.Serialize(model);

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Enseignant/{model.IdEns}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Modification réussie";
                }
                else
                {
                    TempData["Error"] = "Erreur modification";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Erreur Web : {ex.Message}");
            }
        }

        // GET-DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync($"api/Enseignant/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return Content("Enseignant introuvable");
                }

                var json = await response.Content.ReadAsStringAsync();

                var enseignant = JsonSerializer.Deserialize<Enseignant>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return View(enseignant);
            }
            catch (Exception ex)
            {
                return Content($"Erreur : {ex.Message}");
            }
        }

        //POST-DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(Enseignant model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.DeleteAsync($"api/Enseignant/{model.IdEns}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Enseignant supprimé avec succès";
                }
                else
                {
                    TempData["Error"] = "Erreur lors de la suppression";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Erreur Web : {ex.Message}");
            }
        }
    }
}