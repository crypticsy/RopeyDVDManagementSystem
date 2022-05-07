namespace RopeyDVDManagementSystem.Data.Services
{
    using RopeyDVDManagementSystem.Models;
    using RopeyDVDManagementSystem.Models.ViewModels;

    public interface IDVDTitleService
    {
        Task<IEnumerable<DVDTitle>> GetAll();
        Task<DVDTitle> GetById(int id);

        void Add(DVDTitle dvdTitle);

        Task<DVDTitle> Update(int id, DVDTitle dvdTitle);

        Task Delete(int id);

        Task<DVDTitleDropdownsVM> GetDVDTitleDropdownValues();
    }
}
