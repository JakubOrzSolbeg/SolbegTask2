namespace SolbegTask2.Models.Dto;

public class QuestionDto
{
    public string QuestionText { get; set; } = "";
    public AnswerDto AnswerA { get; set; }
    public AnswerDto AnswerB { get; set; }
    public AnswerDto AnswerC { get; set; }
    public AnswerDto AnswerD { get; set; }
}