using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.WebApplication.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC.WebApplication.Controllers
{
    public class NoteController : Controller
    {
        private static readonly string _noteEndpoint = @"https://localhost:7011/api/Note/";
        private static readonly string _StatusEndpoint = @"https://localhost:7011/api/Status/";
        private static readonly string _UserEndpoint = @"https://localhost:7011/api/User/";

        /// <summary>
        ///   Load list of notes from database
        /// </summary>
        /// <returns>List</returns>
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(_noteEndpoint);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<NoteModel>>(responseBody).ToList();
                    return View(model);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return BadRequest(e.Message);
                }
            }
        }


        /// <summary>
        ///   Handle HttpPost event based on button value.
        ///     <para>Create Note entry when button value="ButtonCreate"</para>
        ///     <para>Update Note entry when button value="ButtonUpdate"</para>
        /// </summary>
        /// <param name="NoteId">(nullable) Note Id</param>
        /// <param name="noteModal">Note model</param>
        /// <param name="button">Button value</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int NoteId, NoteModel noteModal, string button)
        {
            if (!ModelState.IsValid && !(button == "ButtonUpdate" || button == "ButtonCreate"))
            {
                return BadRequest("Invalid");
            }

            // Create
            if (button == "ButtonCreate")
            {
                var objectString = JsonConvert.SerializeObject(noteModal);
                var jsonContent = new StringContent(objectString, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    try
                    {
                        using HttpResponseMessage response = await client.PostAsync(_noteEndpoint, jsonContent);
                        response.EnsureSuccessStatusCode();
                        return RedirectToAction("Index");
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                        return BadRequest(e.Message);
                    }
                }
            }

            // Update
            if (button == "ButtonUpdate")
            {
                var objectString = JsonConvert.SerializeObject(noteModal);
                var jsonContent = new StringContent(objectString, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    try
                    {
                        using HttpResponseMessage response = await client.PutAsync(_noteEndpoint + NoteId, jsonContent);
                        response.EnsureSuccessStatusCode();
                        return PartialView(noteModal);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                        return BadRequest(e.Message);
                    }
                }
            }

            return View();
        }

        /// <summary>
        ///   Delete Note entry in database
        /// </summary>
        /// <param name="id">Note Id</param>
        /// <param name="button">Button value</param>
        /// <returns>null</returns>
        public async Task<IActionResult> Delete(int id, string button)
        {
            // Invalid access
            if (button != "ButtonDelete")
            {
                return BadRequest("Invalid");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        using HttpResponseMessage response = await client.DeleteAsync(_noteEndpoint + id);
                        response.EnsureSuccessStatusCode();
                        return RedirectToAction("Index");
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                        return BadRequest(e.Message);
                    }
                }
            }
        }

        /// <summary>
        ///   Load partial view with all relevent data.
        /// </summary>
        /// <param name="id">Nullable NoteId</param>
        /// <returns>Partial View</returns>
        [HttpGet]
        public async Task<IActionResult> PartialViewDetails(int? id)
        {
            // Create new Note
            if (id == null)
            {
                // Get dropdown list
                var statusListItems = await GetStatusList();
                var userListIems = await GetUserList();

                // Empty model
                var model = new NoteModel()
                {
                    DueDate = DateTime.Now,
                    StatusListItems = statusListItems,
                    UserListIems = userListIems
                };
                return PartialView(model);
            }
            else // Load previous note
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        using HttpResponseMessage response = await client.GetAsync(_noteEndpoint + id);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<NoteModel>(responseBody);

                        // Get dropdown list
                        model.StatusListItems = await GetStatusList();
                        model.UserListIems = await GetUserList();

                        return PartialView(model);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("\nException Caught!");
                        Console.WriteLine("Message :{0} ", e.Message);
                        return BadRequest(e.Message);
                    }
                }
            }
        }

        /// <summary>
        ///   Get entire list of available status from database.
        /// </summary>
        /// <returns>List&lt;SelectListItem&gt;</returns>
        private async Task<List<SelectListItem>> GetStatusList()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(_StatusEndpoint);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<StatusModel>>(responseBody);
                    return model.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                }
                catch (HttpRequestException e)
                {
                    return new List<SelectListItem>();
                }
            }
        }

        /// <summary>
        ///   Get entire list of available users from database.
        /// </summary>
        /// <returns>List&lt;SelectListItem&gt;</returns>
        private async Task<List<SelectListItem>> GetUserList()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(_UserEndpoint);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<UserModel>>(responseBody);
                    return model.Select(x => new SelectListItem
                    {
                        Text = x.Username,
                        Value = x.UserId.ToString()
                    }).ToList();
                }
                catch (HttpRequestException e)
                {
                    return new List<SelectListItem>();
                }
            }
        }

    }
}
