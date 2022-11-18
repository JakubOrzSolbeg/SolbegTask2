using System.Text.Json.Serialization;

namespace SolbegTask2.Models;

public class Question
{
    [JsonIgnore]
    public int QuestionId { get; set; }
    [JsonIgnore]
    public DateTime TimeAdded { get; set; }
    public string QuestionText { get; set; } = "";
    public string CorrectAnswer { get; set; } = "";
    public string WrongAnswerB { get; set; } = "";
    public string WrongAnswerC { get; set; } = "";
    public string WrongAnswerD { get; set; } = "";
    [JsonIgnore]
    public int CorrectAnswerHash { get; set; } = -1;
}