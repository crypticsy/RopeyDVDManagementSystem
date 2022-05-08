using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _service;

        public ActorController(IActorService service)
        {
            _service = service; //Assigning the service
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll(); //Assigning all Actor table data to variable 'data'
            return View(data);
        }

        public IActionResult Create()
        {
            return View(); //Assigning view to add new Actor
        }


        //Request to post data 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorFirstName, ActorSurName")] Actor actor)
        {
            _service.Add(actor);

            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetById(id); //Assigning selected Actor table data to variable 'actorDetails'

            if (actorDetails == null) return View("NotFound"); //Handeling errors

            return View(actorDetails); //Assigning view to view details
        }

        public async Task<IActionResult> Edit(int id)
        {

            var actorDetails = await _service.GetById(id); 

            if (actorDetails == null) return View("NotFound"); //Handeling errors

            return View(actorDetails); //Assigning view to edit
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {

            actor.ActorNumber = Convert.ToUInt32(id); 
            await _service.Update(id, actor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetById(id);

            if ( actorDetails == null) return View("NotFound"); //Handeling errors

            return View(actorDetails); //Assigning view to delete
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index)); //Assigning view to delete
        }
    }
}

