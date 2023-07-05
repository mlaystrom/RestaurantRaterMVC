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

    //Edit Method
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        RestaurantDetail? restaurant = await _service.GetRestaurantAsync(id);
        if (restaurant is null)
        return NotFound();

        RestaurantEdit model = new()
        {
        Id = restaurant.Id,
        Name = restaurant.Name ?? "",
        Location = restaurant.Location ?? ""
        };

        return View(model);
    }

    //HttpPost method that takes in the Id of the Restaurant, as well as the RestaurantEdit model which will contain the new data

    [HttpPost]
    public async Task<IActionResult> Edit(int id, RestaurantEdit model)
    {
        if (!ModelState.IsValid)
        return View(model);
    //pass the model to the new service method, if the service return a true and the update passes, return the user to the Detail View
        if (await _service.UpdateRestaurantAsync(model))
            return RedirectToAction(nameof(Details), new { id = id });

    //if the Restaurant has failed to update, add a model error for the user and return the user back to the edit user
        ModelState.AddModelError("Save Error", "Could not update the Restaurant. Please try again.");
            return View(model);
    }

   
}