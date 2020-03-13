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
        //private string[] AllWords;
        private int NumberOfUserErrors;
        private string RandomWord;
        private string GuessedLetters;
        private char PlayerLetter;
        private List<char> User_SelectedLetters;
        private Random GeneratorNumber;
        private string PlayersWord;

        public HangmanGame()
        {
            SetHangmanGrawing();
            User_SelectedLetters = new List<char>();
            GeneratorNumber = new Random();    
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


        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            DisplayGuessedLetters();
            DisplayOptionsThatPlayerCanSelect();

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
            string[] Words = GetWordsFromResourceFile();
            int RandomNumberWord = GeneratorNumber.Next(0, Words.Length);
            RandomWord = Words[RandomNumberWord];
        }

        private string[] GetWordsFromResourceFile() => 
            Properties.Resources.HangmanGameWords.Split(new string[] { "\r\n", " " }, 
                                                        StringSplitOptions.RemoveEmptyEntries);

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
                UserSelectsOptions();

                DisplayGuessedLetters();
                DisplayLettersSelectedByUser();
                DisplayOptionsThatPlayerCanSelect();
                DisplayHangmanItems();
            }
        }

        private void UserSelectsOptions()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    UserGuessingLetter();
                    break;

                case ConsoleKey.D2:
                    UserGuessingWord();
                    break;

                default:
                    ErrorMessage("Można tylko wybrać opcje 1 lub 2!");
                    break;
            }
        }

        private void UserGuessingWord()
        {
            Console.Clear();
            DisplayGuessedLetters();
            DisplayLettersSelectedByUser();
            DisplayHangmanItems();            

            Console.WriteLine("\n" + "Podaj słowo: ");
            PlayersWord = Console.ReadLine();

            for (int i = 0; i < PlayersWord.Length; ++i)
                if (PlayersWord[i] == ' ' || PlayersWord[i] == '\t')
                    PlayersWord = PlayersWord.Remove(i--, 1);

            if (PlayersWord == RandomWord)
                GuessedLetters = PlayersWord;   // this will cause the IsGameOver function to return true and this will cause the player to be declared victorious

            else
            {
                ErrorMessage("Złe słowo!");
                ++NumberOfUserErrors;
            }
        }

        private void DisplayOptionsThatPlayerCanSelect()
        {
            Console.WriteLine("\n\n" + "Wybierz opcję: ");
            Console.WriteLine("1. Podanie litery");
            Console.WriteLine("2. Odgadnięcie hasła");
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
            Console.Clear();
            DisplayGuessedLetters();
            DisplayLettersSelectedByUser();
            DisplayHangmanItems();
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

        private void UserGivesLetter()
        {
            Console.WriteLine("\n" + "Podaj literę (naciśnij odpowiedni klawisz):");
            PlayerLetter = Console.ReadKey(true).KeyChar;
        }

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
            Console.Write("\n" + "Wprowadzane litery: ");

            if (User_SelectedLetters.Count == 0)
                Console.Write("brak");

            foreach (var item in User_SelectedLetters)
                Console.Write(item + " ");
        }

        private void DisplayHangmanItems()
        {
            Console.WriteLine();
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
