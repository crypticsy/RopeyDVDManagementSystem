using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDCopyController : Controller
    {
        private readonly IDVDCopyService _service;

        public DVDCopyController(IDVDCopyService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            ViewBag.AllCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(data.Select(x => x.CopyNumber).ToList());
            return View(data);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string request)
        {
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            var data = await _service.GetAllAsync();
            ViewBag.AllCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(data.Select(x => x.CopyNumber).ToList());

            if (CopyNumber == "")
            {
                return View(data);
            }
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
            return View();
        }

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
            var dvdCopyDetails = await _service.GetByIdAsync(id);
            if (dvdCopyDetails == null) return View("Not Found");
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
            if (dvdCopyDetails == null) return View("Not Found");
            return View(dvdCopyDetails);
        }

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
            if (dvdCopyDetails == null) return View("Not Found");
            return View(dvdCopyDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dvdCopyDetails = await _service.GetByIdAsync(id);
            if (dvdCopyDetails == null) return View("Not Found");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
