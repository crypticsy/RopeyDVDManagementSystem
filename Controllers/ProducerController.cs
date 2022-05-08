using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
    public class ProducerController : Controller
    {
        private readonly IProducerService _service;

        public ProducerController(IProducerService service)
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
        public async Task<IActionResult> Create([Bind("ProducerName")] Producer producer)
        {

            _service.Add(producer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {

            producer.ProducerNumber = Convert.ToUInt32(id);
            await _service.Update(id, producer);

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
