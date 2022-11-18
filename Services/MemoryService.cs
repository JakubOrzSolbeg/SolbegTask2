using System.Buffers;
using System.Text.Json;
using Microsoft.AspNetCore.Connections.Features;
using SolbegTask2.Models;
using SolbegTask2.Models.Dto;
using SolbegTask2.Models.Enums;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class MemoryService : IMemoryService
{
    private readonly Dictionary<string, int> _gamesCurrentQuestionNumber;
    private readonly Dictionary<string, QuestionDto> _gamesCurrentQuestion;
    private readonly Dictionary<string, GameStatus> _gameStatuses;
    private readonly Dictionary<string, int> _correctAnswers;

    public MemoryService()
    {
        _gamesCurrentQuestionNumber = new Dictionary<string, int>();
        _gamesCurrentQuestion = new Dictionary<string, QuestionDto>();
        _gameStatuses = new Dictionary<string, GameStatus>();
        _correctAnswers = new Dictionary<string, int>();
    }
    
    public int GetCurrentQuestionNumber(string gameId)
    {
        if (_gamesCurrentQuestionNumber.ContainsKey(gameId))
        {
            return _gamesCurrentQuestionNumber[gameId];
        }
        else
        {
            _gamesCurrentQuestionNumber[gameId] = 0;
            return 0;
        }
    }
    
    public int UpdateGameQuestionNumber(string gameId)
    {
        if (_gamesCurrentQuestion.ContainsKey(gameId))
        {
            _gamesCurrentQuestionNumber[gameId] += 1;
        }
        else
        {
            _gamesCurrentQuestionNumber[gameId] = 0;
        }

        return _gamesCurrentQuestionNumber[gameId];
    }

    public bool UpdateCurrentQuestion(string gameId, QuestionDto question)
    {
        _gamesCurrentQuestion[gameId] = question;
        return true;
    }

    public QuestionDto GetCurrentQuestion(string gameId)
    {
        return _gamesCurrentQuestion[gameId];
    }

    public int GetCorrectAnswer(string gameId)
    {
        return _correctAnswers[gameId];
    }

    public void SetCorrectAnswer(string gameId, int correctAnswer = 3)
    {
        _correctAnswers[gameId] = correctAnswer;
    }

    public GameStatus GetGameStatus(string gameId)
    {
        if (!_gameStatuses.ContainsKey(gameId))
        {
            _gameStatuses[gameId] = GameStatus.NewGame;
        }

        return _gameStatuses[gameId];
    }

    public bool UpdateGameStatus(string gameId, GameStatus gameStatus = GameStatus.AnsweringQuestion)
    {
        _gameStatuses[gameId] = gameStatus;
        return true;
    }

    public void EndGame(string gameId)
    {
        _gameStatuses.Remove(gameId);
        _gamesCurrentQuestion.Remove(gameId);
        _gamesCurrentQuestionNumber.Remove(gameId);
        _correctAnswers.Remove(gameId);
    }
}