using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
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
            return View(); //Assigning view to add new Producer
        }

        //Request to post data 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProducerName")] Producer producer)
        {

            _service.Add(producer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound"); //Handeling errors

            return View(producerDetails); //Assigning view to view details
        }

        public async Task<IActionResult> Edit(int id)
        {

            var producerDetails = await _service.GetById(id);

            if (producerDetails == null) return View("NotFound"); //Handeling errors

            return View(producerDetails); //Assigning view to edit
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {

            producer.ProducerNumber = Convert.ToUInt32(id);
            await _service.Update(id, producer);

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
            return RedirectToAction(nameof(Index));
        }
    }
}
