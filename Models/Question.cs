namespace SolbegTask2.Models;

public class Question
{
    public int QuestionId { get; set; }
    public DateTime TimeAdded { get; set; }
    public string QuestionText { get; set; } = "";
    public string AnswerA { get; set; } = "";
    public string AnswerB { get; set; } = "";
    public string AnswerC { get; set; } = "";
    public string AnswerD { get; set; } = "";
    public int CorrectAnswerHash { get; set; } = -1;
}