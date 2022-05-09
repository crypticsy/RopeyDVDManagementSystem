using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
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

        public IActionResult Create() //Returning the create view
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorFirstName, ActorSurName")] Actor actor) //Creating a new actor
        {

            _service.Add(actor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) //Displaying actor details
        {
            var actorDetails = await _service.GetById(id);

            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }

        public async Task<IActionResult> Edit(int id) //Displaying the update page for actor
        {

            var actorDetails = await _service.GetById(id);

            if (actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor) //Updating an existing actor
        {

            actor.ActorNumber = Convert.ToUInt32(id);
            await _service.Update(id, actor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) //Displaying the delete page for an actor
        {

            var actorDetails = await _service.GetById(id);

            if ( actorDetails == null) return View("NotFound");

            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) //Deleting an existing actor
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

