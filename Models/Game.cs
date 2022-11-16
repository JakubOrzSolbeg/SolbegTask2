using System.Diagnostics.CodeAnalysis;

namespace SolbegTask2.Models;

public class Game
{
    public string Question { get; set; } = "";
    public string AnsewrA { get; set; } = "";
    public string AnsewrB { get; set; } = "";
    public string AnsewrC { get; set; } = "";
    public string AnsewrD { get; set; } = "";
    public List<Question> QuestionInfo { get; set; }
    public ActionButton AwaibleAction { get; set; }
    
}