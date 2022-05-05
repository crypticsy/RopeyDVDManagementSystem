using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models;
using System.Diagnostics;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            int MaximumUnits = 5;

            ViewBag.DVDCount = _context.DVDTitles.Count();
            ViewBag.UserCount = _context.Users.Count();
            ViewBag.MemberCount = _context.Members.Count();

            // get all the dvd category names and their counts
            var dvdCategoryCounts = (   from dvdTitle in _context.DVDTitles
                                        group dvdTitle by dvdTitle.DVDCategory.CategoryName into dvdCategoryGroup
                                        select new { Category = dvdCategoryGroup.Key, Count = dvdCategoryGroup.Count() })
                                    .OrderBy(x => x.Count).Reverse().Take(MaximumUnits);

            ViewBag.DVDCategoryLabels = System.Text.Json.JsonSerializer.Serialize(dvdCategoryCounts.Select(x => x.Category).ToList());
            ViewBag.DVDCategoryData = System.Text.Json.JsonSerializer.Serialize(dvdCategoryCounts.Select(x => x.Count).ToList());



            // find all the months for the current year based on current date
            var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            months = months.Take(DateTime.Now.Month).ToArray();
            var currentMonthName = months[DateTime.Now.Month - 1];
            var currentYear = DateTime.Now.Year;

            // get total loans for the current year based on each month
            var loansByMonth = new List<int>();
            for (int i = 0; i < months.Length; i++)
            {
                var month = months[i];
                var loans = _context.Loans.Where(x => x.DateOut.Month == i + 1 && x.DateOut.Year == currentYear).Sum(x => x.ReturnAmount);
                loansByMonth.Add( (int)Math.Ceiling((decimal)loans) );
            }

            ViewBag.LoansByMonth = (string)System.Text.Json.JsonSerializer.Serialize(loansByMonth);
            ViewBag.MonthLabels = (string)System.Text.Json.JsonSerializer.Serialize(months);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}