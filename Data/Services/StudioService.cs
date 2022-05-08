using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class StudioService: IStudioService
    {

        private readonly ApplicationDbContext _context;
        public StudioService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Studio studio)
        {
            _context.Studios.Add(studio);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
            _context.Studios.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Studio>> GetAll()
        {
            var result = await _context.Studios.ToListAsync();
            return result;
        }

        public async Task<Studio> GetById(int id)
        {
            var res = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
            return res;
        }

        public async Task<Studio> Update(int id, Studio newStudio) 
        {
            _context.Update(newStudio);
            await _context.SaveChangesAsync(); 
            return newStudio;
        }

    }
}
