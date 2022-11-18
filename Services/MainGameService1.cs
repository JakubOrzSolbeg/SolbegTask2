using SolbegTask2.Models;
using SolbegTask2.Models.Dto;
using SolbegTask2.Models.Enums;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class MainGameService1 : IMainGameService
{
    private readonly IMemoryService _memoryService;
    private readonly IStaticConfigService _staticConfigService;
    private readonly IQuestionService _questionService;

    public MainGameService1(IMemoryService memoryService, IStaticConfigService configService, IQuestionService questionService)
    {
        _memoryService = memoryService;
        _staticConfigService = configService;
        _questionService = questionService;
    }
    private QuestionDto ValidateAnswer(string gameId, int answer)
    {
        var currentQuestion = _memoryService.GetCurrentQuestion(gameId);
        var correctPosition = _memoryService.GetCorrectAnswer(gameId);
        
        var wrongSelected = new [] { false, false, false, false };
        var correctAnswers = new [] { false, false, false, false };

        correctAnswers[correctPosition] = true;
        if (answer != correctPosition)
        {
            wrongSelected[answer] = true;
            _memoryService.UpdateGameStatus(gameId, GameStatus.AnsweredWrong);
        }
        else
        {
            _memoryService.UpdateGameStatus(gameId, GameStatus.AnsweredRight);
        }
        
        var resultQuestionDto = new QuestionDto()
        {
            QuestionText = currentQuestion.QuestionText,
            AnswerA = new AnswerDto()
            {
                Clickable = false,
                IsCorrect = correctAnswers[0],
                IsWrong = wrongSelected[0],
                Text = currentQuestion.AnswerA.Text
            },
            AnswerB = new AnswerDto()
            {
                Clickable = false,
                IsCorrect = correctAnswers[1],
                IsWrong = wrongSelected[1],
                Text = currentQuestion.AnswerB.Text
            },
            AnswerC = new AnswerDto()
            {
                Clickable = false,
                IsCorrect = correctAnswers[2],
                IsWrong = wrongSelected[2],
                Text = currentQuestion.AnswerC.Text
            },
            AnswerD = new AnswerDto()
            {
                Clickable = false,
                IsCorrect = correctAnswers[3],
                IsWrong = wrongSelected[3],
                Text = currentQuestion.AnswerD.Text
            },
        };
        return resultQuestionDto;
    }

    private List<QuestionInfo> PrepareScoreboard(string gameId)
    {
        var userCurrentQuestion = _memoryService.GetCurrentQuestionNumber(gameId);
        var userQuestionScore = _staticConfigService
            .GetQuestionRewardList()
            .Select(q => new QuestionInfo()
            {
                QuestionStatus = QuestionStatus.Unanswered,
                IsMilestone = q.Milestone,
                Reward = q.Price
            })
            .ToList();
        for (int i = Math.Max(0, Math.Min(userQuestionScore.Count - 1, userCurrentQuestion - 1)); i >= 0; i--)
        {
            userQuestionScore[i].QuestionStatus = QuestionStatus.Answered;
        }
        userQuestionScore[userCurrentQuestion].QuestionStatus = QuestionStatus.DuringAnswer;
        
        return userQuestionScore;
    }

    private ActionButton PrepareActionButton(string gameId)
    {
        ActionButton actionButton = new ActionButton();
        switch (_memoryService.GetGameStatus(gameId))
        {
            case GameStatus.AnsweredRight:
                actionButton.ButtonType = ButtonType.GoNextQuestion;
                break;
            case GameStatus.AnsweredWrong:
                actionButton.ButtonType = ButtonType.GoToFailureScreen;
                break;
            case GameStatus.WonGame:
                actionButton.ButtonType = ButtonType.GoToWinScreen;
                break;
            default:
                actionButton.ButtonType = ButtonType.Surrender;
                break;
        }

        return actionButton;
    }

    private async Task<QuestionDto> PrepareNewQuestion(string gameId)
    {
        var randomQuestion = await _questionService.GetRandomQuestion();
        
        var correctAnswer = randomQuestion.CorrectAnswer;
        
        //Shuffle answer order
        var answerList = new List<string>(){ randomQuestion.CorrectAnswer, randomQuestion.WrongAnswerB, randomQuestion.WrongAnswerC, randomQuestion.WrongAnswerD };
        var rng = new Random();
        var shuffledAnswers = answerList.OrderBy(a => rng.Next()).ToList();

        var correctAnswerPosition = shuffledAnswers.FindIndex(x => x.Equals(correctAnswer));
        _memoryService.SetCorrectAnswer(gameId, correctAnswerPosition);
        
        
        var questionDto = new QuestionDto()
        {
            QuestionText = randomQuestion.QuestionText,
            AnswerA = new AnswerDto()
            {
                Text = shuffledAnswers[0],
                Clickable = true,
            },
            AnswerB = new AnswerDto()
            {
                Text = shuffledAnswers[1],
            },
            AnswerC = new AnswerDto()
            {
                Text = shuffledAnswers[2],
            },
            AnswerD = new AnswerDto()
            {
                Text = shuffledAnswers[3],
            }
        };
        _memoryService.UpdateCurrentQuestion(gameId, questionDto);
        return questionDto;
    }

    public async Task<Game> GetCurrentGameStatus(string gameId)
    {
        QuestionDto questionDto;
        var gameStatus = _memoryService.GetGameStatus(gameId);
        
        if(gameStatus is GameStatus.NewGame or GameStatus.AnsweredRight)
        {
            questionDto = await PrepareNewQuestion(gameId);
            _memoryService.UpdateGameStatus(gameId, GameStatus.AnsweringQuestion);
        }
        else
        {
            questionDto = _memoryService.GetCurrentQuestion(gameId);
        }
        var scoreBoard = PrepareScoreboard(gameId);
        var actionButton = PrepareActionButton(gameId);
        return new Game()
        {
            Question = questionDto,
            QuestionInfo = scoreBoard,
            AwaibleAction = actionButton
        };
    }

    public async Task<Game> UpdateGameStatus(string gameId, int answer = 0)
    {
        var userQuestionScore = PrepareScoreboard(gameId);
        
        var resultQuestionDto = ValidateAnswer(gameId, answer);
        if (_memoryService.GetGameStatus(gameId) != GameStatus.AnsweredWrong)
        {
            int userCurrentQuestion = _memoryService.UpdateGameQuestionNumber(gameId);
            if (userCurrentQuestion >= userQuestionScore.Count)
            {
                _memoryService.UpdateGameStatus(gameId, GameStatus.WonGame);
            }
            else
            {
                _memoryService.UpdateGameStatus(gameId, GameStatus.AnsweredRight);
            }
        }
        
        Game updatedGameStatus = new Game()
        {
            Question = resultQuestionDto,
            QuestionInfo = userQuestionScore,
            AwaibleAction = PrepareActionButton(gameId)
        };
        return updatedGameStatus;
    }
    
    public Result GetGameResult(string gameId)
    {
        var resultModel = new Result();
        var scoreBoard = _staticConfigService.GetQuestionRewardList();
        var questionsAnswered = _memoryService.GetCurrentQuestionNumber(gameId);
        var gameStatus = _memoryService.GetGameStatus(gameId);
        
        if (questionsAnswered <= 0)
        {
            resultModel.PriceWon = 0;
        }
        else
        {
            //Won the game
            if (gameStatus == GameStatus.WonGame)
            {
                resultModel.PriceWon = scoreBoard[^1].Price;
            }
            //Surrendered
            else if (gameStatus == GameStatus.AnsweringQuestion || gameStatus == GameStatus.GaveUp)
            {
                resultModel.PriceWon = scoreBoard[questionsAnswered-1].Price;
            }
            //Answered wrong will get only money from last checkpoint
            else
            {
                int i = questionsAnswered;
                while (i >= 0 && !scoreBoard[i].Milestone)
                {
                    i--;
                }
                resultModel.PriceWon = (i < 0)? 0 : scoreBoard[i].Price;
            }
        }
        switch (gameStatus)
        {
            case GameStatus.AnsweringQuestion:
                resultModel.Message = "You gave up like noob, here is your reward";
                break;
            case GameStatus.AnsweredWrong:
                resultModel.Message = "You made a mistake, you got";
                break;
            case GameStatus.WonGame:
                resultModel.Message = "Nice you won";
                break;
            default:
                resultModel.Message = "You expected prize fine, here you got";
                break;
        }
        _memoryService.EndGame(gameId);
        return resultModel;
    }
}