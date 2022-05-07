using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMembersService _service;

        public MembersController(IMembersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("MemberFirstName, MemberLastName, MembershipCategoryNumber, MemberAddress, MemberDateofBirth")]Member member)
        {
            if (ModelState.IsValid)
            {
                return View(member);
            }
            await _service.AddAsync(member);
            return RedirectToAction(nameof(Index));
        }

        //Get: Members/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var memberDetails = await _service.GetByIdAsync(id);
            if (memberDetails == null) return View("Not Found");
            return View(memberDetails);
        }
    }
}
