using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class DVDCopyService : IDVDCopyService
    {
        private ApplicationDbContext _context;
        public DVDCopyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(DVDCopy dvdcopy)
        {
            await _context.DVDCopies.AddAsync(dvdcopy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.DVDCopies.FirstOrDefaultAsync(n => n.CopyNumber == id);
            _context.DVDCopies.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDCopy>> GetAllAsync()
        {
            var result = await _context.DVDCopies.ToListAsync();
            return result;
        }


        public async Task<DVDCopy> UpdateAsync(int id, DVDCopy newDVDCopy)
        {
            _context.Update(newDVDCopy);
            await _context.SaveChangesAsync();
            return newDVDCopy;

        }

        public async Task<DVDCopy> GetByIdAsync(int id)
        {
            var result = await _context.DVDCopies.FirstOrDefaultAsync(n => n.CopyNumber == id);
            return result;
        }

    }
}
