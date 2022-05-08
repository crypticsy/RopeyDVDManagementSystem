using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class MembersService : IMembersService
    {
        private ApplicationDbContext _context;
        public MembersService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Members.FirstOrDefaultAsync(n => n.MemberNumber == id);
            _context.Members.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
           var result = await _context.Members.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync()
        {
            var result = await  (from m in _context.Members
                                join mc in _context.MembershipCategories on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber
                                orderby m.MemberFirstName, m.MemberLastName
                                select new MemberDetailViewModel
                                {
                                    MemberNumber = m.MemberNumber,
                                    MembershipCategory = mc.MembershipCategoryName,
                                    MemberFirstName = m.MemberFirstName,
                                    MemberLastName = m.MemberLastName,
                                    LoanCount = (from l in _context.Loans
                                                where l.MemberNumber == m.MemberNumber && l.DateReturned == DateTime.MinValue
                                                select l.LoanNumber).Count(),
                                    Remarks = (from l in _context.Loans
                                                where l.MemberNumber == m.MemberNumber && l.DateReturned == DateTime.MinValue
                                                select l.LoanNumber).Count() > (int)mc.MembershipCategoryTotalLoans ? "Too many DVDs" : null
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync(int id, string lastName)
        {
            var result = await  (from m in _context.Members
                                join mc in _context.MembershipCategories on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber
                                where m.MemberNumber == id || m.MemberLastName.Contains(lastName)
                                orderby m.MemberFirstName, m.MemberLastName
                                select new MemberDetailViewModel
                                {
                                    MemberNumber = m.MemberNumber,
                                    MembershipCategory = mc.MembershipCategoryName,
                                    MemberFirstName = m.MemberFirstName,
                                    MemberLastName = m.MemberLastName,
                                    LoanCount = (from l in _context.Loans
                                                where l.MemberNumber == m.MemberNumber && l.DateReturned == DateTime.MinValue
                                                select l.LoanNumber).Count(),
                                    Remarks = (from l in _context.Loans
                                                where l.MemberNumber == m.MemberNumber && l.DateReturned == DateTime.MinValue
                                                select l.LoanNumber).Count() > (int)mc.MembershipCategoryTotalLoans ? "Too many DVDs" : null
                                }).ToListAsync();

            return result;
        }

        public async Task<Member> UpdateAsync(int id, Member newMember)
        {
            _context.Update(newMember);
            await _context.SaveChangesAsync();
            return newMember;

        }

        public async Task<Member> GetByIdAsync(int id)
        {
            var result = await _context.Members.FirstOrDefaultAsync(n => n.MemberNumber == id);
            return result;
        }

    }
}
