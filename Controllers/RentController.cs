using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController(ApplicationDbContext context)
        {
            _context = context;
        }

         public IEnumerable<RentModel> GetAllAvailableCopy()
        {
            IEnumerable<RentModel> loanRecord = (   from dt in _context.DVDTitles
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
            // Get all the available records
            IEnumerable<RentModel> availableCopy = GetAllAvailableCopy();

            // Get a list of all Copy Number that are on available
            ViewBag.AvailableCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Where(x=>x.IsOnLoan==false).Select(x => x.CopyNumber).Distinct().ToList());

            return View(availableCopy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string request)
        {
            string CopyNumber = Request.Form["SearchCopyNumber"];
            ViewBag.SearchCopyNumber = CopyNumber;

            // Get a list of all Copy Number that are on available
            ViewBag.AvailableCopyNumberList = (string)System.Text.Json.JsonSerializer.Serialize(_context.DVDCopies.Where(x=>x.IsOnLoan==false).Select(x => x.CopyNumber).Distinct().ToList());

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
                return View(loanRecord);
            }
            else if (CopyNumber == "")
            {
                // Get all the loan records
                IEnumerable<RentModel> loanRecord = GetAllAvailableCopy();
                return View(loanRecord);
            }
            else
            {
                return View();
            }

        }


        public IActionResult Create(int id)
        {
            if (id == 0 || _context.DVDCopies.Where(l => l.CopyNumber == id && l.IsOnLoan == false).Count() == 0 ){return RedirectToAction("Index");}

            // Get the copy detials
            RentModel currentLoan =    (from dt in _context.DVDTitles
                                        join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                        join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                        where dc.CopyNumber == id
                                        select new RentModel{   CopyNumber = dc.CopyNumber,
                                                                DVDTitleName = dt.DVDTitleName,
                                                                DVDCategory = dtc.CategoryName,
                                                                AgeRestricted = dtc.AgeRestricted
                                                            }).First();

            ViewData["MemberNumberList"] = from m in _context.Members select new SelectViewModel{ SelectValue = (int)m.MemberNumber, SelectKey = m.MemberFirstName + " " + m.MemberLastName};
            ViewData["LoanTypeList"] = from lt in _context.LoanTypes select new SelectViewModel{ SelectValue = (int)lt.LoanTypeNumber, SelectKey = lt.LoanTypeTitle};
            ViewData["AvailableDVD"] = currentLoan;
            ViewBag.CopyNumber = currentLoan.CopyNumber;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LoanTypeNumber, CopyNumber, MemberNumber")] RentModel rent)
        {
            // Get the copy detials
            RentModel currentLoan =    (from dt in _context.DVDTitles
                                        join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                        join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                        where dc.CopyNumber == rent.CopyNumber
                                        select new RentModel{   CopyNumber = dc.CopyNumber,
                                                                DVDTitleName = dt.DVDTitleName,
                                                                DVDCategory = dtc.CategoryName,
                                                                AgeRestricted = dtc.AgeRestricted
                                                            }).First();

            ViewData["MemberNumberList"] = from m in _context.Members select new SelectViewModel{ SelectValue = (int)m.MemberNumber, SelectKey = m.MemberFirstName + " " + m.MemberLastName};
            ViewData["LoanTypeList"] = from lt in _context.LoanTypes select new SelectViewModel{ SelectValue = (int)lt.LoanTypeNumber, SelectKey = lt.LoanTypeTitle};
            ViewData["AvailableDVD"] = currentLoan;

            ViewBag.LoanTypeNumber = rent.LoanTypeNumber;
            ViewBag.MemberNumber = rent.MemberNumber;
            ViewBag.CopyNumber = rent.CopyNumber;
            
            string operation;
            try{
                operation = Request.Form["GenerateButton"];
            }
            catch{
                operation = "";
            }

            // Check for age restriction
            bool ageRestriction =   (from dc in _context.DVDCopies
                                    join d in _context.DVDTitles on dc.DVDNumber equals d.DVDNumber
                                    join dtc in _context.DVDCategories on d.CategoryNumber equals dtc.CategoryNumber
                                    where dc.CopyNumber == rent.CopyNumber
                                    select dtc.AgeRestricted).First();

            if ( ageRestriction == true &&  (DateTime.Today - _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberDateOfBirth).Days / 365 < 18)
            {
                TempData["Error"] = "The Specified Member is below the age restriction for this DVD";
                return View();
            }

            // Check for maximum loan limit
            uint maxRentLimit = (from m in _context.Members
                                join mt in _context.MembershipCategories on m.MembershipCategoryNumber equals mt.MembershipCategoryNumber
                                where m.MemberNumber == rent.MemberNumber
                                select mt.MembershipCategoryTotalLoans).FirstOrDefault();

            if ( _context.Loans.Where(l => l.DateReturned == DateTime.MinValue && l.MemberNumber == rent.MemberNumber).Count() >= maxRentLimit )
            {
                TempData["Error"] = "The Specified Member has reached the maximum number of loans";
                return View();
            }
            

            // Display the loan information
            uint loanDays = (from lt in _context.LoanTypes
                            where lt.LoanTypeNumber == rent.LoanTypeNumber
                            select lt.LoanDuration).First();

            DVDReturnModel returnModel = (  from dt in _context.DVDTitles
                                            join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                            join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                            where dc.CopyNumber == rent.CopyNumber
                                            select new DVDReturnModel { CopyNumber = dc.CopyNumber,
                                                                        DVDTitleName = dt.DVDTitleName,
                                                                        DVDCategory = dtc.CategoryName,
                                                                        DateOut = DateTime.Today,
                                                                        DateDue = DateTime.Today.AddDays(loanDays),
                                                                        MemberName = _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberFirstName + " " + _context.Members.Where(m => m.MemberNumber == rent.MemberNumber).First().MemberLastName,
                                                                        StandardCharge = dt.StandardCharge, 
                                                                        Payment = dt.StandardCharge * loanDays,
                                                                    }).First();
            ViewData["RentalInformation"] = returnModel;

            if (operation != "Generate")
            {
                // add a new loan record
                Loan loan = new Loan { CopyNumber = rent.CopyNumber,
                                        MemberNumber = (uint)rent.MemberNumber,
                                        LoanTypeNumber = (uint)rent.LoanTypeNumber,
                                        DateOut = DateTime.Today,
                                        DateDue = DateTime.Today.AddDays(loanDays),
                                        DateReturned = DateTime.MinValue,
                                        ReturnAmount = returnModel.Payment
                                    };
                _context.Loans.Add(loan);

                // Put the copy status to unavailable
                var Copy = _context.DVDCopies.Where(c => c.CopyNumber == rent.CopyNumber).First();
                Copy.IsOnLoan = true;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
