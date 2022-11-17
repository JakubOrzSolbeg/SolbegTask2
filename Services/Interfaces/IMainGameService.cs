using SolbegTask2.Models;

namespace SolbegTask2.Services.Interfaces;

public interface IMainGameService
{
    public Task<Game> GetCurrentGameStatus(string gameId);
    public Task<Game> UpdateGameStatus(string gameId, int answer = -1);
    public Result GetGameResult(string gameId);
}