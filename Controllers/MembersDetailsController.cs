using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data.Services;

namespace RopeyDVDManagementSystem.Controllers
{
    public class MembersDetailsController : Controller
    {
        private readonly IMembersService _service;
        public MembersDetailsController(IMembersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllDetailsAsync();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var memberDetails = await _service.GetByIdAsync(id);
            if (memberDetails == null) return View("Not Found");
            return View(memberDetails);
        }
    }
}
