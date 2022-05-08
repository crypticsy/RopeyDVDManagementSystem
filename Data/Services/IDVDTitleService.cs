namespace RopeyDVDManagementSystem.Data.Services
{
    using RopeyDVDManagementSystem.Models;
    using RopeyDVDManagementSystem.Models.ViewModels;

    public interface IDVDTitleService
    {
        //This is the interface for the DVD Title Service
        Task<IEnumerable<DVDTitle>> GetAll();
        Task<DVDTitle> GetById(int id);

        Task Add(NewDVDTitleVM data);

        Task Update(NewDVDTitleVM dvdTitle);

        Task Delete(int id);

        Task<DVDTitleDropdownsVM> GetDVDTitleDropdownValues();
    }
}
