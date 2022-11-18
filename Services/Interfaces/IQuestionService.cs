using SolbegTask2.Models;
using SolbegTask2.Models.Dto;

namespace SolbegTask2.Services.Interfaces;

public interface IQuestionService
{
    public Task<string> AddNewQuestion(NewQuestion newQuestion);
    public Task<List<Question>> GetAllQuestions();
    public Task<bool> VerifyAnswer(int questionId, string answer);

    public Task<string> GetCorrectAnswer(int questionId);
    public Task<Question> GetRandomQuestion();
    public Task<Question?> GetQuestionById(int questionId);
}