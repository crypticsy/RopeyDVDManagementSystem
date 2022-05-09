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
        public void Add(Studio studio) //Add a new studio
        {
            _context.Studios.Add(studio);
            _context.SaveChanges();
        }

        public async Task Delete(int id) //Delete an existing studi according to StudioNumber
        {
            var res = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
            _context.Studios.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Studio>> GetAll() //Get all studios for displaying in the index
        {
            var result = await _context.Studios.ToListAsync();
            return result;
        }

        public async Task<Studio> GetById(int id) //Get particular studio data according to the StudioNumber 
        {
            var res = await _context.Studios.FirstOrDefaultAsync(n => n.StudioNumber == id);
            return res;
        }

        public async Task<Studio> Update(int id, Studio newStudio)  //Edit an existing studio according to the StudioNumber
        {
            _context.Update(newStudio);
            await _context.SaveChangesAsync(); 
            return newStudio;
        }

    }
}
