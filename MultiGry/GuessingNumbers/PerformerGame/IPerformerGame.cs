namespace MultiGry.GuessingNumbers
{
    public interface IPerformerGame
    {
        byte NumberToGuess { set; get; }
        int GameProcessing();
    }
}
