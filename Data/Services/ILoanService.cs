using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAll();

        Task<Loan> GetById(int id);

        
        Task AddAsync (Loan loan);

        Task<Loan> Update(int id, Loan loan);

        Task Delete(int id);
    }
}
