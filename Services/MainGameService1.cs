using SolbegTask2.Models;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class MainGameService1 : IMainGameService
{
    private IMemoryService _memoryService;
    private IStaticConfigService _staticConfigService;
    private IQuestionService _questionService;

    public MainGameService1(IMemoryService memoryService, IStaticConfigService configService, IQuestionService questionService)
    {
        _memoryService = memoryService;
        _staticConfigService = configService;
        _questionService = questionService;
    }
    public async Task<Game> UpdateGameStatus(Game game)
    {
        var userCurrentQuestion = 3;
        var count = 0;
        
        var userQuestionScore = _staticConfigService
            .GetQuestionRewardList()
            .Select(q => new QuestionInfo()
            {
                QuestionStatus = QuestionStatus.Unanswered,
                IsMilestone = q.Milestone,
                Reward = q.Price
            })
            .ToList();
        
        // //TODO remove this temporary mockup
        //
        userQuestionScore[userCurrentQuestion].QuestionStatus = QuestionStatus.DuringAnswer;
        for (int i = Math.Max(0, userCurrentQuestion - 1); i >= 0; i--)
        {
            userQuestionScore[i].QuestionStatus = QuestionStatus.Answered;
            Console.WriteLine($"{userQuestionScore[i].Reward} {userQuestionScore[i].QuestionStatus}");
        }
        Game updatedGameStatus = new Game()
        {
            Question = await _questionService.GetRandomQuestion(),
            QuestionInfo = userQuestionScore,
            AwaibleAction = new ActionButton()
            {
                ButtonType = ButtonType.Surrender,
                CustomText = ":("
            }
        };
        return updatedGameStatus;
    }
}