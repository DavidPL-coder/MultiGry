using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiGry.Hangman
{
    class HangmanGame : IMenuOption
    {
        public string NameOption => "Wisielec";
        public HangmanCartoonist HangmanDraftsman { private set; get; }      
        public List<char> LettersSelectedByUser { private set; get; } 
        public int NumberOfUserErrors { private set; get; }
        public string RandomWord { private set; get; }
        public char[] DisplayedCharacters { private set; get; }  

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            PlayingGames();
            GameSummary();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            HangmanDraftsman = new HangmanCartoonist();
            HangmanDraftsman.CreateHangmanDrawing();

            NumberOfUserErrors = 0;

            var Getter = new GetterRandomWordAndDisplayedCharacters();
            RandomWord = Getter.RandomWord;
            DisplayedCharacters = Getter.DisplayedCharacters;

            LettersSelectedByUser = new List<char>();
        }

        private void PlayingGames()
        {
            while (!IsGameOver())
            {
                var HangmanGameInterface = new HangmanGameInterface(this);
                HangmanGameInterface.DisplayInterface();
                UserSelectsOptions();
            }
        }

        private bool IsGameOver() => 
            DisplayedCharacters.Contains('_') ? NumberOfUserErrors == 
                                                HangmanDraftsman.DrawingLength 
                                                : true;

        private void UserSelectsOptions()
        {
            var PerformerOption = new PerformerOfSelectedPlayerOption(this);

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1: PerformerOption.UserGuessingLetter(); break;
                case ConsoleKey.D2: PerformerOption.UserGuessingWord(); break;
                default: PerformerOption.ErrorMessage("Można tylko wybrać " +
                                                      "opcje 1 lub 2!"); break;
            }

            NumberOfUserErrors = PerformerOption.NumberOfUserErrors;
        }

        private void GameSummary()
        {
            Console.Clear();
            var GameScore = DidUserWin() ? "Odgadłeś wyraz " 
                                         : "Nie udało ci się odgadnąć wyrazu ";

            Console.WriteLine(GameScore + RandomWord);
            Console.WriteLine("Ilość błędów: " + NumberOfUserErrors);
            Console.ReadKey();
        }

        private bool DidUserWin() => 
            !DisplayedCharacters.Contains('_');
    }
}
