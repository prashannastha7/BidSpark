using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BidSpark.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public double price { get; set; }

        [Required] // is the user is deleted list will be deleted
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]

        public IdentityUser? IdentityUser { get; set; }

        public int? ListingId { get; set; }
        [ForeignKey ("ListingId")]
        public Listing? Listing { get; set; }

    }
}
