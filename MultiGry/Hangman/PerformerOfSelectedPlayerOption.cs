using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiGry.Hangman
{
    class PerformerOfSelectedPlayerOption
    {
        private List<char> LettersSelectedByUser;
        public int NumberOfUserErrors { private set; get; }
        private char PlayerLetter;
        private readonly string RandomWord;
        private char[] DisplayedCharacters;              
        private string PlayersWord;
        private readonly HangmanGame Game;

        public PerformerOfSelectedPlayerOption(HangmanGame Game)
        {
            LettersSelectedByUser = Game.LettersSelectedByUser;
            NumberOfUserErrors = Game.NumberOfUserErrors;
            RandomWord = Game.RandomWord;
            DisplayedCharacters = Game.DisplayedCharacters;

            this.Game = Game;
        }

        public void UserGuessingLetter()
        {
            var HangmanGameInterface = new HangmanGameInterface(Game);
            HangmanGameInterface.DisplayGameInterfaceWithoutOptions();
            UserGivesLetter();
            LetterProcessingFromUser();           
        }

        private void UserGivesLetter()
        {
            Console.WriteLine("\n" + "Podaj literę (kliknij właściwy klawisz):");
            PlayerLetter = Console.ReadKey(true).KeyChar;
        }

        private void LetterProcessingFromUser()
        {
            if (DisplayPossibleErrorMessagesWithLetters())
                return;

            else if (DidUserGuessedLetter())
                DisclosureOfGuessedLetters();

            else
                ++NumberOfUserErrors;

            LettersSelectedByUser.Add(PlayerLetter);
        }

        private bool DisplayPossibleErrorMessagesWithLetters()
        {
            if (char.IsLetter(PlayerLetter) == false)
                ErrorMessage("To nie jest litera!");

            else if (WasLetterEntered())
                ErrorMessage("Znak był już wprowadzany!");

            else
                return false;

            return true;
        }

        public void ErrorMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        private bool WasLetterEntered() => 
            LettersSelectedByUser.Contains(PlayerLetter);

        private bool DidUserGuessedLetter() => 
            RandomWord.Contains(PlayerLetter);

        private void DisclosureOfGuessedLetters()
        {
            for (int i = 0; i < RandomWord.Length; ++i)
                if (PlayerLetter == RandomWord[i])
                    DisplayedCharacters[i] = PlayerLetter;
        }

        public void UserGuessingWord()
        {
            var HangmanGameInterface = new HangmanGameInterface(Game);
            HangmanGameInterface.DisplayGameInterfaceWithoutOptions();
            UserGivesWord();
            RemoveWhitespaceFromPlayersWord();
            ResultOfGuessingWordByUser();
        }

        private void UserGivesWord()
        {
            Console.WriteLine("\n" + "Podaj słowo: ");
            PlayersWord = Console.ReadLine();
        }

        private void RemoveWhitespaceFromPlayersWord()
        {
            for (int i = 0; i < PlayersWord.Length; ++i)
            {
                if (PlayersWord[i] == ' ' || PlayersWord[i] == '\t')
                    PlayersWord = PlayersWord.Remove(i--, 1);
            }
        }

        private void ResultOfGuessingWordByUser()
        {
            // this will cause the IsGameOver function to return true 
            // and this will cause the player to be declared victorious
            if (PlayersWord == RandomWord)
                PlayersWord.ToList().CopyTo(DisplayedCharacters);

            else
            {
                ErrorMessage("Złe słowo!");
                ++NumberOfUserErrors;
            }
        }
    }
}
