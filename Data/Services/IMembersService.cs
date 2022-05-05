using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IMembersService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task AddAsync(Member member);
        Task<Member> UpdateAsync(int id, Member newMember);
        Task DeleteAsync(int id);
        
    }
}
