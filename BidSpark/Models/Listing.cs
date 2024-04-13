using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidSpark.Models
{
    public class Listing
    {
    
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double  Price { get; set; }
        public required string ImagePath { get; set; }
        public required bool IsSold { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]

        //Navigation properties
        public IdentityUser? IdentityUser { get; set; }

        public List<Bid>? Bids { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
