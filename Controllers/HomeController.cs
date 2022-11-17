using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SolbegTask2.Models;

namespace SolbegTask2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (!HttpContext.Request.Cookies.ContainsKey("gameguid"))
        {
            var newcookie = Guid.NewGuid();
            HttpContext.Response.Cookies.Append("gameguid", newcookie.ToString());
        }
        return View();
    }
    
    public IActionResult EndGame()
    {
        HttpContext.Response.Cookies.Delete("gameguid");
        return View("Index");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}