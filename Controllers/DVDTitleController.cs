using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDTitleController : Controller
    {
        private readonly IDVDTitleService _service;

        public DVDTitleController(IDVDTitleService service, ApplicationDbContext context)
        {
            _service = service; //Assigning the service
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll(); //Assigning all DVDTitle table data to variable 'data'
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var dvdTitleDropdownsData = await _service.GetDVDTitleDropdownValues();


            ViewBag.CategoryNumber = new SelectList(dvdTitleDropdownsData.Categories, "CategoryNumber", "CategoryName"); //Returning the data for the dropdowns to the views through viewbags
            ViewBag.ProducerNumber = new SelectList(dvdTitleDropdownsData.Producers, "ProducerNumber", "ProducerName");
            ViewBag.StudioNumber = new SelectList(dvdTitleDropdownsData.Studios, "StudioNumber", "StudioName");
            ViewBag.ActorNumber = new SelectList(dvdTitleDropdownsData.Actors, "ActorNumber", "ActorFullName");

            return View(); //Assigning view to add new DVD Title
        }

        //Request to post data 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DVDPoster,DVDTitleName,CategoryNumber,StudioNumber,ProducerNumber,DateReleased,CastMembers,StandardCharge,PenaltyCharge")] DVDTitle dvdTitle) //ADD DVCOPYS HERE
        {

            _service.Add(dvdTitle);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dvdTitleDetails = await _service.GetById(id);

            if (dvdTitleDetails == null) return View("NotFound");//Handeling errors

            return View(dvdTitleDetails); //Assigning view to view details
        }

        public async Task<IActionResult> Edit(int id)
        {

            var dvdTitleDetails = await _service.GetById(id);
            var dvdTitleDropdownsData = await _service.GetDVDTitleDropdownValues();

            ViewBag.CategoryNumber = new SelectList(dvdTitleDropdownsData.Categories, "CategoryNumber", "CategoryName"); //Returning the data for the dropdowns to the views through viewbags
            ViewBag.ProducerNumber = new SelectList(dvdTitleDropdownsData.Producers, "ProducerNumber", "ProducerName");
            ViewBag.StudioNumber = new SelectList(dvdTitleDropdownsData.Studios, "StudioNumber", "StudioName");
            ViewBag.ActorNumber = new SelectList(dvdTitleDropdownsData.Actors, "ActorNumber", "ActorFullName");

            if (dvdTitleDetails == null) return View("NotFound");//Handeling errors

            return View(dvdTitleDetails); //Assigning view to edit
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, DVDTitle dvdTitle)
        {
            dvdTitle.DVDNumber = Convert.ToUInt32(id);
            await _service.Update(id, dvdTitle);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var dvdTitleDetails = await _service.GetById(id);

            if (dvdTitleDetails == null) return View("NotFound");//Handeling errors

            return View(dvdTitleDetails); //Assigning view to delete
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dvdTitleDetails = await _service.GetById(id);
            if (dvdTitleDetails == null) return View("NotFound");//Handeling errors

            await _service.Delete(id);
            return RedirectToAction(nameof(Index)); //Assigning view to delete
        }
    }
}
