using Microsoft.AspNetCore.Mvc;

namespace SolbegTask2.Controllers;

public class GameController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}