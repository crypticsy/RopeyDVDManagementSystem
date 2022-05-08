using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class ActorService: IActorService
    {

        private readonly ApplicationDbContext _context;
        public ActorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var res = await _context.Actors.FirstOrDefaultAsync(n => n.ActorNumber == id);
            _context.Actors.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        public async Task<Actor> GetById(int id)
        {
            var res = await _context.Actors.FirstOrDefaultAsync(n => n.ActorNumber == id);
            return res;
        }

        public async Task<Actor> Update(int id, Actor newActor)
        {
            _context.Update(newActor);
            await _context.SaveChangesAsync(); 
            return newActor;
        }

    }
}
