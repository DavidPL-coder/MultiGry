using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class ChangeTextColorOption : IMenuOption
    {
        public string NameOption => "Zmień kolor tekstu";
        private ConsoleColor CurrentTextColor;

        public OptionsCategory OptionExecuting()
        {
            CurrentTextColor = Console.ForegroundColor;
            DisplayTextColorsToSelectFrom();
            SetTextColor();

            return OptionsCategory.NormalOption;
        }


        private void DisplayTextColorsToSelectFrom()
        {
            Console.WriteLine("Wybierz kolor:");
            DisplayBlueOptions();
            DisplayRedOptions();
            DisplayWhiteOptions();
            DisplayGreenOptions();
            DisplayYellowOptions();
            DisplayGrayOptions();
            DisplayPinkOptions();
            DisplayCyanOptions();
            DisplayCancelOptions();
        }

        private void DisplayBlueOptions()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Niebieski");
        }

        private void DisplayRedOptions()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2. Czerwony");
        }

        private void DisplayWhiteOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("3. Biały");
        }

        private void DisplayGreenOptions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("4. Zielony");
        }

        private void DisplayYellowOptions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("5. Żółty");
        }

        private void DisplayGrayOptions()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("6. Szary (domyślny)");
        }

        private void DisplayPinkOptions()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("7. Różowy");
        }

        private void DisplayCyanOptions()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("8. Cyjanowy");
        }

        private void DisplayCancelOptions()
        {
            Console.ForegroundColor = CurrentTextColor;
            Console.WriteLine("9. Anuluj");
        }

        private void SetTextColor()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: Console.ForegroundColor = ConsoleColor.Blue; break;
                case ConsoleKey.D2: Console.ForegroundColor = ConsoleColor.Red; break;
                case ConsoleKey.D3: Console.ForegroundColor = ConsoleColor.White; break;
                case ConsoleKey.D4: Console.ForegroundColor = ConsoleColor.Green; break;
                case ConsoleKey.D5: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case ConsoleKey.D6: Console.ForegroundColor = ConsoleColor.Gray; break;
                case ConsoleKey.D7: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case ConsoleKey.D8: Console.ForegroundColor = ConsoleColor.Cyan; break;
            }
        }
    }
}
