using Microsoft.EntityFrameworkCore;
using RopeyDVDManagementSystem.Models;
using RopeyDVDManagementSystem.Models.ViewModels;

namespace RopeyDVDManagementSystem.Data.Services
{
    public class DVDTitleService : IDVDTitleService
    {

        private readonly ApplicationDbContext _context;
        public DVDTitleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(NewDVDTitleVM data) //To add a new DVD Title
        {
            var newDVD = new DVDTitle()
            {
                DVDTitleName = data.DVDTitleName,
                CategoryNumber = data.CategoryNumber,
                StudioNumber = data.StudioNumber,
                ProducerNumber = data.ProducerNumber,
                DVDPoster = data.DVDPoster,
                DateReleased = data.DateReleased,
                StandardCharge = data.StandardCharge,
                PenaltyCharge = data.PenaltyCharge
            };
            await _context.DVDTitles.AddAsync(newDVD);
            await _context.SaveChangesAsync();

            //ADD Movie Actors
            foreach (var actorId in data.CastMembers) //To add the Actors into the CastMembers table with the DVD ID of the newly created DVD title
            {
                var newCastMember = new CastMember()
                {
                    DVDNumber = newDVD.DVDNumber,
                    ActorNumber = actorId
                };
                await _context.CastMembers.AddAsync(newCastMember);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id) //Delete an existing dvd title
        {
            var res = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).FirstOrDefaultAsync(n => n.DVDNumber == id);
            _context.DVDTitles.Remove(res);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DVDTitle>> GetAll() //Get all data for DVD title index
        {
            var result = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).Include(n => n.CastMembers).ThenInclude(n => n.Actor).ToListAsync();
            return result;
        }

        public async Task<DVDTitle> GetById(int id) //Get individual data for deletion/details
        {
            var res = await _context.DVDTitles.Include(n => n.DVDCategory).Include(n => n.Producer).Include(n => n.Studio).Include(n => n.CastMembers).ThenInclude(n => n.Actor).FirstOrDefaultAsync(n => n.DVDNumber == id);
            return res;
        }

        public async Task<DVDTitleDropdownsVM> GetDVDTitleDropdownValues() //Gets the values from categories, producers, studios and actors for the dropdownlists
        {
            var response = new DVDTitleDropdownsVM()
            {
                Categories = await _context.DVDCategories.OrderBy(n => n.CategoryName).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.ProducerName).ToListAsync(),
                Studios = await _context.Studios.OrderBy(n => n.StudioName).ToListAsync(),
                Actors = await _context.Actors.OrderBy(n => n.ActorFirstName).ToListAsync()
            };
            return response;
        }

        public async Task Update(NewDVDTitleVM data) //Update an existing DVD title
        {
            var dbDvdTitle = await _context.DVDTitles.FirstOrDefaultAsync(n => n.DVDNumber == data.DVDNumber);
            if (dbDvdTitle != null) 
            {
                dbDvdTitle.DVDTitleName = data.DVDTitleName;
                dbDvdTitle.CategoryNumber = data.CategoryNumber;
                dbDvdTitle.StudioNumber = data.StudioNumber;
                dbDvdTitle.ProducerNumber = data.ProducerNumber;
                dbDvdTitle.DVDPoster = data.DVDPoster;
                dbDvdTitle.DateReleased = data.DateReleased;
                dbDvdTitle.StandardCharge = data.StandardCharge;
                dbDvdTitle.PenaltyCharge = data.PenaltyCharge;
                await _context.SaveChangesAsync();
            }
            //DELETE all existing cast members
            var existingActorsDb = _context.CastMembers.Where(n => n.DVDNumber == data.DVDNumber).ToList();
            _context.CastMembers.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //ADD new Actors
            foreach (var actorId in data.CastMembers)
            {
                var newCastMember = new CastMember()
                {
                    DVDNumber = data.DVDNumber,
                    ActorNumber = actorId
                };
                await _context.CastMembers.AddAsync(newCastMember);
            }
            await _context.SaveChangesAsync();
        }
    }
}
