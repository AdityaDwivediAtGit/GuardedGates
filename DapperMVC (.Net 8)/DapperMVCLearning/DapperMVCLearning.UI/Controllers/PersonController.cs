using DapperMVCLearning.Data.Models.Domain;
using DapperMVCLearning.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DapperMVCLearning.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;
        public PersonController(IPersonRepository personRepo ) { _personRepo = personRepo; }

        #region Add Person
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {   
                if (!ModelState.IsValid) return View(person);
                bool PersonAdded = await _personRepo.AddAsync(person);
                if (PersonAdded) TempData["Message"] = "Successfully Added !";
                else TempData["Message"] = "Could not be added !";
            }
            catch {
                TempData["Message"] = "Could not be Added !";
            }
            return RedirectToAction(nameof(Add));
        }
        #endregion

        #region Edit Person
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null) { return NotFound(); }
            return View(person);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid) return View(person);
                bool PersonEdited = await _personRepo.UpdateAsync(person);
                if (PersonEdited) TempData["Message"] = "Successfully Edited !";
                    //return RedirectToAction(nameof(DisplayAll));
                else TempData["Message"] = "Could not be edited !";
            }
            catch
            {
                TempData["Message"] = "Could not be Edited !";
                return View(person);
            }
            return View(person);
        }
        #endregion

        #region Get Person/People
        public async Task<IActionResult> GetById(int id)
        {
            return View();
        }

        
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> DisplayAll(Person person)
        {
            var people = await _personRepo.GetAllAsync();
            return View(people);
        }
        #endregion

        #region Delete Person
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }
        #endregion
    }
}
