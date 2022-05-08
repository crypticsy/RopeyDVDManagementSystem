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
            return View(); //Assigning view to add new Category
        }

        //Request to post data 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName, CategoryDescription, AgeRestricted")] DVDCategory category)
        {

            _service.Add(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _service.GetById(id);  //Assigning selected Actor table data to variable 'categoryDetails'

            if (categoryDetails == null) return View("NotFound"); //Handeling errors

            return View(categoryDetails); //Assigning view to view details
        }

        public async Task<IActionResult> Edit(int id)
        {

            var categoryDetails = await _service.GetById(id);

            if (categoryDetails == null) return View("NotFound");

            return View(categoryDetails); //Assigning view to edit
        }


        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DVDCategory category)
        {

            category.CategoryNumber = Convert.ToUInt32(id);
            await _service.Update(id, category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var categoryDetails = await _service.GetById(id);

            if (categoryDetails == null) return View("NotFound"); //Handeling errors

            return View(categoryDetails); //Assigning view to delete
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetById(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index)); //Assigning view to delete
        }
    }
}
