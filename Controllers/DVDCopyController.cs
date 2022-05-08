using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDCopyController : Controller
    {
        private readonly IDVDCopyService _service; //Assigning the service

        public DVDCopyController(IDVDCopyService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(); //Assigning all Actor table data to variable 'data'
            ViewBag.AllCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(data.Select(x => x.CopyNumber).ToList());
            return View(data);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string request)
        {
            //Search through input 
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            var data = await _service.GetAllAsync();
            ViewBag.AllCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(data.Select(x => x.CopyNumber).ToList());

            //View all data on search field empty 
            if (CopyNumber == "")
            {
                return View(data);
            }
            //View matched data on search field value entered
            else if (uint.TryParse(CopyNumber,out uint result) && data.Where(x => x.CopyNumber == result).Count() > 0)
            {   
                data = data.Where(x => x.CopyNumber == result).ToList();
                return View(data);
            }
            return View();
        }

        //Get: DVDCopy/Create
        public IActionResult Create()
        {
            return View(); //Assigning view to add new Actor
        }

        //Request to post data 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DVDNumber, DVDTitle, DatePurchased")] DVDCopy dvdcopy)
        {
            if (ModelState.IsValid)
            {
                return View(dvdcopy);
            }
            await _service.AddAsync(dvdcopy);
            return RedirectToAction(nameof(Index));
        }

        //Get: Members/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var dvdCopyDetails = await _service.GetByIdAsync(id); //Assigning selected Actor table data to variable 'dvdCopyDetails'
            if (dvdCopyDetails == null) return View("Not Found"); //Handeling errors
            return View(dvdCopyDetails);
        }

        public async Task<IActionResult> OldDVDCopy()
        {
            var data =  (from d in await _service.GetAllAsync()
                        where d.DatePurchased < DateTime.Now.AddYears(-1)
                        select d).ToList();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OldDVDCopy(string Request)
        {
            var data =  (from d in await _service.GetAllAsync()
                        where d.DatePurchased < DateTime.Now.AddYears(-1) && d.IsOnLoan == false
                        select d).ToList();
            
            // Delete all DVDCopy that are older than 1 year
            foreach (var d in data)
            {
                await _service.DeleteAsync((int)d.CopyNumber);
            }
            return RedirectToAction(nameof(Index));
        }

        //Get: Members/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var dvdCopyDetails = await _service.GetByIdAsync(id);
            if (dvdCopyDetails == null) return View("Not Found"); //Handeling errors
            return View(dvdCopyDetails);
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("DVDNumber, DVDTitle, DatePurchased")] DVDCopy dvdcopy)
        {
            if (ModelState.IsValid)
            {
                return View(dvdcopy);
            }
            dvdcopy.CopyNumber = Convert.ToUInt32(id);
            await _service.UpdateAsync(id, dvdcopy);
            return RedirectToAction(nameof(Index));
        }

        //Get: Members/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var dvdCopyDetails = await _service.GetByIdAsync(id);
            if (dvdCopyDetails == null) return View("Not Found"); //Handeling errors
            return View(dvdCopyDetails);
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dvdCopyDetails = await _service.GetByIdAsync(id);
            if (dvdCopyDetails == null) return View("Not Found"); //Handeling errors
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
