using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Data;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Controllers
{
    public class DVDTransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDTransactionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> DVDTransaction()
        {

            var LoanRecord = from dvdtitles in _context.DVDTitles
                             join dc in _context.DVDCopies on dvdtitles.DVDNumber equals dc.DVDNumber
                             join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                             join m in _context.Members on l.MemberNumber equals m.MemberNumber
                             select new DVDTranscation { CopyNumber = dc.CopyNumber, DVDTitleName = dvdtitles.DVDTitleName, DateOut = l.DateOut, DateDue = l.DateDue, DateReturned = l.DateReturned, MemberName = m.MemberFirstName+' '+m.MemberLastName };


            return View(LoanRecord);
        }

        public async Task<IActionResult> DVDLoan()
        {
            //total loan for each copy that is loaned out and sort by dateout
            var LoanRecord = from dvdtitles in _context.DVDTitles
                             join dc in _context.DVDCopies on dvdtitles.DVDNumber equals dc.DVDNumber
                             join l in _context.Loans on dc.CopyNumber equals l.CopyNumber
                             join m in _context.Members on l.MemberNumber equals m.MemberNumber
                             select new DVDTranscation { CopyNumber = dc.CopyNumber, DVDTitleName = dvdtitles.DVDTitleName, DateOut = l.DateOut, DateDue = l.DateDue, DateReturned = l.DateReturned, MemberName = m.MemberFirstName + ' ' + m.MemberLastName };


            return View(LoanRecord);
        }
    }
}
