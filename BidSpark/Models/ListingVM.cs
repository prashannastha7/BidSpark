//ListingViewModel

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidSpark.Models
{
    public class ListingVM
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public required IFormFile Image { get; set; }
        public required bool IsSold { get; set; } = false;

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]

        //Navigation properties
        public IdentityUser? IdentityUser { get; set; }
    }
}
