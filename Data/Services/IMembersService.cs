using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IMembersService
    {
        //Interface for the Members Service
        Task<IEnumerable<Member>> GetAllAsync();
        Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync();
        Task<IEnumerable<MemberDetailViewModel>> GetAllDetailsAsync(int id, string lastName);
        Task<Member> GetByIdAsync(int id);
        Task AddAsync(Member member);
        Task<Member> UpdateAsync(int id, Member newMember);
        Task DeleteAsync(int id);
        
    }
}
