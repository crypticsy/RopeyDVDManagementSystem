using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class ProducerService: IProducerService
    {
        private readonly ApplicationDbContext _context;
        public ProducerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id);
            _context.Producers.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producer>> GetAll()
        {
            var result = await _context.Producers.ToListAsync();
            return result;
        }

        public async Task<Producer> GetById(int id)
        {
            var res = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id);
            return res;
        }

        public async Task<Producer> Update(int id, Producer newProducer)
        {
            _context.Update(newProducer);
            await _context.SaveChangesAsync(); 
            return newProducer;
        }
    }
}
