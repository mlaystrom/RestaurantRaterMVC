using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantRaterMVC.Models.Restaurant;

public class RestaurantDetail
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }

    [DisplayName("Average Score")]
    [DisplayFormat(DataFormatString = "{0:N2}")]//rounding the average to 2 decimal points
    public double? Score { get; set; }
}