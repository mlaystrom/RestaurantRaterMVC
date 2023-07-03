// Service Layer->modifies how objects are retrieved from or added to the database

using RestaurantRaterMVC.Data;

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
}