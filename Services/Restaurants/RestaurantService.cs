// Service Layer->modifies how objects are retrieved from or added to the database

using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurants;

public class RestaurantService : IRestaurantService
{
    //use this to set up the Restaurant service methods, which will deal with the database directly and return formatted C# objects for controller
    //a field to hold the RestaurantDbContext
    //RestaurantDbContext is then injected through the constructor
    private RestaurantDbContext _context;
    public RestaurantService(RestaurantDbContext context)
    {
        _context = context;
    }

    //Returning a collection of RestaurantListItems
    public async Task<List<RestaurantListItem>>GetAllRestaurantsAsync()
    {
        List<RestaurantListItem> restaurants = await _context.Restaurants
        .Include(r => r.Ratings) //Using Foreign Key by using the Include() method for Ratings Property
        .Select(r => new RestaurantListItem()
        {
            Id = r.Id,
            Name = r.Name,
            Score = r.AverageRating
        })
        .ToListAsync();//taking the new queried selection and converting into a C# List

        return restaurants;
    }
}