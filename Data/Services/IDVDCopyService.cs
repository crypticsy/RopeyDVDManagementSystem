using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Data.Services
{
    public interface IDVDCopyService
    {
        //This is the interface for the DVD Copy Service
        Task<IEnumerable<DVDCopy>> GetAllAsync();
        Task<DVDCopy> GetByIdAsync(int id);
        Task AddAsync(DVDCopy dvdcopy);
        Task<DVDCopy> UpdateAsync(int id, DVDCopy newDVDcopy);
        Task DeleteAsync(int id);

    }
}
