namespace SolbegTask2.Models.Dto;

public class NewQuestion
{
    public string QuestionText { get; set; } = "";
    public string CorrectAnswer { get; set; } = "";
    public string WrongAnswer1 { get; set; } = "";
    public string WrongAnswer2 { get; set; } = "";
    public string WrongAnswer3 { get; set; } = "";
    public string ReturnMessage { get; set; } = "";
}