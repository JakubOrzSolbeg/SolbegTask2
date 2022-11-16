using Microsoft.AspNetCore.Mvc;
using SolbegTask2.Models;

namespace SolbegTask2.Controllers;

public class GameController : Controller
{
    // GET
    public IActionResult Index()
    {
        var modelResult = new Game()
        {
            Question = "Do you want to be a big shot?",
            AnsewrA = "Not realy",
            AnsewrB = "Kromer",
            AnsewrC = "Hyperlink Blocked iansdinsdi asdiandlasd asdlinasioda sl diasndas dlasd",
            AnsewrD = "That is [redacted]",
            AwaibleAction = new ActionButton()
            {
                ButtonType = ButtonType.Surrender,
            },
        };
        return View(model: modelResult);
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