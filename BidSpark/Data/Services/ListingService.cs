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
        public IQueryable<Listing> GetAll()
        {
            var applicationDbContext = _context.Listings.Include(l => l.IdentityUser);
            return applicationDbContext;
        }
    }
}
