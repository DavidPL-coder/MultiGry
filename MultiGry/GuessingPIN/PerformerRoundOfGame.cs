using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiGry.GuessingPIN
{
    class PerformerRoundOfGame
    {
        public int UserAttempt { private set; get; }
        public char[] DisplayedCharacters { private set; get; }
        public List<int> EnteredDigitsFromUser { private set; get; }
        private int UserDigit;
        private readonly int[] RandomPINnumbers;

        public PerformerRoundOfGame(int[] RandomPINnumbers)
        {
            UserAttempt = 0;
            DisplayedCharacters = "????".ToArray();
            EnteredDigitsFromUser = new List<int>();
            this.RandomPINnumbers = RandomPINnumbers;
        }

        public void RoundProcessing()
        {
            ++UserAttempt;
            DisplayGameInterface();
            GetDigitFromUser();
            TryToExposeDigitOfPIN();
        }

        private void DisplayGameInterface()
        {
            var GameInterfaceDisplay = new GameInterfaceDisplay(this);
            GameInterfaceDisplay.DisplayGameInterface();
        }

        private void GetDigitFromUser() =>
            UserDigit = Console.ReadKey(true).Key - ConsoleKey.D0;

        private void TryToExposeDigitOfPIN()
        {
            if (ValidateUserDigit())
            {
                InsertDigitInDisplayedCharacters();
                EnteredDigitsFromUser.Add(UserDigit);
            }

            else
                --UserAttempt; // this will cause the user to be able to try to 
                               // enter letter again in the round
        }

        private bool ValidateUserDigit()
        {
            if (!IsSelectedKeyDigit())
                DisplayMessage("Wybrałeś klawisz inny niż klawisze 0-9!");

            else if (HasDigitEverBeenEntered())
                DisplayMessage("Ta cyfra była już podana wcześniej!");

            else
                return true;

            return false;
        }

        private bool IsSelectedKeyDigit() =>
            UserDigit >= 0 && UserDigit <= 9;

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private bool HasDigitEverBeenEntered() => 
            EnteredDigitsFromUser.Contains(UserDigit);

        private void InsertDigitInDisplayedCharacters()
        {
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                if (UserDigit == RandomPINnumbers[i])
                    DisplayedCharacters[i] = UserDigit.ToString()[0];
        }

        public bool AreAllDigitsGuessed() =>
            !DisplayedCharacters.Contains('?');
    }
}
