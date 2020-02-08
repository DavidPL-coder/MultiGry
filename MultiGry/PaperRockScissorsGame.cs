using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class PaperRockScissorsGame : IMenuOption
    {
        public string NameOption => "Papier, kamień, nożyce";
        private enum HandShapes
        {
            Paper = 1, Rock, Scissor
        }
        private byte Rounds;
        private int UserPoints;
        private int ComputerPoints;
        private HandShapes OptionChosenByUser;
        private HandShapes OptionDrawnByComputer;

        public OptionsCategory OptionExecuting()
        {
            Console.WriteLine("Podaj ilość rund, w których zmierzysz się z botem: ");

            try
            {
                Rounds = byte.Parse(Console.ReadLine());
            }

            catch (InvalidOperationException NoValue)
            {

            }
            catch (OverflowException TooHighValue)
            {

            }

            DisplayingHandShapeSelection();
            DownloadingSelectingOption();
            ComputerSelectionDraw();

            if ((int)OptionChosenByUser < 1 || (int)OptionChosenByUser > 3)
                Console.WriteLine("Niewłaściwa opcja! Możesz wybrać tylko Papier(1), Kamień(2), Nożyce(3).");

            return OptionsCategory.Game;
        }


        private void DisplayingHandShapeSelection()
        {
            Console.WriteLine("Wybierz:");
            Console.WriteLine("1. Papier");
            Console.WriteLine("2. Kamień");
            Console.WriteLine("3. Nożyce");
        }

        private void DownloadingSelectingOption() =>
            OptionChosenByUser = (HandShapes)(Console.ReadKey().Key - ConsoleKey.D0);

        private void ComputerSelectionDraw()
        {
            var random = new Random();
            OptionDrawnByComputer = (HandShapes) random.Next(1, 4);
        }
    }
}
