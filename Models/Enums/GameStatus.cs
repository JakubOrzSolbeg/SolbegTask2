namespace SolbegTask2.Models.Enums;

public enum GameStatus : byte
{
    AnsweringQuestion = 0,
    AnsweredWrong = 1,
    AnsweredRight = 2,
    WonGame = 3,
    GaveUp = 4,
    NewGame = 5,
}