using SolbegTask2.Models;

namespace SolbegTask2.Services.Interfaces;

public interface IMemoryService
{
    public int GetCurrentQuestionNumber(string gameId);
    public int UpdateGameQuestionNumber(string gameId);
}