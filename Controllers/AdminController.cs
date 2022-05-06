using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.Identity;
using System.Diagnostics;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        private ApplicationDbContext _context;

        public AdminController( UserManager<ApplicationUser> userManager,
                                IConfiguration configuration,
                                ILogger<AdminController> logger, 
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
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



        public IActionResult Profile()
        {
            // Get current user details from the database
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            
            // Display the user details
            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.UserRole =  (from role in _context.Roles
                                join userRole in _context.UserRoles on role.Id equals userRole.RoleId
                                where userRole.UserId == user.Id
                                select role.Name).FirstOrDefault();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(PasswordChangeModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            // Get current user details from the database
            var user = _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            // Change the password for the current user
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            
            // Display the user details
            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.UserRole =  (from role in _context.Roles
                                join userRole in _context.UserRoles on role.Id equals userRole.RoleId
                                where userRole.UserId == user.Id
                                select role.Name).FirstOrDefault();

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Current Password");
                return View(model);
            }

            ViewBag.IsSuccess = true;
            ModelState.Clear();
            return View(model);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}