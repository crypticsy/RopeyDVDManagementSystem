using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDTitleController : Controller
    {
        private readonly IDVDTitleService _service;
        private readonly ApplicationDbContext _context;

        public DVDTitleController(IDVDTitleService service, ApplicationDbContext context)
        {
            _service = service; //Assigning the service
            _context = context; //Assigning the context
        }


        public IEnumerable<DVDPreviewModel> GetDVDDetails(){
            // Get all the request DVD information from the database
            IEnumerable<DVDPreviewModel> dvdDetails =   from  dvd in _context.DVDTitles
                                                        join studio in _context.Studios on dvd.StudioNumber equals studio.StudioNumber
                                                        join producer in _context.Producers on dvd.ProducerNumber equals producer.ProducerNumber
                                                        join category in _context.DVDCategories on dvd.CategoryNumber equals category.CategoryNumber
                                                        orderby dvd.DateReleased
                                                        select new DVDPreviewModel
                                                        {
                                                            DVDNumber = dvd.DVDNumber,
                                                            DVDTitleName = dvd.DVDTitleName,
                                                            DVDCategory = category.CategoryName,
                                                            ProducerName = producer.ProducerName,
                                                            StudioName = studio.StudioName,
                                                            DateReleased = dvd.DateReleased,
                                                            CastMembers = ( from castMember in _context.CastMembers 
                                                                            where castMember.DVDNumber == dvd.DVDNumber 
                                                                            orderby castMember.Actor.ActorSurName
                                                                            select castMember.Actor.ActorFirstName+" "+castMember.Actor.ActorSurName
                                                                        ).ToList(),
                                                            AvailableQuantity =    (from dvdCopy in _context.DVDCopies
                                                                                    where dvdCopy.DVDNumber == dvd.DVDNumber
                                                                                    select dvdCopy.IsOnLoan).Count()
                                                        };
            return dvdDetails;
        }



        public IActionResult Index()
        {
            var dvdDetails = GetDVDDetails();

            // Get a dvdTitles list
            ViewBag.DVDSearchList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDTitles.Select(x => x.DVDTitleName.ToString()).ToList());

            return View(dvdDetails);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string request)
        {
            var dvdDetails = GetDVDDetails();
            string searchDVD = Request.Form["SearchDVDTitle"];
            ViewBag.SearchDVDTitle = searchDVD;

            // Get a dvdTitles list
            ViewBag.DVDSearchList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDTitles.Select(x => x.DVDTitleName.ToString()).ToList());
            
            if (searchDVD == "")
            {
                return View(dvdDetails);
            }
            else if (_context.DVDTitles.Where(x => x.DVDTitleName.Contains(searchDVD)).Count() > 0)
            {
                // Get all member details
                var data = dvdDetails.Where(x => x.DVDTitleName.Contains(searchDVD));
                return View(data);
            }

            return View();
        }



        public IActionResult Inactive()
        {
            // Find the last loan date for all the dvds
            var dvdActivity =   from dv in _context.DVDTitles
                                    select new {
                                        DVDNumber = dv.DVDNumber,
                                        LastLoaned = (from dvdCopy in _context.DVDCopies
                                                        join loan in _context.Loans on dvdCopy.CopyNumber equals loan.CopyNumber
                                                         where dvdCopy.DVDNumber == dv.DVDNumber
                                                        orderby loan.DateOut descending
                                                        select loan.DateOut).FirstOrDefault(),
                                    };

            // Find the inactive DVD titles (where the last loan occured before 31 days)
            List<uint> inactiveDVD = dvdActivity.Where(x => x.LastLoaned != null && x.LastLoaned < DateTime.Now.AddDays(-31)).Select(x => x.DVDNumber).ToList();

            // Get all the request DVD information from the database
            IEnumerable<DVDPreviewModel> dvdDetails =   from  dvd in _context.DVDTitles
                                                        join dc in _context.DVDCopies on dvd.DVDNumber equals dc.DVDNumber
                                                        join studio in _context.Studios on dvd.StudioNumber equals studio.StudioNumber
                                                        join producer in _context.Producers on dvd.ProducerNumber equals producer.ProducerNumber
                                                        join category in _context.DVDCategories on dvd.CategoryNumber equals category.CategoryNumber
                                                        where inactiveDVD.Contains(dvd.DVDNumber)
                                                        orderby dvd.DateReleased
                                                        select new DVDPreviewModel
                                                        {
                                                            DVDNumber = dvd.DVDNumber,
                                                            DVDTitleName = dvd.DVDTitleName,
                                                            DVDCategory = category.CategoryName,
                                                            ProducerName = producer.ProducerName,
                                                            StudioName = studio.StudioName,
                                                            DateReleased = dvd.DateReleased,
                                                            LastLoanDate = dvdActivity.Where(x => x.DVDNumber == dvd.DVDNumber).Select(x => x.LastLoaned).First(),
                                                            CastMembers = ( from castMember in _context.CastMembers 
                                                                            where castMember.DVDNumber == dvd.DVDNumber 
                                                                            orderby castMember.Actor.ActorSurName
                                                                            select castMember.Actor.ActorFirstName+" "+castMember.Actor.ActorSurName
                                                                        ).ToList(),
                                                            AvailableQuantity =    (from dvdCopy in _context.DVDCopies
                                                                                    where dvdCopy.DVDNumber == dvd.DVDNumber
                                                                                    select dvdCopy.IsOnLoan).Count()
                                                        };

            return View(dvdDetails);
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
        public async Task<IActionResult> Create(NewDVDTitleVM dvdTitle) //ADD DVCOPYS HERE
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

            await _service.Add(dvdTitle);

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

            var response = new NewDVDTitleVM()
            {
                DVDNumber = dvdTitleDetails.DVDNumber,
                DVDTitleName = dvdTitleDetails.DVDTitleName,
                CategoryNumber = dvdTitleDetails.CategoryNumber,
                StudioNumber = dvdTitleDetails.StudioNumber,
                ProducerNumber = dvdTitleDetails.ProducerNumber,
                DVDPoster = dvdTitleDetails.DVDPoster,
                StandardCharge = dvdTitleDetails.StandardCharge,
                PenaltyCharge = dvdTitleDetails.PenaltyCharge,
                CastMembers = dvdTitleDetails.CastMembers.Select(n => n.ActorNumber).ToList()
            };


            var dvdTitleDropdownsData = await _service.GetDVDTitleDropdownValues();

            ViewBag.CategoryNumber = new SelectList(dvdTitleDropdownsData.Categories, "CategoryNumber", "CategoryName"); //Returning the data for the dropdowns to the views through viewbags
            ViewBag.ProducerNumber = new SelectList(dvdTitleDropdownsData.Producers, "ProducerNumber", "ProducerName");
            ViewBag.StudioNumber = new SelectList(dvdTitleDropdownsData.Studios, "StudioNumber", "StudioName");
            ViewBag.ActorNumber = new SelectList(dvdTitleDropdownsData.Actors, "ActorNumber", "ActorFullName");

            if (dvdTitleDetails == null) return View("NotFound");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewDVDTitleVM dvdTitle)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(dvdTitle);
            //}
            dvdTitle.DVDNumber = Convert.ToUInt32(id);
            await _service.Update(dvdTitle);

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
