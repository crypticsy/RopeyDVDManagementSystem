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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("DVDPoster,DVDTitleName,CategoryNumber,StudioNumber,ProducerNumber,DateReleased,CastMembers,StandardCharge,PenaltyCharge")] DVDTitle dvdTitle) //ADD DVCOPYS HERE
        {

            //if (!ModelState.IsValid) 
            //{
            //    var dvdTitleDropdownsData = await _service.GetDVDTitleDropdownValues();

            //    ViewBag.ActorNumber = new SelectList(dvdTitleDropdownsData.Actors, "ActorNumber", "ActorFirstName");
            //    ViewBag.CategoryNumber = new SelectList(dvdTitleDropdownsData.Categories, "CategoryNumber", "CategoryName"); //Returning the data for the dropdowns to the views through viewbags
            //    ViewBag.ProducerNumber = new SelectList(dvdTitleDropdownsData.Producers, "ProducerNumber", "ProducerName");
            //    ViewBag.StudioNumber = new SelectList(dvdTitleDropdownsData.Studios, "StudioNumber", "StudioName");
                
            //    return View(dvdTitle);
            //}

            _service.Add(dvdTitle);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var dvdTitleDetails = await _service.GetById(id);

            if (dvdTitleDetails == null) return View("NotFound");

            return View(dvdTitleDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var dvdTitleDetails = await _service.GetById(id);

            if (dvdTitleDetails == null) return View("NotFound");

            return View(dvdTitleDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DVDTitle dvdTitle)
        {
            if (!ModelState.IsValid)
            {
                return View(dvdTitle);
            }
            dvdTitle.DVDNumber = Convert.ToUInt32(id);
            await _service.Update(id, dvdTitle);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var dvdTitleDetails = await _service.GetById(id);

            if (dvdTitleDetails == null) return View("NotFound");

            return View(dvdTitleDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dvdTitleDetails = await _service.GetById(id);
            if (dvdTitleDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
