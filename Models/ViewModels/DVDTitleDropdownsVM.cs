using RopeyDVDManagementSystem.Models;

namespace RopeyDVDManagementSystem.Models.ViewModels
{
    public class DVDTitleDropdownsVM
    {
        public DVDTitleDropdownsVM()
        {
            Categories = new List<DVDCategory>();
            Producers = new List<Producer>();
            Studios = new List<Studio>();
            Actors = new List<Actor>();
        }
        public List<DVDCategory> Categories { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Studio> Studios { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
