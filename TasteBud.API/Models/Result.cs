using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteBud.API.Models
{
    // Model to represent 
    public class Result
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int QuizId { get; set; }

        public DateTime Date { get; set; }

        public string RestaurantName { get; set; }

        public string Location { get; set; }

        public string PriceRange { get; set; }
    }
}
