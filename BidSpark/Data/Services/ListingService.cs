//ListingService is like a manager that handles tasks related to listings in a computer program.
//It's designed to help add new listings to a database and retrieve existing listings when needed


using BidSpark.Data.Services;
using BidSpark.Models;
using Microsoft.EntityFrameworkCore;

namespace BidSpark.Data.Services
{
    public class ListingService : IListingService
    {
        private readonly ApplicationDbContext _context;

        public ListingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Listing> GetAll()
        {
            var applicationDbContext = _context.Listings.Include(l => l.IdentityUser);
            return applicationDbContext;
        }

        public async Task<Listing> GetById(int? id)
        {
                  var listing = await _context.Listings
                 .Include(l => l.User)
                 .Include(l => l.Comments)
                 .Include(l => l.Bids)
                 .ThenInclude(l => l.User)
                 .FirstOrDefaultAsync(m => m.Id == id);
            return listing
        }
    }
}
