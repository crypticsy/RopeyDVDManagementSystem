using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IProducerService
    {
        Task<IEnumerable<Producer>> GetAll();
        Task<Producer> GetById(int id);

        void Add(Producer producer);

        Task<Producer> Update(int id, Producer producer);

        Task Delete(int id);
    }
}
