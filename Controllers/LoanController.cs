using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _service;

        public LoanController(ILoanService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAll();
            return View(data);
        }

        //Get:Actors/Create
        public  IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("LoanTypeNumber, CopyNumber, MemberNumber")] Loan loan)
        {


            if (ModelState.IsValid)
            {
                return View(loan);
            }

            _service.AddAsync(loan);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var loanDetails = await _service.GetById(id);

            if (loanDetails == null) return View("NotFound");

            return View(loanDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var loanDetails = await _service.GetById(id);

            if (loanDetails == null) return View("NotFound");

            return View(loanDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Loan loan)
        {


            loan.LoanNumber = Convert.ToUInt32(id);
            await _service.Update(id, loan);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var loanDetails = await _service.GetById(id);

            if (loanDetails == null) return View("NotFound");

            return View(loanDetails);
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
