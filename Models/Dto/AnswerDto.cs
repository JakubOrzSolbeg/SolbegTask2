namespace SolbegTask2.Models.Dto;

public class AnswerDto
{
    public string Text { get; set; } = "";
    public bool Clickable { get; set; } = true;
    public bool IsWrong { get; set; } = false;
    public bool IsCorrect { get; set; } = false;
}