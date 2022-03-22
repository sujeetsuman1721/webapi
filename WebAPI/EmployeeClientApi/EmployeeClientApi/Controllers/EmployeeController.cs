using EmployeeClientApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeClientApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient http;
        public EmployeeController(IHttpClientFactory factory)
        {
            http = factory.CreateClient("myapi");
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IActionResult> IndexAsync()
        {
            var response = await http.GetAsync("api/Employee");
            
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            List<Employee> employee = JsonConvert.DeserializeObject<List<Employee>>(json);
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            if (!ModelState.IsValid) return View(model);
    
            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await http.PostAsync("api/products", content);

           
            return RedirectToAction("Index");
        }
    }
}
