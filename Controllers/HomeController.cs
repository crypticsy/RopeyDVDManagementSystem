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
            // Get all the request DVD information from the database
            var dvdDetails =from dvd in _context.DVDTitles
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
                                                            ).ToList()
                                                        ),
                                AvailableQuantity =    _context.DVDCopies.Where(d => d.DVDNumber == dvd.DVDNumber).Count() == 0 ?
                                                        -1 :
                                                        (from dvdCopy in _context.DVDCopies
                                                        where dvdCopy.DVDNumber == dvd.DVDNumber
                                                        select dvdCopy.IsOnLoan? 0 : 1 ).Sum()
                            };
            return View(dvdDetails);
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