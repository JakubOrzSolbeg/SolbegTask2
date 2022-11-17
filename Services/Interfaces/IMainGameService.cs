using SolbegTask2.Models;

namespace SolbegTask2.Services.Interfaces;

public interface IMainGameService
{
    public Task<Game> UpdateGameStatus(Game game);
}