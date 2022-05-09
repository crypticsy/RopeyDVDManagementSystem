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
        public void Add(Producer producer) //Adding a new producer
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id); //Delete an existing producer according to ProducerNumber
            _context.Producers.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producer>> GetAll() //Get all producer data for the index
        {
            var result = await _context.Producers.ToListAsync();
            return result;
        }

        public async Task<Producer> GetById(int id) //Get individual producer data for details/deletion
        {
            var res = await _context.Producers.FirstOrDefaultAsync(n => n.ProducerNumber == id);
            return res;
        }

        public async Task<Producer> Update(int id, Producer newProducer) //Edit an existing producer 
        {
            _context.Update(newProducer);
            await _context.SaveChangesAsync(); 
            return newProducer;
        }
    }
}
