using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
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

            if (studioDetails == null) return View("NotFound");

            return View(studioDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var studioDetails = await _service.GetById(id);

            if (studioDetails == null) return View("NotFound");

            return View(studioDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Studio studio)
        {

            studio.StudioNumber = Convert.ToUInt32(id);
            await _service.Update(id, studio);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetById(id);
            if (producerDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
