using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IActorService
    {
        //This is the interface for the Actor Service
        Task<IEnumerable<Actor>> GetAll();
        Task<Actor> GetById(int id);

        void Add(Actor actor);

        Task<Actor> Update(int id, Actor actor);

        Task Delete(int id);

    }
}
