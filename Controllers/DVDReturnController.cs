using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDReturnController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDReturnController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DVDReturnModel> GetAllLoanRecords()
        {
            // All list of DVD Copy that are on loan
            IEnumerable<DVDReturnModel> loanRecord =    from dt in _context.DVDTitles
                                                        join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                                        join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                                                        join m in _context.Members on l.MemberNumber equals m.MemberNumber
                                                        where l.DateReturned == DateTime.MinValue
                                                        orderby l.DateOut descending, dt.DVDTitleName
                                                        select new DVDReturnModel { CopyNumber = dc.CopyNumber,
                                                                                    DVDTitleName = dt.DVDTitleName,
                                                                                    DateOut = l.DateOut,
                                                                                    DateDue = l.DateDue,
                                                                                    MemberName = m.MemberFirstName + ' ' + m.MemberLastName,
                                                                                    TotalLoan = (int)(from la in _context.Loans
                                                                                                    join dc in _context.DVDCopies on l.CopyNumber equals dc.CopyNumber
                                                                                                    where la.DateOut == l.DateOut
                                                                                                    select la.LoanNumber).Count(),
                                                                                    LoanNumber = l.LoanNumber
                                                                                };
            return loanRecord;
        }

        public IActionResult Index()
        {
            // Get all the loan records
            IEnumerable<DVDReturnModel> loanRecord = GetAllLoanRecords();

            // Get a list of all DVD Copy that are on loan
            ViewBag.LoanedCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Select(x => x.CopyNumber).ToList());

            return View(loanRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string request)
        {
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            // Get a list of all DVD Copy that are on loan
            ViewBag.LoanedCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Select(x => x.CopyNumber).ToList());

            if (CopyNumber != null &&
                int.TryParse(CopyNumber, out int copyNumber) &&
                _context.DVDCopies.Where(x => x.CopyNumber == copyNumber).Count() > 0)
            {
                var loanRecord = (  from dt in _context.DVDTitles
                                    join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                    join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                    join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                                    join m in _context.Members on l.MemberNumber equals m.MemberNumber
                                    orderby l.DateOut descending
                                    where dc.CopyNumber == copyNumber
                                    select new DVDReturnModel { CopyNumber = dc.CopyNumber,
                                                                DVDTitleName = dt.DVDTitleName,
                                                                DVDCategory = dtc.CategoryName,
                                                                DateOut = l.DateOut,
                                                                DateDue = l.DateDue,
                                                                DateReturned = l.DateReturned,
                                                                MemberName = m.MemberFirstName + ' ' + m.MemberLastName,
                                                                LoanNumber = l.LoanNumber, 
                                                                Payment =  l.ReturnAmount
                                    });

                if(loanRecord.Count() > 0)
                {
                    ViewData["LoanRecord"] = loanRecord.First();
                }
                return View();
            }
            else if (CopyNumber == "")
            {
                // Get all the loan records
                IEnumerable<DVDReturnModel> loanRecord = GetAllLoanRecords();
                return View(loanRecord);
            }
            else
            {
                return View();
            }

        }



        public IActionResult ReturnConfirmation(int LoanID)
        {
            if (LoanID == 0 || 
                _context.Loans.Where(l => l.LoanNumber == LoanID).Count() == 0 || 
                _context.Loans.Where(l => l.LoanNumber == LoanID).First().DateReturned != DateTime.MinValue)
            {
                return RedirectToAction("Index");
            }

            // Get the loan record
            DVDReturnModel currentLoan =    (from dt in _context.DVDTitles
                                            join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                            join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                            join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                                            join m in _context.Members on l.MemberNumber equals m.MemberNumber
                                            where l.LoanNumber == LoanID
                                            select new DVDReturnModel { CopyNumber = dc.CopyNumber,
                                                                        DVDTitleName = dt.DVDTitleName,
                                                                        DVDCategory = dtc.CategoryName,
                                                                        DateOut = l.DateOut,
                                                                        DateDue = l.DateDue,
                                                                        MemberName = m.MemberFirstName + ' ' + m.MemberLastName,
                                                                        LoanNumber = l.LoanNumber,
                                                                        StandardCharge = l.ReturnAmount,
                                                                        PenaltyCharge = dt.PenaltyCharge
                                                                    }).First();

            var today = DateTime.Today;
            var overDueDays = (today - currentLoan.DateDue).TotalDays;
            if (overDueDays < 0) overDueDays = 0;

            currentLoan.OverDue = (int)overDueDays;
            currentLoan.Payment = currentLoan.StandardCharge + currentLoan.PenaltyCharge * (decimal)overDueDays;
            currentLoan.PenaltyCharge = currentLoan.PenaltyCharge * (decimal)overDueDays;

            ViewData["ReturnDVD"] = currentLoan;
            return View();
        }

        [HttpPost]
        public IActionResult ReturnConfirmation(DVDReturnModel returnDVD)
        {
            if (returnDVD.LoanNumber == 0 || 
                returnDVD.CopyNumber == 0 || 
                _context.Loans.Where(l => l.LoanNumber == returnDVD.LoanNumber).Count() == 0 || 
                _context.DVDCopies.Where(dc => dc.CopyNumber == returnDVD.CopyNumber).Count() == 0 ||
                _context.DVDCopies.Where(dc => dc.CopyNumber == returnDVD.CopyNumber).First().IsOnLoan == false)
            {
                return RedirectToAction("Index");
            }

            // Update the loan record
            var loan = _context.Loans.Find(returnDVD.LoanNumber);
            loan.DateReturned = DateTime.Today;
            loan.ReturnAmount = returnDVD.Payment;
            
            // Update the DVD copy record
            var copy = _context.DVDCopies.Find(returnDVD.CopyNumber);
            copy.IsOnLoan = false;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
