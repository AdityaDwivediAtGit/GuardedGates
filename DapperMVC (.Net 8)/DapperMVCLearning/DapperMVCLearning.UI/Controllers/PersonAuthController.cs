using DapperMVCLearning.Data.Models.Domain;
using DapperMVCLearning.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DapperMVCLearning.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonAuthController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        public PersonAuthController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAll()
        {
            var people = await _personRepo.GetAllAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool personAdded = await _personRepo.AddAsync(person);
                if (personAdded)
                {
                    return Ok(new { message = "Successfully Added!" });
                }
                else
                {
                    return BadRequest(new { message = "Could not be added!" });
                }
            }
            catch
            {
                return StatusCode(500, new { message = "Could not be added!" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != person.Id)
                {
                    return BadRequest(new { message = "Invalid Id!" });
                }

                bool personUpdated = await _personRepo.UpdateAsync(person);
                if (personUpdated)
                {
                    return Ok(new { message = "Successfully Updated!" });
                }
                else
                {
                    return BadRequest(new { message = "Could not be updated!" });
                }
            }
            catch
            {
                return StatusCode(500, new { message = "Could not be updated!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleteResult = await _personRepo.DeleteAsync(id);
                if (deleteResult)
                {
                    return Ok(new { message = "Successfully Deleted!" });
                }
                else
                {
                    return BadRequest(new { message = "Could not be deleted!" });
                }
            }
            catch
            {
                return StatusCode(500, new { message = "Could not be deleted!" });
            }
        }
    }
}
