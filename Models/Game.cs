using System.Diagnostics.CodeAnalysis;
using SolbegTask2.Models.Dto;
using SolbegTask2.Models.Enums;

namespace SolbegTask2.Models;

public class Game
{
    [AllowNull]
    public QuestionDto Question { get; set; }
    [AllowNull]
    public List<QuestionInfo> QuestionInfo { get; set; }
    [AllowNull]
    public ActionButton AwaibleAction { get; set; }
    
}