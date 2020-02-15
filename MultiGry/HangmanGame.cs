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
        private List<char> User_SelectedLetters;

        public HangmanGame()
        {
            SetHangmanGrawing();
            SetAllWords();
            User_SelectedLetters = new List<char>();
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
            User_SelectedLetters.Clear();
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

            Console.WriteLine();
        }

        private void PlayingGames()
        {
            while (!IsGameOver())
            {
                UserGuessingLetter();
                DisplayGuessedLetters();
                DisplayLettersSelectedByUser();
                Console.WriteLine();
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

            if (char.IsLetter(PlayerLetter) == false)
            {
                ErrorMessage("To nie jest litera!");
                return;
            }

            if (WasLetterEntered())
            {
                ErrorMessage("Znak był już wprowadzany!");
                return;
            }

            else if (DidUserGuessedLetter())
                DisclosureOfGuessedLetters();

            else
                ++NumberOfUserErrors;

            Console.Clear();
            User_SelectedLetters.Add(PlayerLetter);
        }

        private void UserGivesLetter() =>
            PlayerLetter = Console.ReadKey(true).KeyChar;

        private void ErrorMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        private bool WasLetterEntered()
        {
            foreach (var item in User_SelectedLetters)
                if (item == PlayerLetter)
                    return true;

            return false;
        }

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

        private void DisplayLettersSelectedByUser()
        {
            foreach (var item in User_SelectedLetters)
                Console.Write(item + " ");
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
