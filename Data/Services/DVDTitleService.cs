using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

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
            var res = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).FirstOrDefaultAsync(n => n.DVDNumber == id);
            _context.DVDTitles.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDTitle>> GetAll()
        {
            var result = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).ToListAsync();
            return result;
        }

        public async Task<DVDTitle> GetById(int id)
        {
            var res = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).FirstOrDefaultAsync(n => n.DVDNumber == id);
            return res;
        }

        public async Task<DVDTitleDropdownsVM> GetDVDTitleDropdownValues()
        {
            var response = new DVDTitleDropdownsVM()
            {
                Categories = await _context.DVDCategories.OrderBy(n => n.CategoryName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync(),
                Studios = await _context.Studios.OrderBy(n => n.StudioName).ToListAsync(),
                Actors = await _context.Actors.OrderBy(n => n.ActorFirstName).ToListAsync()
            };
            return response;
        }

        public async Task<DVDTitle> Update(int id, DVDTitle newDvdTitle)
        {
            _context.Update(newDvdTitle);
            await _context.SaveChangesAsync(); 
            return newDvdTitle;
        }
    }
}
