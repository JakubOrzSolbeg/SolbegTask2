using System.Text.Json.Serialization;

namespace SolbegTask2.Models;

public class Question
{
    [JsonIgnore]
    public int QuestionId { get; set; }
    [JsonIgnore]
    public DateTime TimeAdded { get; set; }
    public string QuestionText { get; set; } = "";
    public string AnswerA { get; set; } = "";
    public string AnswerB { get; set; } = "";
    public string AnswerC { get; set; } = "";
    public string AnswerD { get; set; } = "";
    [JsonIgnore]
    public int CorrectAnswerHash { get; set; } = -1;
}