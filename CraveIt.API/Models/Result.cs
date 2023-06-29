using System.ComponentModel.DataAnnotations;

namespace CraveIt.API.Models
{
    // Model to represent a saved result from a quiz that a specific user has taken
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
