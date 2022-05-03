using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class DVDTitleService : IDVDTitleService
    {

        private readonly ApplicationDbContext _context;
        public DVDTitleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(DVDTitle dvdTitle)
        {
            _context.DVDTitles.Add(dvdTitle);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.DVDTitles.FirstOrDefaultAsync(n => n.DVDNumber == id);
            _context.DVDTitles.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDTitle>> GetAll()
        {
            var result =await _context.DVDTitles.ToListAsync();
            return result;
        }

        public async Task<DVDTitle> GetById(int id)
        {
            var res = await _context.DVDTitles.FirstOrDefaultAsync(n => n.DVDNumber == id);
            return res;
        }

        public async Task<DVDTitle> Update(int id, DVDTitle newDvdTitle)
        {
            _context.Update(newDvdTitle);
            await _context.SaveChangesAsync(); //DOES NOT UPDATE FOR SOME REASON
            return newDvdTitle;
        }
    }
}
