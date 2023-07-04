using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurants;

//where the basic CRUD methods we'll need will be defined
public interface IRestaurantService
{
    Task<List<RestaurantListItem>> GetAllRestaurantsAsync();

    //CreateRestaurant 
    Task<bool> CreateRestaurantAsync(RestaurantCreate Model);

    
}