using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurants;

//where the basic CRUD methods we'll need will be defined
public interface IRestaurantService
{
    Task<List<RestaurantListItem>> GetAllRestaurantsAsync();

    //CreateRestaurant 
    Task<bool> CreateRestaurantAsync(RestaurantCreate Model);

    Task<RestaurantDetail?> GetRestaurantAsync(int id);

    Task<bool>UpdateRestaurantAsync(RestaurantEdit model);

    Task<bool>DeleteRestaurantAsync(int id);
}