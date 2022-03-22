using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly IRepository<Employee> _productsRepository;


        public EmployeeController(IRepository<Employee> repository)
        {
            _productsRepository = repository;

        }

        [HttpGet]
        public IActionResult GetEmployee()
        {

            var employee = _productsRepository.Get();

            return Ok(employee);

        }

        // get by id 

        [HttpGet("{id}")]

        public IActionResult GetEmployee(int id)
        {
            var Products = _productsRepository.Get(p => p.Id == id);
            return Ok(Products);
        }

        // save data int othe data base we are going to use the httpPost

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee model)
        {
            _productsRepository.Add(model);
            int rowCount = await _productsRepository.SaveAsync();

            if (rowCount > 0)
            {
                return Ok(rowCount);
            }
            else
                return BadRequest();


        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Employee model)
        {
            _productsRepository.Update(model);

            int rowEfected = await _productsRepository.SaveAsync();

            if (rowEfected > 0)
            {
                return Ok(model);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var Employee = _productsRepository.Get(p => p.Id == id).FirstOrDefault();


            if (Employee != null)
            {
                _productsRepository.Delete(Employee);
                int rowCount = await _productsRepository.SaveAsync();

                if (rowCount > 0)
                {
                    return Ok(Employee);
                }

            }
            return NotFound();

        }






    }
}