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
            ViewBag.StudentCount = _context.DVDTitles.Count();
            ViewBag.UserCount = _context.Users.Count();
            ViewBag.MemberCount = _context.Members.Count();

            // get all the dvd category names and their counts
            var dvdCategoryCounts = (   from dvdTitle in _context.DVDTitles
                                        group dvdTitle by dvdTitle.DVDCategory.CategoryDescription into dvdCategoryGroup
                                        select new { Category = dvdCategoryGroup.Key, Count = dvdCategoryGroup.Count() })
                                    .OrderBy(x => x.Count);

            List<string> dVDCategoryLabels = dvdCategoryCounts.Select(x => x.Category).Skip(Math.Max(0, dvdCategoryCounts.Count() - 5)).ToList();
            ViewBag.DVDCategoryLabels = (string) System.Text.Json.JsonSerializer.Serialize(dVDCategoryLabels);

            List<int> dvdCategoryData = dvdCategoryCounts.Select(x => x.Count).Skip(Math.Max(0, dvdCategoryCounts.Count() - 5)).ToList();
            ViewBag.DVDCategoryData = (string)System.Text.Json.JsonSerializer.Serialize(dvdCategoryData);



            // find all the months for the current year based on current date
            var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var currentMonthName = months[System.DateTime.Now.Month - 1];


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}