using System;
using System.Collections.Generic;

namespace MultiGry.Hangman
{
    class HangmanGameInterface
    {
        private readonly HangmanCartoonist HangmanDraftsman;
        private readonly List<char> LettersSelectedByUser;
        private readonly char[] DisplayedCharacters;
        private readonly int NumberOfUserErrors;

        public HangmanGameInterface(HangmanGame Game)
        {
            HangmanDraftsman = Game.HangmanDraftsman;
            LettersSelectedByUser = Game.LettersSelectedByUser;
            DisplayedCharacters = Game.DisplayedCharacters;
            NumberOfUserErrors = Game.NumberOfUserErrors;
        }

        public void DisplayInterface()
        {
            Console.Clear();
            DisplayContentOfDisplayedCharacters();
            DisplayLettersSelectedByUser();
            DisplayOptionsThatPlayerCanSelect();
            HangmanDraftsman.DisplayHangmanItems(NumberOfUserErrors);
        }

        private void DisplayContentOfDisplayedCharacters()
        {
            foreach (var item in DisplayedCharacters)
                Console.Write(item + " ");

            Console.WriteLine();
        }

        private void DisplayLettersSelectedByUser()
        {
            Console.Write("\n" + "Wprowadzane litery: ");

            if (LettersSelectedByUser.Count == 0)
                Console.Write("brak");

            foreach (var item in LettersSelectedByUser)
                Console.Write(item + " ");
        }

        private void DisplayOptionsThatPlayerCanSelect()
        {
            Console.WriteLine("\n\n" + "Wybierz opcję: ");
            Console.WriteLine("1. Podanie litery");
            Console.WriteLine("2. Odgadnięcie hasła");
        }

        public void DisplayInterfaceWithoutOptions()
        {
            Console.Clear();
            DisplayContentOfDisplayedCharacters();
            DisplayLettersSelectedByUser();
            HangmanDraftsman.DisplayHangmanItems(NumberOfUserErrors);
        }
    }
}
