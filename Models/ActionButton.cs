namespace SolbegTask2.Models;

public enum ButtonType : byte{
    Surrender = 0,
    GoNextQuestion = 1,
    GoToFailureScreen = 2,
    GoToWinScreen = 3
}

public class ActionButton
{
    public ButtonType ButtonType { get; set; }
    public string CustomText { get; set; } = "";
}