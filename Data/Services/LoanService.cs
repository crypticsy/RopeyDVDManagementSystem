using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;
        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            _context.Loans.AddAsync(loan);
            _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var res = await _context.Loans.FirstOrDefaultAsync(n => n.LoanNumber == id);
            _context.Loans.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            var result = await _context.Loans.ToListAsync();
            return result;
        }

        public async Task<Loan> GetById(int id)
        {
            var res = await _context.Loans.FirstOrDefaultAsync(n => n.LoanNumber == id);
            return res;
        }

        public async Task<Loan> Update(int id, Loan newLoan)
        {
            _context.Update(newLoan);
            await _context.SaveChangesAsync(); //DOES NOT UPDATE FOR SOME REASON
            return newLoan;
        }
    }
}
