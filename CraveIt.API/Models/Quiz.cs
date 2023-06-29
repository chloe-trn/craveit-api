using System.ComponentModel.DataAnnotations;

namespace CraveIt.API.Models
{
    // Model to represent a quiz a user has taken
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Distance { get; set; }

        public string PriceRange { get; set; }

        public string Cuisine { get; set; }

        // Navigation properties

        // one Quiz can have many saved Results,
        // a Quiz doesn't have to a Result at first so a nullable value is allowed
        public ICollection<Result>? Results { get; set; }
    }
}
