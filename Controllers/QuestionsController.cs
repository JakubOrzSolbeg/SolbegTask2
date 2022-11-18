using Microsoft.AspNetCore.Mvc;
using SolbegTask2.Models;
using SolbegTask2.Models.Dto;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Controllers;

public class QuestionsController : Controller
{
    private readonly IQuestionService _questionService;
    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View("Question2", model: new NewQuestion());
    }

    [HttpPost]
    public async Task<IActionResult> Index(NewQuestion model)
    {
        var result = await _questionService.AddNewQuestion(model);
        ModelState.Clear();
        return View("Question2", model: new NewQuestion(){QuestionText = "", ReturnMessage = result});
    }
    
    
}