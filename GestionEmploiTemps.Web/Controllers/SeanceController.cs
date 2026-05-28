using GestionEmploiTemps.Web.Models;
using GestionEmploiTemps.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GestionEmploiTemps.Web.Controllers
{
    public class SeanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SeanceController(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private async Task<T> Load<T>(
    HttpClient client,
    string url,
    System.Text.Json.JsonSerializerOptions options)
        {
            var res = await client.GetAsync(url);

            var json = await res.Content.ReadAsStringAsync();

            return System.Text.Json.JsonSerializer.Deserialize<T>(json, options)
                   ?? Activator.CreateInstance<T>();
        }

        public async Task<IActionResult> Index()
        {
            var vm = new SeanceVM();

            var client =
                _httpClientFactory.CreateClient("API");

            var options =
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

            // SEANCES
            var seancesRes =
                await client.GetAsync("api/Seance");

            if (seancesRes.IsSuccessStatusCode)
            {
                var json =
                    await seancesRes.Content.ReadAsStringAsync();

                vm.Seances =
                    JsonSerializer.Deserialize<List<Seance>>
                    (json, options) ?? new();
            }

            //  ENSEIGNANTS
            var ensRes =
                await client.GetAsync("api/Enseignant");

            if (ensRes.IsSuccessStatusCode)
            {
                var json =
                    await ensRes.Content.ReadAsStringAsync();

                vm.Enseignants =
                    JsonSerializer.Deserialize<List<Enseignant>>
                    (json, options) ?? new();
            }

            //  MATIERES
            var matRes =
                await client.GetAsync("api/Matiere");

            if (matRes.IsSuccessStatusCode)
            {
                var json =
                    await matRes.Content.ReadAsStringAsync();

                vm.Matieres =
                    JsonSerializer.Deserialize<List<Matiere>>
                    (json, options) ?? new();
            }

            // SALLES
            var salleRes =
                await client.GetAsync("api/Salle");

            if (salleRes.IsSuccessStatusCode)
            {
                var json =
                    await salleRes.Content.ReadAsStringAsync();

                vm.Salles =
                    JsonSerializer.Deserialize<List<Salle>>
                    (json, options) ?? new();
            }

            // CRENEAUX
            var crenRes =
                await client.GetAsync("api/Creneau");

            if (crenRes.IsSuccessStatusCode)
            {
                var json =
                    await crenRes.Content.ReadAsStringAsync();

                vm.Creneaux =
                    JsonSerializer.Deserialize<List<Creneau>>
                    (json, options) ?? new();
            }

            //  PARCOURS
            var parcRes =
                await client.GetAsync("api/Parcours");

            if (parcRes.IsSuccessStatusCode)
            {
                var json =
                    await parcRes.Content.ReadAsStringAsync();

                vm.Parcours =
                    JsonSerializer.Deserialize<List<Parcours>>
                    (json, options) ?? new();
            }

            return View(vm);
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Seance s)
        {
            var client =
                _httpClientFactory.CreateClient("API");

            try
            {
                var json =
                    System.Text.Json.JsonSerializer.Serialize(s);

                var content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    );

                var response =
                    await client.PostAsync(
                        "api/Seance",
                        content
                    );

                var result =
                    await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Séance ajoutée avec succès";
                }
                else
                {
                    TempData["Error"] =
                        result;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    $"Erreur serveur : {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // GET-EDIT

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new SeanceVM();

            var client =
                _httpClientFactory.CreateClient("API");

            try
            {
                // 🔵 séance
                var res =
                    await client.GetAsync($"api/Seance/{id}");

                if (!res.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Séance introuvable";
                    return RedirectToAction("Index");
                }

                var json =
                    await res.Content.ReadAsStringAsync();

                var seance =
                    System.Text.Json.JsonSerializer.Deserialize<Seance>(
                        json,
                        new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                vm.NouvelleSeance = seance ?? new Seance();

                // 🔵 reload dropdowns (IMPORTANT)
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                vm.Enseignants =
                    await Load<List<Enseignant>>(client, "api/Enseignant", options);

                vm.Matieres =
                    await Load<List<Matiere>>(client, "api/Matiere", options);

                vm.Salles =
                    await Load<List<Salle>>(client, "api/Salle", options);

                vm.Creneaux =
                    await Load<List<Creneau>>(client, "api/Creneau", options);

                vm.Parcours =
                    await Load<List<Parcours>>(client, "api/Parcours", options);

                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        //POST-EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(Seance s)
        {
            var client =
                _httpClientFactory.CreateClient("API");

            try
            {
                var json =
                    System.Text.Json.JsonSerializer.Serialize(s);

                var content =
                    new StringContent(json, Encoding.UTF8, "application/json");

                var res =
                    await client.PutAsync($"api/Seance/{s.IdSeance}", content);

                var result =
                    await res.Content.ReadAsStringAsync();

                if (res.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Séance modifiée";
                    return RedirectToAction("Index");
                }

                TempData["Error"] = result;
                return RedirectToAction("Edit", new { id = s.IdSeance });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Edit", new { id = s.IdSeance });
            }
        }

        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client =
                _httpClientFactory.CreateClient("API");

            try
            {
                var response =
                    await client.DeleteAsync(
                        $"api/Seance/{id}"
                    );

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] =
                        "Séance supprimée";
                }
                else
                {
                    TempData["Error"] =
                        "Erreur suppression";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}