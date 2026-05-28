using GestionEmploiTemps.Web.Models;
using GestionEmploiTemps.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class ParcoursController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ParcoursController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        // 🟦 PAGE PRINCIPALE
        
        public async Task<IActionResult> Index()
        {
            var vm = new AcademiqueVM();

            var client = _httpClientFactory.CreateClient("API");

            try
            {
                // NIVEAUX 
                var niveauxRes = await client.GetAsync("api/Niveau");
                if (niveauxRes.IsSuccessStatusCode)
                {
                    var json = await niveauxRes.Content.ReadAsStringAsync();
                    vm.Niveaux = JsonSerializer.Deserialize<List<Niveau>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }

                // PARCOURS 
                var parcoursRes = await client.GetAsync("api/Parcours");
                if (parcoursRes.IsSuccessStatusCode)
                {
                    var json = await parcoursRes.Content.ReadAsStringAsync();
                    vm.Parcours = JsonSerializer.Deserialize<List<Parcours>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                }

                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(vm);
            }
        }

  
        // 🟦 CREATE NIVEAU
        
        [HttpPost]
        public async Task<IActionResult> CreateNiveau(AcademiqueVM vm)
        {
            var client = _httpClientFactory.CreateClient("API");

            var data = new { Nom = vm.NouveauNiveau };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("api/Niveau", content);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur ajout niveau";

            return RedirectToAction("Index");
        }

        
        // 🟦 UPDATE NIVEAU
     
        [HttpPost]
        public async Task<IActionResult> EditNiveau(int id, string nom)
        {
            var client = _httpClientFactory.CreateClient("API");

            var data = new { IdNiveau = id, Nom = nom };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"api/Niveau/{id}", content);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur modification niveau";

            return RedirectToAction("Index");
        }

     
        // 🟦 DELETE NIVEAU
        
        [HttpPost]
        public async Task<IActionResult> DeleteNiveau(int id)
        {
            var client = _httpClientFactory.CreateClient("API");

            var response = await client.DeleteAsync($"api/Niveau/{id}");

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur suppression niveau";

            return RedirectToAction("Index");
        }

      
        // 🟩 CREATE PARCOURS
    
        [HttpPost]
        public async Task<IActionResult> CreateParcours(AcademiqueVM vm)
        {
            var client = _httpClientFactory.CreateClient("API");

            var data = new
            {
                Nom = vm.NouveauParcours,
                IdNiveau = vm.IdNiveau
            };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync("api/Parcours", content);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur ajout parcours";

            return RedirectToAction("Index");
        }

     
        // 🟩 UPDATE PARCOURS
        
        [HttpPost]
        public async Task<IActionResult> EditParcours(int id, string nom, int idNiveau)
        {
            var client = _httpClientFactory.CreateClient("API");

            var data = new
            {
                IdParcours = id,
                Nom = nom,
                IdNiveau = idNiveau
            };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"api/Parcours/{id}", content);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur modification parcours";

            return RedirectToAction("Index");
        }

        // 🟩 DELETE PARCOURS
        
        [HttpPost]
        public async Task<IActionResult> DeleteParcours(int id)
        {
            var client = _httpClientFactory.CreateClient("API");

            var response = await client.DeleteAsync($"api/Parcours/{id}");

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Action reussi" : "Erreur suppression parcours";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new AcademiqueVM();

            try
            {
                var client = _httpClientFactory.CreateClient("API");

                // 🔵 1. RÉCUPÉRATION PARCOURS
                var parcoursRes = await client.GetAsync($"api/Parcours/{id}");

                if (!parcoursRes.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Parcours introuvable ou erreur API.";
                    return RedirectToAction("Index");
                }

                var json = await parcoursRes.Content.ReadAsStringAsync();

                var parcours = System.Text.Json.JsonSerializer.Deserialize<Parcours>(
                    json,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (parcours == null)
                {
                    TempData["Error"] = "Données parcours invalides.";
                    return RedirectToAction("Index");
                }

                vm.IdParcours = parcours.IdParcours;
                vm.NouveauParcours = parcours.Nom;
                vm.IdNiveau = parcours.IdNiveau;

                // 🔵 2. RÉCUPÉRATION NIVEAUX
                var niveauxRes = await client.GetAsync("api/Niveau");

                if (!niveauxRes.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Impossible de charger les niveaux.";
                    vm.Niveaux = new List<Niveau>();
                    return View("EditParcoursNiveau", vm);
                }

                var jsonN = await niveauxRes.Content.ReadAsStringAsync();

                var niveaux = System.Text.Json.JsonSerializer.Deserialize<List<Niveau>>(
                    jsonN,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                vm.Niveaux = niveaux ?? new List<Niveau>();

                return View("EditParcoursNiveau", vm);
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = "Erreur de connexion avec l'API.";
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
            catch (System.Text.Json.JsonException ex)
            {
                TempData["Error"] = "Erreur de lecture des données API.";
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erreur inattendue.";
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}