using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class GuessingPIN_Game : IMenuOption
    {
        public string NameOption => "Zgadywanie PINu";
        private int UserAttempt;
        private string DisplayedCharacters;
        private int[] RandomPINnumbers;
        private ConsoleKey KeySelectedByUser;

        public GuessingPIN_Game()
        {
            UserAttempt = 1;
            DisplayedCharacters = "????";
            DrawingPINnumbers();
        }

        private void DrawingPINnumbers()
        {
            var random = new Random();
            RandomPINnumbers = new int[4];

            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                RandomPINnumbers[i] = random.Next(0, 10);
        }

        public OptionsCategory OptionExecuting()
        {
            DisplayGameInterface();
            GetDigitFromUser();
            if (IsSelectedKeyDigit())
            {
                for (int i = 0; i < RandomPINnumbers.Length; ++i)
                {
                    int UserDigit = KeySelectedByUser - ConsoleKey.D0;
                    if (UserDigit == RandomPINnumbers[i])
                    {
                        var tmp = new StringBuilder(DisplayedCharacters);
                        tmp[i] = Convert.ToChar(UserDigit);
                        DisplayedCharacters = tmp.ToString();
                    }
                }
            }

            return OptionsCategory.Game;
        }

        private bool IsSelectedKeyDigit() => 
            KeySelectedByUser >= ConsoleKey.D0 && KeySelectedByUser <= ConsoleKey.D9;

        private void GetDigitFromUser() => 
            KeySelectedByUser = Console.ReadKey().Key;

        private void DisplayGameInterface()
        {
            Console.WriteLine("Próba: " + UserAttempt);
            Console.WriteLine(DisplayedCharacters);
            Console.WriteLine("Podaj cyfrę! (Wciśnij klawisz 0-9)");
        }
    }
}
