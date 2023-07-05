using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Rating;
using RestaurantRaterMVC.Services.Ratings;

namespace RestaurantRaterMVC.Controllers;

public class RatingController : Controller
{
    //Private IRatingService via dependency injection by being added as a parameter to the constructor
    //Has a private field (_service) that a value can be assigned to
    private readonly IRatingService _service;
    public RatingController(IRatingService service)
    {
        _service = service;
    }

//Index method for Ratings
    public async Task<IActionResult> Index()
    {
        List<RatingListItem> model = await _service.GetRatingsAsync();
        return View(model);
    }
}