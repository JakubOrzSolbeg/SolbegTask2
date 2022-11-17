using System.Buffers;
using System.Text.Json;
using Microsoft.AspNetCore.Connections.Features;
using SolbegTask2.Models;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class MemoryService : IMemoryService
{
    private readonly Dictionary<string, int> _gamesCurrentQuestion;

    public MemoryService()
    {
        _gamesCurrentQuestion = new Dictionary<string, int>();
    }
    
    public int GetCurrentQuestionNumber(string gameId)
    {
        if (_gamesCurrentQuestion.ContainsKey(gameId))
        {
            return _gamesCurrentQuestion[gameId];
        }
        else
        {
            _gamesCurrentQuestion[gameId] = 0;
            return 0;
        }
    }

    public int UpdateGameQuestionNumber(string gameId)
    {
        if (_gamesCurrentQuestion.ContainsKey(gameId))
        {
            _gamesCurrentQuestion[gameId] += 1;
        }
        else
        {
            _gamesCurrentQuestion[gameId] = 0;
        }

        return _gamesCurrentQuestion[gameId];
    }
}