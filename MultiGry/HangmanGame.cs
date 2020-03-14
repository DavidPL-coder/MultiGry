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
        private List<char> User_SelectedLetters;
        private int NumberOfUserErrors;
        private Random GeneratorNumber;
        private string RandomWord;
        private string GuessedLetters;
        private char PlayerLetter;                
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
            PlayingGames();
            GameSummary();
            Console.ReadKey();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }


        private void SetDefaults()
        {
            NumberOfUserErrors = 0;
            DrawWordToGuess();
            SetDefaultValueForGuessedLetters();
            User_SelectedLetters.Clear();
        }

        private void DrawWordToGuess()
        {
            string[] Words = GetWordsFromResourceFile();
            int RandomNumberWord = GeneratorNumber.Next(0, Words.Length);
            RandomWord = Words[RandomNumberWord];
        }

        private string[] GetWordsFromResourceFile() => 
            Properties.Resources.HangmanGameWords.Split(new string[] { "\r\n", " ", "\t" }, 
                                                        StringSplitOptions.RemoveEmptyEntries);

        // if the letter is not guessed, underscore is inserted
        private void SetDefaultValueForGuessedLetters()
        {
            var tmp = new StringBuilder();
            for (int i = 0; i < RandomWord.Length; ++i)
                tmp.Append("_");

            GuessedLetters = tmp.ToString();
        }

        private void PlayingGames()
        {
            while (!IsGameOver())
            {
                DisplayGameInterface();
                UserSelectsOptions();
            }
        }

        private bool IsGameOver()
        {
            for (int i = 0; i < GuessedLetters.Length; ++i)
                if (GuessedLetters[i] == '_')
                    return NumberOfUserErrors == HangmanDrawing.Length;

            return true;
        }

        private void DisplayGameInterface()
        {
            Console.Clear();
            DisplayGuessedLetters();
            DisplayLettersSelectedByUser();
            DisplayOptionsThatPlayerCanSelect();
            DisplayHangmanItems();
        }

        private void DisplayGuessedLetters()
        {
            for (int i = 0; i < GuessedLetters.Length; ++i)
                Console.Write(GuessedLetters[i] + " ");

            Console.WriteLine();
        }

        private void DisplayLettersSelectedByUser()
        {
            Console.Write("\n" + "Wprowadzane litery: ");

            if (User_SelectedLetters.Count == 0)
                Console.Write("brak");

            foreach (var item in User_SelectedLetters)
                Console.Write(item + " ");
        }

        private void DisplayOptionsThatPlayerCanSelect()
        {
            Console.WriteLine("\n\n" + "Wybierz opcję: ");
            Console.WriteLine("1. Podanie litery");
            Console.WriteLine("2. Odgadnięcie hasła");
        }

        private void DisplayHangmanItems()
        {
            Console.WriteLine();
            for (int i = 0; i < NumberOfUserErrors; ++i)
                Console.WriteLine(HangmanDrawing[i]);
        }

        private void UserSelectsOptions()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1: UserGuessingLetter(); break;
                case ConsoleKey.D2: UserGuessingWord(); break;
                default: ErrorMessage("Można tylko wybrać opcje 1 lub 2!"); break;
            }
        }

        private void UserGuessingLetter()
        {
            DisplayGameInterfaceWithoutOptions();
            UserGivesLetter();
            LetterProcessingFromUser();
            User_SelectedLetters.Add(PlayerLetter);
        }

        private void DisplayGameInterfaceWithoutOptions()
        {
            Console.Clear();
            DisplayGuessedLetters();
            DisplayLettersSelectedByUser();
            DisplayHangmanItems();
        }

        private void UserGivesLetter()
        {
            Console.WriteLine("\n" + "Podaj literę (naciśnij odpowiedni klawisz):");
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

        private void UserGuessingWord()
        {
            DisplayGameInterfaceWithoutOptions();
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
            if (PlayersWord == RandomWord)
                GuessedLetters = PlayersWord;   // this will cause the IsGameOver function to return true and this will cause the player to be declared victorious

            else
            {
                ErrorMessage("Złe słowo!");
                ++NumberOfUserErrors;
            }
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
