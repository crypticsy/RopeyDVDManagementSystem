using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service; //Assigning the service
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll(); //Assigning all DVDCategory table data to variable 'data'
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorFirstName, ActorSurName")] DVDCategory category)
        {

            _service.Add(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetById(id);

            if (categoryDetails == null) return View("NotFound");

            return View(categoryDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var categoryDetails = await _service.GetById(id);

            if (categoryDetails == null) return View("NotFound");

            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DVDCategory category)
        {

            category.CategoryNumber = Convert.ToUInt32(id);
            await _service.Update(id, category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            var categoryDetails = await _service.GetById(id);

            if (categoryDetails == null) return View("NotFound");

            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetById(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
