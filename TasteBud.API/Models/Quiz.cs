using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteBud.API.Models
{
    // Model to represent a quiz a user has taken
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public string PriceRange { get; set; }

        public string Cuisine { get; set; }

        // Navigation property
        public IdentityUser User {get; set;}

        // one Quiz can have many random Results generated,
        // a Quiz doesn't have to a Result at first so a nullable value is allowed
        public ICollection<Result>? Results { get; set; }
    }
}
