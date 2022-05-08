using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class StudioController : Controller
    {
        private readonly IStudioService _service;

        public StudioController(IStudioService service)
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
        public async Task<IActionResult> Create([Bind("StudioName")] Studio studio)
        {

            _service.Add(studio);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var studioDetails = await _service.GetById(id);

            if (studioDetails == null) return View("NotFound"); //Handeling errors

            return View(studioDetails); //Assigning view to view details
        }

        public async Task<IActionResult> Edit(int id)
        {

            var studioDetails = await _service.GetById(id);

            if (studioDetails == null) return View("NotFound"); //Handeling errors

            return View(studioDetails); //Assigning view to edit
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Studio studio)
        {

            studio.StudioNumber = Convert.ToUInt32(id);
            await _service.Update(id, studio);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound"); //Handeling errors

            return View(producerDetails); //Assigning view to delete
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetById(id);
            if (producerDetails == null) return View("NotFound"); //Handeling errors

            await _service.Delete(id);
            return RedirectToAction(nameof(Index)); //Assigning view to delete
        }
    }
}
