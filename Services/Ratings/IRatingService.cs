using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Ratings;

public interface IRatingService
{
    Task<bool> CreateRatingAsync(RatingCreate model);
    Task<List<RatingListItem>> GetRatingsAsync();
    Task<List<RatingListItem>> GetRestaurantsRatingsAsync(int restaurantId);
    Task<bool> DeleteRatingAsync(int restaurantid);
}