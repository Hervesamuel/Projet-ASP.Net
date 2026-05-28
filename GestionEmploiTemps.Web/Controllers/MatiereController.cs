using GestionEmploiTemps.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class MatiereController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatiereController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //INDEX 
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync("api/Matiere");

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Erreur chargement matieres";
                    return View(new List<Matiere>());
                }

                var json = await response.Content.ReadAsStringAsync();

                var matieres = JsonSerializer.Deserialize<List<Matiere>>
                (
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return View(matieres);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new List<Matiere>());
            }
        }

        // CREATE 
        [HttpPost]
        public async Task<IActionResult> Create(Matiere model)
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
                    "api/Matiere",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Matiere ajoutee avec succees";
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

        // GET- EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.GetAsync(
                    $"api/Matiere/{id}"
                );

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        "Matiere introuvable";

                    return RedirectToAction("Index");
                }

                var json =
                    await response.Content.ReadAsStringAsync();

                var matiere =
                    JsonSerializer.Deserialize<Matiere>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return View(matiere);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST-EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(Matiere model)
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
                    $"api/Matiere/{model.IdMatiere}",
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

        //DELETE 
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("API");

                var response = await client.DeleteAsync(
                    $"api/Matiere/{id}"
                );

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Matiere supprimee";
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