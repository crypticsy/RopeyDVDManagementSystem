using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController(ApplicationDbContext context)
        {
            _context = context;
        }

         public IEnumerable<RentModel> GetAllLoanRecords()
        {
            // All list of DVD Copy that are on loan
            IEnumerable<RentModel> loanRecord = (from dt in _context.DVDTitles
                                                      join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                                      join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                                      where dc.IsOnLoan == false
                                                      select new RentModel
                                                      {
                                                          CopyNumber = dc.CopyNumber,
                                                          DVDTitleName = dt.DVDTitleName,
                                                          DVDCategory = dtc.CategoryName,
                                                          AgeRestricted = dtc.AgeRestricted
                                                      });
            return loanRecord;
        }

        public IActionResult Index()
        {
            // Get all the loan records
            IEnumerable<RentModel> loanRecord = GetAllLoanRecords();

            // Get a list of all DVD Copy that are on loan
            ViewBag.LoanedCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Select(x => x.CopyNumber).Distinct().ToList());

            return View(loanRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string request)
        {
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            // Get a list of all DVD Copy that are on loan
            ViewBag.LoanedCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Select(x => x.CopyNumber).Distinct().ToList());

            if (CopyNumber != null &&
                int.TryParse(CopyNumber, out int copyNumber) &&
                _context.DVDCopies.Where(x => x.CopyNumber == copyNumber).Count() > 0)
            {
                var loanRecord = (from dt in _context.DVDTitles
                                  join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                  join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                  where dc.IsOnLoan == false && dc.CopyNumber == copyNumber
                                  select new RentModel
                                  {
                                      CopyNumber = dc.CopyNumber,
                                      DVDTitleName = dt.DVDTitleName,
                                      DVDCategory = dtc.CategoryName,
                                      AgeRestricted = dtc.AgeRestricted
                                  });

                if (loanRecord.Count() > 0)
                {
                    var loanRecordFirst = loanRecord.First();
                    //if (loanRecordFirst.DateReturned != DateTime.MinValue) loanRecordFirst.OverDue = 1;
                    //ViewData["LoanRecord"] = loanRecordFirst;
                }
                return View();
            }
            else if (CopyNumber == "")
            {
                // Get all the loan records
                IEnumerable<RentModel> loanRecord = GetAllLoanRecords();
                return View(loanRecord);
            }
            else
            {
                return View();
            }

        }


        public IActionResult Create()
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

            _context.AddAsync(loan);

            return RedirectToAction(nameof(Index));
        }




        

    }
}
