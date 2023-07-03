using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Data;

public class Restaurant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    //this is a virtual property so doesn't represent a column in the database
    // A space for EntityFramework to collect and deposit the Ratings for this Restaurant
    // holds the list of Ratings
    public List<Rating> Ratings { get; set; } = new();

    //Property for the average value the Rating scores for the Restaurant
    public double? AverageRating
    {
        get
        {
            if (Ratings.Count == 0)
                return 0;

            return Ratings.Select(r => r.Score).Sum() / Ratings.Count;
        }
    }
}