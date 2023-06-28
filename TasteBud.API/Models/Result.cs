using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteBud.API.Models
{
    // Model to represent a result from a quiz that a specific user has taken
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

        public string Distance { get; set; }

        public float Rating { get; set; }
    }
}
