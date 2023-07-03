using Microsoft.AspNetCore.Mvc;
using RestaurantRaterMVC.Services.Restaurants;

namespace RestaurantRaterMVC.Controllers;

public class RestaurantController : Controller
{
    private IRestaurantService _service;
    public RestaurantController(IRestaurantService service)
    {
        _service = service;
    }
}