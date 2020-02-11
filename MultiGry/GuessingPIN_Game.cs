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
        private ConsoleKey KeySelectedByUser;

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
        }

        private void GetDigitFromUser() =>
            KeySelectedByUser = Console.ReadKey().Key;

        private void TryToInsertDigitInDisplayedCharacters()
        {
            if (IsSelectedKeyDigit())
                InsertDigitInDisplayedCharacters();

            else
            {
                Console.WriteLine("Wybrałeś klawisz inny niż klawisze 0-9!");
                System.Threading.Thread.Sleep(2500);
                --UserAttempt;                          // this will cause the user to be able to retry
            }
        }

        private bool IsSelectedKeyDigit() =>
            KeySelectedByUser >= ConsoleKey.D0 && KeySelectedByUser <= ConsoleKey.D9;

        private void InsertDigitInDisplayedCharacters()
        {
            int UserDigit = KeySelectedByUser - ConsoleKey.D0;

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
                Console.WriteLine("Nie zgadłeś PINu! Twój czas: " + Timer.ElapsedMilliseconds / 1000.0 + " s");

            else
                Console.WriteLine("Zgadłeś PIN w ciągu " + Timer.ElapsedMilliseconds / 1000.0 + " s oraz w próbie " + UserAttempt);

            Console.Write("PIN: ");
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                Console.Write(RandomPINnumbers[i]);

            Console.ReadKey();
        }
    }
}
