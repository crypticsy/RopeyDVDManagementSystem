using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
           var result = await _context.Members.ToListAsync();
            return result;
        }

        public Member Update(int id, Member newMember)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            var result = await _context.Members.FirstOrDefaultAsync(n => n.MemberNumber == id);
            return result;
        }

       
    }
}
