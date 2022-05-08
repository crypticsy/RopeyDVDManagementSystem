using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IStudioService
    {
        Task<IEnumerable<Studio>> GetAll();
        Task<Studio> GetById(int id);

        void Add(Studio studio);

        Task<Studio> Update(int id, Studio studio);

        Task Delete(int id);
    }
}
