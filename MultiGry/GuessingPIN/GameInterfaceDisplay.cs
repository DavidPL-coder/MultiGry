using System;

namespace MultiGry.GuessingPIN
{
    class GameInterfaceDisplay
    {
        private PerformerRoundOfGame PerformerRound;

        public GameInterfaceDisplay(PerformerRoundOfGame PerformerRound) => 
            this.PerformerRound = PerformerRound;

        public void DisplayGameInterface()
        {
            Console.WriteLine("Próba: " + PerformerRound.UserAttempt);
            DisplayContentOfDisplayedCharacters();
            Console.WriteLine("Podaj cyfrę! (Wciśnij klawisz 0-9)");
            DisplayEnteredDigits();
        }

        private void DisplayContentOfDisplayedCharacters()
        {
            foreach (var item in PerformerRound.DisplayedCharacters)
                Console.Write(item);

            Console.WriteLine();
        }

        private void DisplayEnteredDigits()
        {
            Console.Write("Podane cyfry: ");
            if (PerformerRound.UserAttempt == 1)
                Console.Write("brak");

            foreach (var item in PerformerRound.EnteredDigitsFromUser)
                Console.Write(item + " ");

            Console.WriteLine();
        }
    }
}
