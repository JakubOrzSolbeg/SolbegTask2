using Microsoft.AspNetCore.Mvc;
using SolbegTask2.Models;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Controllers;

public class GameController : Controller
{
    private readonly IMainGameService _mainGameService;
    public GameController(IMainGameService mainGameService)
    {
        _mainGameService = mainGameService;
    }

    private string GetGameCookie()
    {
        string? gameCookie;
        if (!HttpContext.Request.Cookies.ContainsKey("gameguid"))
        {
            gameCookie = Guid.NewGuid().ToString();
            HttpContext.Response.Cookies.Append("gameguid", gameCookie);
        }

        gameCookie = HttpContext.Request.Cookies["gameguid"];
        if (gameCookie == null)
        {
            gameCookie = Guid.NewGuid().ToString();
            HttpContext.Response.Cookies.Append("gameguid", gameCookie);
            return gameCookie;
        }
        else
        {
            return gameCookie;
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string gameCookie = GetGameCookie();
        var resultModel = await _mainGameService.GetCurrentGameStatus(gameCookie);
        return View("Index", model: resultModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(string answer)
    {
        string gameCookie = GetGameCookie();
        
        var resultModel = await _mainGameService.UpdateGameStatus(gameCookie, int.Parse(answer));
        return View("Index", model: resultModel);
    }

    [HttpGet]
    public IActionResult Results()
    {
        string gameCookie = GetGameCookie();
        var resultModel = _mainGameService.GetGameResult(gameCookie);
        HttpContext.Response.Cookies.Delete("gameguid");
        return View("Results", model: resultModel);
    }
    
    
    [HttpGet]
    public IActionResult Result()
    {
        return RedirectToAction("Index", "Home");
    }
}