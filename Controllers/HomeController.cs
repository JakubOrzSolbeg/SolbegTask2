using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SolbegTask2.Models;

namespace SolbegTask2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Request.Cookies.ContainsKey("gameguid"))
        {
            //Started game == redirect to game controller
            return RedirectToAction("Index", "Game");
        }
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}