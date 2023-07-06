using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Ratings;

public class RatingService : IRatingService
{
    private readonly RestaurantDbContext _context;
    public RatingService(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<bool>CreateRatingAsync(RatingCreate model)
    {
        Rating entity = new()
        {
            RestaurantId = model.RestaurantId,
            Score = model.Score
        };

        _context.Ratings.Add(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<List<RatingListItem>> GetRatingsAsync()
    {
        var ratings = await _context.Ratings
        .Include(r => r.Restaurant)
        .Select(r => new RatingListItem
        {
            Id = r.Id,
            RestaurantName = r.Restaurant.Name,
            Score = r.Score
        })
        .ToListAsync();

        return ratings;
    }
    
    public async Task<List<RatingListItem>> GetRestaurantsRatingsAsync(int restaurantId)
    {
        var ratings = await _context.Ratings
        .Include(r => r.Restaurant)
        .Where(r => r.RestaurantId == restaurantId)
        .Select(r => new RatingListItem
        {
            RestaurantName = r.Restaurant.Name,
            Score = r.Score
        })
        .ToListAsync();

        return ratings;
    }

    public async Task<bool> DeleteRatingAsync(int id)
    {
        Rating? entity = await _context.Ratings.FindAsync(id);
        if(entity is null) return false;
        _context.Ratings.Remove(entity);

    //telling DbSet to remove the found entity
    //Save changes to the database and return a boolean that states 1 change was made
        return await _context.SaveChangesAsync() == 1;
    }

}