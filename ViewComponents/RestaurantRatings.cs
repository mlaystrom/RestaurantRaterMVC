using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Services.Ratings;

namespace RestaurantRaterMVC.ViewComponents;

[ViewComponent(Name = nameof(RestaurantRatings))]
public class RestaurantRatings : ViewComponent
{
    //adding IRatingService as a field and to the constructor inside the class
    //Not using curly braces for Constructor body, instead using an expression body to set a value to the field
    private readonly IRatingService _ratingService;
    public RestaurantRatings(IRatingService ratingService) => _ratingService = ratingService;

    //InvokeAsync Method (gets called when the ViewComponents gets used)
    public async Task<IViewComponentResult> InvokeAsync(int restaurantId)
    {
        var ratings = await _ratingService.GetRestaurantsRatingsAsync(restaurantId);
        return View(ratings);
    }
}