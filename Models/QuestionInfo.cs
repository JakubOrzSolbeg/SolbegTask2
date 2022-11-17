namespace SolbegTask2.Models;

public enum QuestionStatus : byte
{
    Unanswered = 0,
    DuringAnswer = 1,
    Answered = 2,
}

public class QuestionInfo
{
    public int Reward { get; set; }
    public QuestionStatus QuestionStatus { get; set; }
    public bool IsMilestone { get; set; } = false;
}