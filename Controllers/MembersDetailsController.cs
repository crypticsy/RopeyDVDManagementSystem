﻿using Microsoft.AspNetCore.Mvc;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Data.Services;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    public class MembersDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IMembersService _service;
        public MembersDetailsController(ApplicationDbContext context, IMembersService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            // Get all member details
            var data = await _service.GetAllDetailsAsync();
            
            // Get a list of all Members Numbers and Last Names
            var members = (List<String>) _context.Members.Select(x => x.MemberNumber.ToString()).ToList();
            var membersLastNames = (List<String>) _context.Members.Select(x => x.MemberLastName).ToList();
            members.AddRange(membersLastNames);
            ViewBag.MemberSearchList = (string)System.Text.Json.JsonSerializer.Serialize(members);
            
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string request)
        {
            string MemberNumber = Request.Form["SearchMemberNumber"];
            ViewBag.SearchMemberNumber = MemberNumber;

            // Get a list of all Members Numbers and Last Names
            var members = (List<String>) _context.Members.Select(x => x.MemberNumber.ToString()).ToList();
            var membersLastNames = (List<String>) _context.Members.Select(x => x.MemberLastName).ToList();
            members.AddRange(membersLastNames);
            ViewBag.MemberSearchList = (string)System.Text.Json.JsonSerializer.Serialize(members);

            if (MemberNumber != null &&
                int.TryParse(MemberNumber, out int memNumber) &&
                _context.Members.Where(x => x.MemberNumber == memNumber).Count() > 0)
            {
                // Get all member details
                var data = await _service.GetAllDetailsAsync(memNumber, "");
                return View(data);
            }
            else if (MemberNumber == "")
            {
                // Get all member details
                var data = await _service.GetAllDetailsAsync();
                return View(data);
            }
            else
            {
                // Get all member details
                var data = await _service.GetAllDetailsAsync(-1, MemberNumber);
                return View(data);
            }
        }

        public IActionResult Details(int id)
        {


            if (_context.Members.Where(x => x.MemberNumber == id).Count() == 0)
            {
                return RedirectToAction("Index");
            }

            var currentMember = _context.Members.Where(x => x.MemberNumber == id).FirstOrDefault();

            ViewBag.MemberFirstName = currentMember.MemberFirstName;
            ViewBag.MemberLastName = currentMember.MemberLastName;
            ViewBag.MemberAddress = currentMember.MemberAddress;
            ViewBag.Birthday = currentMember.MemberDateOfBirth;
            ViewBag.MemebershipType = _context.MembershipCategories.Where(x => x.MembershipCategoryNumber == currentMember.MembershipCategoryNumber).FirstOrDefault().MembershipCategoryName;
            ViewBag.LastLoan = _context.Loans.Where(x => x.MemberNumber == currentMember.MemberNumber).OrderByDescending(x => x.DateOut).FirstOrDefault().DateOut;
            ViewBag.TotalLoans = _context.Loans.Where(x => x.MemberNumber == currentMember.MemberNumber).Count();
            
            IEnumerable<DVDReturnModel> loanRecord = (  from dt in _context.DVDTitles
                                                        join dtc in _context.DVDCategories on dt.CategoryNumber equals dtc.CategoryNumber
                                                        join dc in _context.DVDCopies on dt.DVDNumber equals dc.DVDNumber
                                                        join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                                                        join m in _context.Members on l.MemberNumber equals m.MemberNumber
                                                        orderby l.DateOut descending
                                                        where m.MemberNumber == currentMember.MemberNumber && l.DateOut >= DateTime.Today - TimeSpan.FromDays(31)
                                                        select new DVDReturnModel { CopyNumber = dc.CopyNumber,
                                                                                    DVDTitleName = dt.DVDTitleName,
                                                                                    DVDCategory = dtc.CategoryName,
                                                                                    DateOut = l.DateOut,
                                                                                    DateDue = l.DateDue,
                                                                                    DateReturned = l.DateReturned,
                                                                                    MemberName = m.MemberFirstName + ' ' + m.MemberLastName,
                                                                                    LoanNumber = l.LoanNumber, 
                                                                                    Payment =  l.ReturnAmount
                                                        });

            return View(loanRecord);
        }


        public IActionResult ActiveMember()
        {
            IEnumerable<FilteredLoan> filteredLoan =    from l in _context.Loans
                                                        where l.DateReturned == null
                                                        where l.DateOut >= DateTime.Today - TimeSpan.FromDays(31)
                                                        select new FilteredLoan{    DateOut = l.DateOut, 
                                                                                    MemberNumber = l.MemberNumber, 
                                                                                    CopyNumber= l.CopyNumber};

            IEnumerable<MemberDetailViewModel>  data =  from m in _context.Members
                                                join mc in _context.MembershipCategories on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber
                                                select new MemberDetailViewModel
                                                {
                                                    MemberNumber = m.MemberNumber,
                                                    MemberFirstName = m.MemberFirstName,
                                                    MemberLastName = m.MemberLastName,
                                                    MemberAddress = m.MemberAddress,
                                                    LastLoanDate = filteredLoan.Where(x => x.MemberNumber == m.MemberNumber).OrderByDescending(x => x.DateOut).FirstOrDefault().DateOut,
                                                    LastLoanDVDTitle = (from mem in _context.Members
                                                                        join l in filteredLoan on mem.MemberNumber equals l.MemberNumber
                                                                        join c in _context.DVDCopies on l.CopyNumber equals c.CopyNumber
                                                                        join dt in _context.DVDTitles on c.DVDNumber equals dt.DVDNumber
                                                                        where mem.MemberNumber == m.MemberNumber
                                                                        orderby l.DateOut descending
                                                                        select dt.DVDTitleName).FirstOrDefault(),
                                                    LastActivity = (DateTime.Today - filteredLoan.Where(x => x.MemberNumber == m.MemberNumber).OrderByDescending(x => x.DateOut).FirstOrDefault().DateOut).Days,
                                                };
            
            data = data.OrderBy(x => x.LastLoanDate);
            return View(data);
        }
    }
}
