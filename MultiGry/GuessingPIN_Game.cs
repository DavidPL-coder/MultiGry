using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class GuessingPIN_Game : IMenuOption
    {
        public string NameOption => "Zgadywanie PINu";
        private int UserAttempt;
        private const int MaximumNumberOfAttempts = 7;
        private string DisplayedCharacters;
        private Stopwatch Timer;
        private int[] RandomPINnumbers;
        private int UserDigit;

        private List<int> EnteredDigitsFromUser;

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            Timer.Start();
            PlayingGame();
            Timer.Stop();
            DisplayResults();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }


        private void SetDefaults()
        {
            UserAttempt = 1;
            DisplayedCharacters = "????";
            Timer = new Stopwatch();
            RandomPINnumbers = new int[4];
            DrawingPINnumbers();
            EnteredDigitsFromUser = new List<int>();
        }

        private void DrawingPINnumbers()
        {
            var random = new Random();

            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                RandomPINnumbers[i] = random.Next(0, 10);
        }

        private void PlayingGame()
        {
            while (UserAttempt <= MaximumNumberOfAttempts)
            {
                DisplayGameInterface();
                GetDigitFromUser();
                TryToInsertDigitInDisplayedCharacters();

                if (AreAllDigitsGuessed())
                    break;

                ++UserAttempt;
                Console.Clear();
            }
        }

        private void DisplayGameInterface()
        {
            Console.WriteLine("Próba: " + UserAttempt);
            Console.WriteLine(DisplayedCharacters);
            Console.WriteLine("Podaj cyfrę! (Wciśnij klawisz 0-9)");
            DisplayEnteredDigits();
        }

        private void DisplayEnteredDigits()
        {
            Console.Write("Podane cyfry: ");
            if (UserAttempt == 1)
                Console.Write("brak");

            foreach (var item in EnteredDigitsFromUser)
                Console.Write(item + " ");

            Console.WriteLine();
        }

        private void GetDigitFromUser() =>
            UserDigit = Console.ReadKey(true).Key - ConsoleKey.D0;

        private void TryToInsertDigitInDisplayedCharacters()
        {
            if (IsSelectedKeyDigit())
            {
                if (HasDigitEverBeenEntered())
                {
                    DisplayMessage("Ta cyfra była już podana wcześniej!");
                    --UserAttempt;                                                  // this will cause the user to be able to retry
                }

                else
                {
                    InsertDigitInDisplayedCharacters();
                    EnteredDigitsFromUser.Add(UserDigit);
                }                
            }

            else
            {
                DisplayMessage("Wybrałeś klawisz inny niż klawisze 0-9!");
                --UserAttempt;                                                  
            }
        }

        private bool IsSelectedKeyDigit() =>
            UserDigit >= 0 && UserDigit <= 9;

        private bool HasDigitEverBeenEntered()
        {
            foreach (var item in EnteredDigitsFromUser)
                if (item == UserDigit)
                    return true;

            return false;
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void InsertDigitInDisplayedCharacters()
        {
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                if (UserDigit == RandomPINnumbers[i])
                {
                    var tmp = new StringBuilder(DisplayedCharacters);
                    string tmp_2 = UserDigit.ToString();

                    tmp[i] = tmp_2[0];

                    DisplayedCharacters = tmp.ToString();
                }
        }        

        private bool AreAllDigitsGuessed()
        {
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                if (DisplayedCharacters[i] == '?')
                    return false;

            return true;
        }

        private void DisplayResults()
        {
            Console.Clear();

            if (UserAttempt > MaximumNumberOfAttempts)
                Console.WriteLine("Nie zgadłeś PINu! Twój czas: " + Timer.ElapsedMilliseconds / 1000 + " sekund");

            else
                Console.WriteLine("Zgadłeś PIN w ciągu " + Timer.ElapsedMilliseconds / 1000 + " sekund oraz w próbie " + UserAttempt);

            Console.Write("PIN: ");
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                Console.Write(RandomPINnumbers[i]);

            Console.ReadKey();
        }
    }
}
