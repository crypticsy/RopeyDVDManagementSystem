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
        public void Add(DVDCategory category) //Add a new DVD category
        {
            _context.DVDCategories.Add(category);
            _context.SaveChanges();
        }

        public async Task Delete(int id) //Delete an existing DVD category
        {
            var res = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
            _context.DVDCategories.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDCategory>> GetAll() //Get all information for DVD category index
        {
            var result = await _context.DVDCategories.ToListAsync();
            return result;
        }

        public async Task<DVDCategory> GetById(int id) //Get individual data for details/deletion
        {
            var res = await _context.DVDCategories.FirstOrDefaultAsync(n => n.CategoryNumber == id);
            return res;
        }

        public async Task<DVDCategory> Update(int id, DVDCategory newCategory) //Update an existing DVD category
        {
            _context.Update(newCategory);
            await _context.SaveChangesAsync(); 
            return newCategory;
        }
    }
}
