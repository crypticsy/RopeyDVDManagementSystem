using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<DVDCategory>> GetAll();
        Task<DVDCategory> GetById(int id);

        void Add(DVDCategory category);

        Task<DVDCategory> Update(int id, DVDCategory category);

        Task Delete(int id);
    }
}
