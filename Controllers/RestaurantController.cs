using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurants;

namespace RestaurantRaterMVC.Controllers;

public class RestaurantController : Controller
{
    private IRestaurantService _service;
    public RestaurantController(IRestaurantService service)
    {
        _service = service;

    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<RestaurantListItem> restaurants = await _service.GetAllRestaurantsAsync();
        return View(restaurants);

    }

    //Returning Detail view for a Restaurant
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        RestaurantDetail? model = await _service.GetRestaurantAsync(id);

        if(model is null)
        return NotFound();

        return View(model);
    }

    //A Get endpoint to get the Create View
    //Create View contains the HTML form that will be used to create Restaurants
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantCreate model)
    {
        if(!ModelState.IsValid)
        return View(model);

        await _service.CreateRestaurantAsync(model);

        //redirects back to the index view so we can confirm that the new Restaurant had been created
        return RedirectToAction(nameof(Index));
    }

   
}