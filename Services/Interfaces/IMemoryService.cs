using SolbegTask2.Models;
using SolbegTask2.Models.Dto;
using SolbegTask2.Models.Enums;

namespace SolbegTask2.Services.Interfaces;

public interface IMemoryService
{
    public int GetCurrentQuestionNumber(string gameId);
    public int UpdateGameQuestionNumber(string gameId);
    
    public bool UpdateCurrentQuestion(string gameId, QuestionDto question);
    public QuestionDto GetCurrentQuestion(string gameId);

    public int GetCorrectAnswer(string gameId);
    public void SetCorrectAnswer(string gameId, int correctAnswer = 3);

    public GameStatus GetGameStatus(string gameId);
    public bool UpdateGameStatus(string gameId, GameStatus gameStatus = GameStatus.AnsweringQuestion);
}