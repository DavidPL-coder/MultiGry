using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class HangmanGame : IMenuOption
    {
        public string NameOption => "Wisielec";
        private string[] HangmanDrawing;
        private string[] AllWords;
        private int NumberOfUserErrors;
        private string RandomWord;
        private string GuessedLetters;
        private char PlayerLetter;

        public HangmanGame()
        {
            SetHangmanGrawing();
            SetAllWords();
        }

        private void SetHangmanGrawing()
        {
            HangmanDrawing = new string[]
            {
                @"  ___________",
                @"  |    |    |",
                @"  |    |    |",
                @"  |    |    |",
                @"  |    O    |",
                @"  |   /|\   |",
                @"  |  / | \  |",
                @"  |    |    |",
                @"  |    |    |",
                @"  |   / \   |",
                @"  |  /   \  |",
                @"  |         |",
                @"  |         |",
                @" /|\       /|\",
                @"/ | \     / | \"
            };
        }

        private void SetAllWords()
        {
            AllWords = new string[]
            {
                "telefon",
                "komputer",
                "rewolwer",
                "autostrada",
                "huragan",
                "kompresja",
                "kasztan",
                "helikopter",
                "kamper",
                "butelka",
                "kaskader",
                "laptop",
                "komputer",
                "myszka",
                "telefon",
                "pilot",
                "koniunkcja",
                "operator",
                "stolik",
                "pastuch",
                "owca",
                "paluszki",
                "krakersy",
                "serwis",
                "mieszkanie",
                "balon",
                "obiad",
                "karygodny",
                "krokodyl",
                "autostrada",
                "policja",
                "konfident",
                "bachor",
                "kataklizm",
                "wariatka",
                "kontakt"
            };
        }


        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            DisplayGuessedLetters();

            PlayingGames();

            GameSummary();
            Console.ReadKey();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }


        private void SetDefaults()
        {
            NumberOfUserErrors = 0;
            DrawWordToDuess();
            SetDefaultValueForGuessedLetters();
        }

        private void DrawWordToDuess()
        {
            var Random = new Random();
            int RandomNumberWord = Random.Next(0, AllWords.Length);
            RandomWord = AllWords[RandomNumberWord];
        }

        // if the letter is not guessed, underscore is inserted
        private void SetDefaultValueForGuessedLetters()
        {
            var tmp = new StringBuilder();
            for (int i = 0; i < RandomWord.Length; ++i)
                tmp.Append("_");

            GuessedLetters = tmp.ToString();
        }

        private void DisplayGuessedLetters()
        {
            for (int i = 0; i < GuessedLetters.Length; ++i)
                Console.Write(GuessedLetters[i] + " ");
        }

        private void PlayingGames()
        {
            while (!IsGameOver())
            {
                UserGuessingLetter();
                DisplayGuessedLetters();
                Console.WriteLine("\n");
                DisplayHangmanItems();
            }
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < GuessedLetters.Length; ++i)
                if (GuessedLetters[i] == '_')
                    return NumberOfUserErrors == HangmanDrawing.Length;

            return true;
        }

        private void UserGuessingLetter()
        {
            UserGivesLetter();
            Console.Clear();

            if (DidUserGuessedLetter())
                DisclosureOfGuessedLetters();

            else
                ++NumberOfUserErrors;
        }

        private void UserGivesLetter() =>
            PlayerLetter = Console.ReadKey().KeyChar;

        private bool DidUserGuessedLetter()
        {
            foreach (var item in RandomWord)
                if (PlayerLetter == item)
                    return true;

            return false;
        }

        private void DisclosureOfGuessedLetters()
        {
            for (int i = 0; i < RandomWord.Length; ++i)
                if (PlayerLetter == RandomWord[i])
                {
                    var tmp = new StringBuilder(GuessedLetters);
                    tmp[i] = RandomWord[i];
                    GuessedLetters = tmp.ToString();
                }
        }

        private void DisplayHangmanItems()
        {
            for (int i = 0; i < NumberOfUserErrors; ++i)
                Console.WriteLine(HangmanDrawing[i]);
        }

        private void GameSummary()
        {
            Console.Clear();

            if (DidUserWin())
                Console.WriteLine("Odgadłeś wyraz " + RandomWord + "!");

            else
                Console.WriteLine("Nie udało ci się odgadnąć wyrazu " + RandomWord);

            Console.WriteLine("Ilość błędów: " + NumberOfUserErrors);  
        }

        private bool DidUserWin()
        {
            for (int i = 0; i < GuessedLetters.Length; ++i)
                if (GuessedLetters[i] == '_')
                    return false;

            return true;
        }
    }
}
