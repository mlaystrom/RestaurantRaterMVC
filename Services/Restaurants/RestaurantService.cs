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

    public async Task<bool> CreateRestaurantAsync(RestaurantCreate Model)
    {
        //Restaurant made from the Model passed into the method
        Restaurant entity = new()
        {
            Name = Model.Name,
            Location = Model.Location
        };
        //add the restaurant to the DbSet
        _context.Restaurants.Add(entity);

        //snyc the database with the DbContext, which adds the new Restaurant to the SQL database
        return await _context.SaveChangesAsync() == 1;
    }
    //Returning a collection of RestaurantListItems
    public async Task<List<RestaurantListItem>> GetAllRestaurantsAsync()
    {
        List<RestaurantListItem> restaurants = await _context.Restaurants
        .Include(r => r.Ratings) //Using Foreign Key by using the Include() method for Ratings Property
        .Select(r => new RestaurantListItem()
        {
            Id = r.Id,
            Name = r.Name,
            Score = r.AverageRating ?? 0
        })
        .ToListAsync();//taking the new queried selection and converting into a C# List

        return restaurants;
    }

    //Restaurant Detail Method

    public async Task<RestaurantDetail?> GetRestaurantAsync(int id)
    {
        //1st: retrieve the Restaurant with the given Id from database
        //Get the DbSet of Restaurants from the database context, then include Ratings, then use FirstOrDefaultAsync() to find the right Restaurant
        Restaurant? restaurant = await _context.Restaurants
        .Include(r => r.Ratings)
        .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant is null ? null : new()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location,
            Score = restaurant.AverageRating
        };
    }

    public async Task<bool> UpdateRestaurantAsync(RestaurantEdit model)
    {
        Restaurant? entity = await _context.Restaurants.FindAsync(model.Id);

        if (entity is null)
            return false;
        //Updating the entity's properties
        entity.Name = model.Name;
        entity.Location = model.Location;
        //number of rows changed ->1
        return await _context.SaveChangesAsync() == 1;
    }


}