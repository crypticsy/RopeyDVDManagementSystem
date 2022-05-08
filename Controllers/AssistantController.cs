using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RopeyDVDManagementSystem.Controllers
{
    [Authorize(Roles = "Manager")]
    public class AssistantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AssistantController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }



        public async Task<IActionResult> Index()
        {
            // get all the assistants and display them
            var assistantList = await _userManager.GetUsersInRoleAsync("Assistant");

            // Get a list of all Assistants First and Last Names
            var assistants = assistantList.Select(x => x.FirstName).ToList();
            var assistantsLastNames = assistantList.Select(x => x.LastName).ToList();
            assistants.AddRange(assistantsLastNames);
            ViewBag.AssistantSearchList = (string)System.Text.Json.JsonSerializer.Serialize(assistants);

            return View(assistantList);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string request)
        {
            string searchAssistantUser = Request.Form["SearchAssistant"];
            ViewBag.SearchAssistant = searchAssistantUser;

            // get all the assistants and display them
            var assistantList = await _userManager.GetUsersInRoleAsync("Assistant");

            // Get a list of all Assistants First and Last Names
            var assistants = assistantList.Select(x => x.FirstName).ToList();
            var assistantsLastNames = assistantList.Select(x => x.LastName).ToList();
            assistants.AddRange(assistantsLastNames);
            ViewBag.AssistantSearchList = (string)System.Text.Json.JsonSerializer.Serialize(assistants);

            if (searchAssistantUser == ""){
                return View(assistantList);
            
            } else if (assistantList.Where(x => x.UserName == searchAssistantUser).Count() > 0) {
                return View(assistantList.Where(x => x.UserName == searchAssistantUser));
            }
            
            return View();
        }




        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRegisterModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                TempData["Error"] = "Invalid credentials. Please, try again!";
                return View(model);
            }
            
            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (await _roleManager.RoleExistsAsync(UserRoles.Assistant))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Assistant);
            }

            return RedirectToAction("Index");
        }



        public IActionResult Edit(string Username)
        {
            var user = _userManager.FindByNameAsync(Username).Result;
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            var model = new UserRegisterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
            };

            ViewBag.Password = user.PasswordHash;
            ViewBag.OldUser = user.UserName;

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var oldUserName = Request.Form["OldUser"];
            
            // Check if the user name is same
            if (oldUserName == model.Username)
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    TempData["Error"] = "Invalid credentials. Please, Enter a different Username!";
                    return View(model);
                }
            }

            var user = _userManager.FindByNameAsync(oldUserName).Result;
            
            // Check if password is same
            // dehash password
            if(_userManager.CheckPasswordAsync(user, model.Password).Result)
            {
                _userManager.RemovePasswordAsync(user).Wait();
                await _userManager.AddPasswordAsync(user, model.Password);
            }

            user = _userManager.FindByNameAsync(oldUserName).Result;
            // Update User Info
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;
            _userManager.UpdateAsync(user).Wait();
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User update failed! Please check user details and try again." });            

            return RedirectToAction("Index");
        }

        



        public IActionResult Delete(string Username)
        {
            var user = _userManager.FindByNameAsync(Username).Result;
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found!" });

            _userManager.DeleteAsync(user).Wait();

            return RedirectToAction("Index");
        }

    }
}
