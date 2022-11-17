using System.Diagnostics.CodeAnalysis;

namespace SolbegTask2.Models;

public class Game
{
    [AllowNull]
    public Question Question { get; set; }
    [AllowNull]
    public List<QuestionInfo> QuestionInfo { get; set; }
    [AllowNull]
    public ActionButton AwaibleAction { get; set; }
    
}