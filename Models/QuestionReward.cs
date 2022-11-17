using System.Text.Json.Serialization;

namespace SolbegTask2.Models;

public class QuestionReward
{
    [JsonPropertyName("price")]
    public int Price { get; set; }
    [JsonPropertyName("milestone")]
    public bool Milestone { get; set; }
}