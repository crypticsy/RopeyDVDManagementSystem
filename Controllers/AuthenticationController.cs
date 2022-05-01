using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.Identity;
using RopeyDVDManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace RopeyDVDManagementSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }




        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserDetails(UserDetailsViewModel userDetails)
        {
            return View(userDetails);
        }



        // GET: Authentication/Login
        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {

            if (!ModelState.IsValid) return View(loginModel);

            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                UserDetailsViewModel userDetails = new UserDetailsViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };

                Response.Cookies.Append(    "X-Access-Token",
                                            userDetails.Token,
                                            new CookieOptions
                                            {
                                                HttpOnly = true,
                                                SameSite = SameSiteMode.Strict
                                            });

                //ViewBag.User = userDetails;
                return RedirectToAction("UserDetails", userDetails);
            }

            TempData["Error"] = "Invalid credentials. Please, try again!";
            return View(loginModel);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return RedirectToAction("Index");
        }



        // GET: Authentication/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterModel model)
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

            if (model.UserType == "assistant")
            {
                if (await _roleManager.RoleExistsAsync(UserRoles.Assistant))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Assistant);
                }
            }
            else
            {
                if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Manager);
                }
            }

            return RedirectToAction("Index", "Home");
        }



        public IActionResult UnauthorizedAccess()
        {
            return View();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
