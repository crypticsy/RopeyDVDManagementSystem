using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;
using System.Diagnostics;

namespace RopeyDVDManagementSystem.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Index()
        {
            // Find the most popular dvds and get the top 3
            var dvdPopularityList = (from dvdLoan in _context.Loans
                                     join dvdCopy in _context.DVDCopies on dvdLoan.CopyNumber equals dvdCopy.CopyNumber
                                     group dvdCopy by dvdCopy.DVDNumber into dvdGroup
                                     orderby dvdGroup.Count() descending
                                     select new { DVDNumber = dvdGroup.Key, Copies = dvdGroup.Count() });
            
            var topDVDs = dvdPopularityList.Take(3);
            var remainigDVDs = dvdPopularityList.Skip(3);
            
            // get details of all top DVDs
            var topDVDsDetails = from topDVD in topDVDs 
                                 join dvd in _context.DVDTitles on topDVD.DVDNumber equals dvd.DVDNumber
                                 select new DVDPreviewModel
                                 {
                                    DVDTitleName = dvd.DVDTitleName,
                                    DVDPoster = dvd.DVDPoster,
                                    StandardCharge = dvd.StandardCharge,
                                    DVDType = "top"
                                 };
            
            // get details of the latest released DVDs
            var remainigDVDsDetails = from remainigDVD in remainigDVDs
                                      join dvd in _context.DVDTitles on remainigDVD.DVDNumber equals dvd.DVDNumber
                                      orderby dvd.DateReleased descending
                                      select new DVDPreviewModel
                                      {
                                        DVDTitleName = dvd.DVDTitleName,
                                        DVDPoster = dvd.DVDPoster,
                                        StandardCharge = dvd.StandardCharge,
                                        DVDType = "hot"
                                      };

            // join the two lists together
            topDVDsDetails = topDVDsDetails.Concat(remainigDVDsDetails.Take(3));

            return View(topDVDsDetails);
        }



        public IActionResult Shop()
        {
            // Get filter options from session
            var searchTerm = HttpContext.Session.GetString("searchTerm");
            var sortBy = HttpContext.Session.GetString("sortBy");
            var availability = HttpContext.Session.GetString("Availability");
            var ageRestricted = HttpContext.Session.GetString("AgeRestricted");
            var dvdCategory = HttpContext.Session.GetString("DVDCategory");

            var allDVDList = from dvd in _context.DVDTitles select dvd.DVDNumber;

            // filter dvd list by age restriction
            switch (ageRestricted)
            {
                case "yes":
                    allDVDList = from dvd in allDVDList
                                join dc in _context.DVDCategories on dvd equals dc.CategoryNumber
                                where dc.AgeRestricted == false
                                select dvd;
                    break;
            }

            // filter dvd list by category
            if (!string.IsNullOrEmpty(dvdCategory) && dvdCategory != "all")
                allDVDList = from allList in allDVDList
                            join dvd in _context.DVDTitles on allList equals dvd.DVDNumber
                            join dc in _context.DVDCategories on dvd.CategoryNumber equals dc.CategoryNumber
                            where dc.CategoryName.ToLower() == dvdCategory.ToLower()
                            select allList;



            // Initalizaiton of all remaining Viewbags
            ViewBag.SortBy = string.IsNullOrEmpty(sortBy) ? "na" : sortBy;
            ViewBag.Availability = string.IsNullOrEmpty(availability) ? "all" : availability;
            ViewBag.AgeRestricted = string.IsNullOrEmpty(ageRestricted) ? "no" : ageRestricted;
            ViewBag.DVDCategory = string.IsNullOrEmpty(dvdCategory) ? "all" : dvdCategory;

            // Get all the request DVD information from the database
            IEnumerable<DVDPreviewModel> dvdDetails =   from allDVD in allDVDList
                                                        join dvd in _context.DVDTitles on allDVD equals dvd.DVDNumber 
                                                        join category in _context.DVDCategories on dvd.CategoryNumber equals category.CategoryNumber
                                                        select new DVDPreviewModel
                                                        {
                                                            DVDTitleName = dvd.DVDTitleName,
                                                            DVDPoster = dvd.DVDPoster,
                                                            DVDCategory = category.CategoryName,
                                                            StandardCharge = dvd.StandardCharge,
                                                            DateReleased = dvd.DateReleased,
                                                            CastMember = string.Join(   ", " , 
                                                                                        (   from castMember in _context.CastMembers 
                                                                                            where castMember.DVDNumber == dvd.DVDNumber 
                                                                                            select string.Concat(castMember.Actor.ActorFirstName, " ", castMember.Actor.ActorSurName)
                                                                                        ).ToArray()
                                                                                    ),
                                                            AvailableQuantity =    _context.DVDCopies.Where(d => d.DVDNumber == dvd.DVDNumber).Count() == 0 ?
                                                                                    -1 :
                                                                                    (from dvdCopy in _context.DVDCopies
                                                                                    where dvdCopy.DVDNumber == dvd.DVDNumber
                                                                                    select dvdCopy.IsOnLoan? 0 : 1 ).Sum()
                                                        };

            // fitler dvd list by search term if there is one
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;
                dvdDetails = dvdDetails.Where(d => d.CastMember.ToLower().Contains(searchTerm.ToLower()) || d.DVDTitleName.ToLower().Contains(searchTerm.ToLower()));
            }

            // filter dvd list by availability
            switch (availability)
            {
                case "available":
                    dvdDetails = dvdDetails.Where(d => d.AvailableQuantity > 0);
                    break;

                case "unavailable":
                    dvdDetails = dvdDetails.Where(d => d.AvailableQuantity == 0);
                    break;
                
                case "comingSoon":
                    dvdDetails = dvdDetails.Where(d => d.AvailableQuantity == -1);
                    break;
            }
            
            // Sort the list by the selected option
            switch (sortBy)
            {
                case "pa":
                    dvdDetails = dvdDetails.OrderBy(d => d.StandardCharge);
                    break;
                
                case "pd":
                    dvdDetails = dvdDetails.OrderByDescending(d => d.StandardCharge);
                    break;

                case "nd":
                    dvdDetails = dvdDetails.OrderByDescending(d => d.DVDTitleName);
                    break;
                
                default:
                    dvdDetails = dvdDetails.OrderBy(d => d.DVDTitleName);
                    break;
            }

            return View(dvdDetails);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostShop()
        {
            // Save filter options in session
            HttpContext.Session.SetString("searchTerm", Request.Form["SearchTerm"]);
            HttpContext.Session.SetString("sortBy", Request.Form["sortBy"]);
            HttpContext.Session.SetString("Availability", Request.Form["Availability"]);
            HttpContext.Session.SetString("AgeRestricted", Request.Form["AgeRestricted"]);
            HttpContext.Session.SetString("DVDCategory", Request.Form["DVDCategory"]);

            return RedirectToAction("Shop");
        }



        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}