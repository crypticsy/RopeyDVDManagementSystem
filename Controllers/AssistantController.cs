using Microsoft.AspNetCore.Mvc;

namespace RopeyDVDManagementSystem.Controllers
{
    public class AssistantController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Assigning view to display Assistant data
        }

        public IActionResult Create()
        {
            return View(); //Assigning view to add new Assistant
        }

        public IActionResult Edit()
        {
            return View(); //Assigning view to update Assistant data
        }


    }
}
