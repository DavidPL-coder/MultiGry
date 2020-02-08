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
        private int CurrentRound;
        private int UserPoints;
        private int ComputerPoints;
        private int Draw;
        private HandShapes OptionChosenByUser;
        private HandShapes OptionDrawnByComputer;

        public OptionsCategory OptionExecuting()
        {
            GetNumberOfRoundsFromUser();
            PlayingRounds();

            return OptionsCategory.Game;
        }

        private void PlayingRounds()
        {
            for (CurrentRound = 1; CurrentRound <= Rounds; ++CurrentRound)
            {
                Console.Clear();
                DisplayingHandShapeSelection();
                DownloadingSelectingOption();
                if (!CheckSelectedOption())
                    break;

                ComputerSelectionDraw();
                CheckWhoWin();
                System.Threading.Thread.Sleep(2000);
            }
        }

        private bool CheckSelectedOption()
        {
            if ((int)OptionChosenByUser < 1 || (int)OptionChosenByUser > 3)
            {
                Console.WriteLine("Niewłaściwa opcja! Możesz wybrać tylko Papier(1), Kamień(2), Nożyce(3).");
                --CurrentRound;                          // they reduce "CurrentRound" we prevent us from going to the next round and this means that the user can repeat his choice.
                System.Threading.Thread.Sleep(2000);
                return false;
            }

            else
                return true;
        }

        private void CheckWhoWin()
        {
            if (OptionChosenByUser == OptionDrawnByComputer)
            {
                Console.WriteLine("Remis!");
                ++Draw;
            }

            else if (OptionChosenByUser == HandShapes.Paper && OptionDrawnByComputer == HandShapes.Rock)
            {
                Console.WriteLine("Wybrałeś papier, więc WYGRYWASZ bo wylosowano kamień");
                ++UserPoints;
            }

            else if (OptionChosenByUser == HandShapes.Rock && OptionDrawnByComputer == HandShapes.Paper)
            {
                Console.WriteLine("Wybrałeś kamień, więc PRZEGRYWASZ bo wylosowano papier");
                ++ComputerPoints;
            }

            else if (OptionChosenByUser == HandShapes.Rock && OptionDrawnByComputer == HandShapes.Scissor)
            {
                Console.WriteLine("Wybrałeś kamień, więc WYGRYWASZ bo wylosowano nożyczki");
                ++UserPoints;
            }

            else if (OptionChosenByUser == HandShapes.Scissor && OptionDrawnByComputer == HandShapes.Rock)
            {
                Console.WriteLine("Wybrałeś nożyczki, więc PRZEGRYWASZ bo wylosowano kamień");
                ++ComputerPoints;
            }

            else if (OptionChosenByUser == HandShapes.Paper && OptionDrawnByComputer == HandShapes.Scissor)
            {
                Console.WriteLine("Wybrałeś papier, więc PRZEGRYWASZ bo wylosowano nożyczki");
                ++ComputerPoints;
            }

            else if (OptionChosenByUser == HandShapes.Scissor && OptionDrawnByComputer == HandShapes.Paper)
            {
                Console.WriteLine("Wybrałeś nożyczki, więc WYGRYWASZ bo wylosowano papier");
                ++UserPoints;
            }
        }

        private void GetNumberOfRoundsFromUser()
        {
            Console.Clear();
            Console.WriteLine("Podaj ilość rund, w których zmierzysz się z botem: ");
            try
            {
                Rounds = byte.Parse(Console.ReadLine());

                if (Rounds == 0)
                    throw new OverflowException("Liczba rund nie może być równe 0!");
            }
            catch (OverflowException overflowEception)
            {
                Console.WriteLine(overflowEception.Message + " Dozwolona wartość 1-255.");
                GetNumberOfRoundsFromUser();
            }
            catch (FormatException)
            {
                Console.WriteLine("Wartość jest nieprawidłowa");
                GetNumberOfRoundsFromUser();
            }
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
