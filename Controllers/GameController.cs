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
    // GET
    public async Task<IActionResult> Index()
    {
        var resultModel = await _mainGameService.UpdateGameStatus(new Game());
        return View("Index", model: resultModel);
    }
    
    public IActionResult StartGame()
    {
        //Set cookie 
        return RedirectToAction("Index", "Game");
    }

    public IActionResult EndGame()
    {
        return RedirectToAction("Index", "Home");
    }
}