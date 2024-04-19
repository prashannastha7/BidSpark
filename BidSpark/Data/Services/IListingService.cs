using BidSpark.Models;

namespace BidSpark.Data.Services
{
    public interface IListingService
    {
        IQueryable<Listing> GetAll();

        Task Add(Listing listing);
    }
}
