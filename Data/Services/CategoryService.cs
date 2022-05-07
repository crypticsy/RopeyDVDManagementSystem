using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(DVDCategory category)
        {
            _context.DVDCategories.Add(category);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
            _context.DVDCategories.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDCategory>> GetAll()
        {
            var result = await _context.DVDCategories.ToListAsync();
            return result;
        }

        public async Task<DVDCategory> GetById(int id)
        {
            var res = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
            return res;
        }

        public async Task<DVDCategory> Update(int id, DVDCategory newCategory)
        {
            _context.Update(newCategory);
            await _context.SaveChangesAsync(); 
            return newCategory;
        }
    }
}
