using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Controllers
{
    public class MembersManageController : Controller
    {
        private readonly IMembersService _service;

        public MembersManageController(IMembersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            
            // Get a list of all Members Numbers and Last Names
            var members = (List<String>) data.Select(x => x.MemberNumber.ToString()).ToList();
            var membersLastNames = (List<String>) data.Select(x => x.MemberLastName).ToList();
            members.AddRange(membersLastNames);
            ViewBag.MemberSearchList = (string)System.Text.Json.JsonSerializer.Serialize(members);

            return View(data);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string request)
        {
            var data = await _service.GetAllAsync();

            string MemberNumber = Request.Form["SearchMemberNumber"];
            ViewBag.SearchCopyNumber = MemberNumber;
            
            // Get a list of all Members Numbers and Last Names
            var members = (List<String>) data.Select(x => x.MemberNumber.ToString()).ToList();
            var membersLastNames = (List<String>) data.Select(x => x.MemberLastName).ToList();
            members.AddRange(membersLastNames);
            ViewBag.MemberSearchList = (string)System.Text.Json.JsonSerializer.Serialize(members);

            if (MemberNumber == "")
            {
                return View(data);
            }
            else if (uint.TryParse(MemberNumber,out uint result) && data.Where(x => x.MemberNumber == result).Count() > 0)
            {   
                data = data.Where(x => x.MemberNumber == result).ToList();
                return View(data);
            }
            else if (data.Where(x => x.MemberLastName == MemberNumber).Count() > 0)
            {
                data = data.Where(x => x.MemberLastName == MemberNumber).ToList();
                return View(data);
            }

            return View();
        }
       
        //Get: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("MemberFirstName, MemberLastName, MembershipCategoryNumber, MemberAddress, MemberDateofBirth")]Member member)
        {
            /*if (!ModelState.IsValid) return View(member);*/

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
            if (memberDetails == null) return View("Not Found"); //Handeling errors
            return View(memberDetails);
        }


        //Get: Members/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var memberDetails = await _service.GetByIdAsync(id);
            if (memberDetails == null) return View("Not Found"); //Handeling errors
            return View(memberDetails);
        }

        //Request to post edited data
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("MemberNumber, MemberFirstName, MemberLastName, MembershipCategoryNumber, MemberAddress, MemberDateofBirth")] Member member)
        {
            member.MemberNumber = Convert.ToUInt32(id);
            await _service.UpdateAsync(id, member);
            return RedirectToAction(nameof(Index));
        }
        //Get: Members/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var memberDetails = await _service.GetByIdAsync(id);
            if (memberDetails == null) return View("Not Found"); //Handeling errors
            return View(memberDetails);
        }

        //Request to delete data
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberDetails = await _service.GetByIdAsync(id);
            if (memberDetails == null) return View("Not Found"); //Handeling errors
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
