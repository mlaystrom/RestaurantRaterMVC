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

//Get method to return the Create view
//including the REstaurant id as a parameter
//using the id parameter to create a starting RatingCreate model
//Once it's created, pass the model to the view
    [HttpGet]
    public IActionResult Create(int id)
    {
        RatingCreate model = new() { RestaurantId = id};
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RatingCreate model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }
        //if valid, pass the model off to the service
        //This creates the Rating entity and saves it to the database
        await _service.CreateRatingAsync(model);
        //Will return to the index view using RedirectToAction()
        //redirect to the Restaurant/Details/:id action
        return RedirectToAction("Details", "Restaurant", new { id = model.RestaurantId});
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        RatingListItem model = new() {Id = id, Score = 3}; //what need to replace with a service method call (service.getratingbyid)
        
        return View(model);
    }

    [HttpPost]
    [ActionName(nameof(Delete))]//ActionName connects the method(ConfirmDelete) to the Delete action

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        //passing model off to the Service
        //This Deletes the Rating and saves to the database
        await _service.DeleteRatingAsync(id);
        return RedirectToAction(nameof(Index));
    }

    
}