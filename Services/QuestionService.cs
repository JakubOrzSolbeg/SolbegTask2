using Microsoft.EntityFrameworkCore;
using SolbegTask2.DbContexts;
using SolbegTask2.Migrations;
using SolbegTask2.Models;
using SolbegTask2.Models.Dto;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class QuestionService : IQuestionService
{
    private readonly MainDbContext _dbContext;

    public QuestionService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<string> AddNewQuestion(NewQuestion newQuestion)
    {
        Question questionToBeAdded = new Question()
        {
            QuestionText = newQuestion.QuestionText,
            CorrectAnswer = newQuestion.CorrectAnswer,
            WrongAnswerB = newQuestion.WrongAnswer1,
            WrongAnswerC = newQuestion.WrongAnswer2,
            WrongAnswerD = newQuestion.WrongAnswer3,
            CorrectAnswerHash = newQuestion.CorrectAnswer.GetHashCode()
        };
        await _dbContext.Questions.AddAsync(questionToBeAdded);
        await _dbContext.SaveChangesAsync();
        return "Question added to pool";
    }

    public async Task<List<Question>> GetAllQuestions()
    {
        return await _dbContext.Questions.ToListAsync();
    }

    public async Task<bool> VerifyAnswer(int questionId, string answer)
    {
        var answerHash = answer.GetHashCode();
        var question = await this.GetQuestionById(questionId);
        if (question == null)
        {
            return false;
        }

        return question.CorrectAnswerHash.Equals(answerHash);
    }

    public async Task<string> GetCorrectAnswer(int questionId)
    {
        var result = await _dbContext.Questions
            .Where(q => q.QuestionId == questionId)
            .Select(q => q.CorrectAnswer)
            .FirstOrDefaultAsync();
        return result ?? "";
    }

    public async Task<Question> GetRandomQuestion()
    {
        var randomQuestion = await _dbContext.Questions.OrderBy(x => Guid.NewGuid()).Take(1).FirstAsync();
        return randomQuestion;
    }

    public async Task<Question?> GetQuestionById(int questionId)
    {
        var question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.QuestionId == questionId);
        return question;
    }
}