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
        private HandShapes OptionChosenByUser;
        private HandShapes OptionDrawnByComputer;
        private int Draws;
        private int UserPoints;
        private int ComputerPoints;
        
        public PaperRockScissorsGame()
        {
            Draws = 0;
            UserPoints = 0;
            ComputerPoints = 0;
        }

        public OptionsCategory OptionExecuting()
        {
            GetNumberOfRoundsFromUser();
            PlayingRounds();
            DisplayResultsOfGame();
            Console.ReadKey();  
            ResetPlayerPoints();
            return UserDecidesWhatToDoNext();
        }

        private void GetNumberOfRoundsFromUser()
        {
            Console.Clear();
            Console.Write("Podaj ilość rund, w których zmierzysz się z botem: ");
            try
            {
                Rounds = byte.Parse(Console.ReadLine());

                if (Rounds == 0)
                    throw new OverflowException("Liczba rund nie może być równe 0!");
            }
            catch (OverflowException overflowEception)
            {
                Console.WriteLine(overflowEception.Message + " Dozwolona wartość 1-255.");
                System.Threading.Thread.Sleep(2500);
                GetNumberOfRoundsFromUser();
            }
            catch (FormatException)
            {
                Console.WriteLine("Wartość jest nieprawidłowa");
                System.Threading.Thread.Sleep(2500);
                GetNumberOfRoundsFromUser();
            }
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

                // we display options again to hide the key selected by the user:
                Console.Clear();
                DisplayingHandShapeSelection();   
                
                ComputerSelectionDraw();
                CheckWhoWin();
                System.Threading.Thread.Sleep(2000);
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

        private void ComputerSelectionDraw()
        {
            var random = new Random();
            OptionDrawnByComputer = (HandShapes)random.Next(1, 4);
        }

        private void CheckWhoWin()
        {
            if (OptionChosenByUser == OptionDrawnByComputer)
            {
                Console.WriteLine("Remis!");
                ++Draws;
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

        private void DisplayResultsOfGame()
        {
            Console.Clear();
            Console.WriteLine("Wyniki:");
            Console.WriteLine("Twoje punkty: " + UserPoints);
            Console.WriteLine("Punkty bota: " + ComputerPoints);
            Console.WriteLine("Remisy: " + Draws);
        }

        private void ResetPlayerPoints() =>
            Draws = UserPoints = ComputerPoints = 0;

        private OptionsCategory UserDecidesWhatToDoNext()
        {
            Console.Clear();
            Console.WriteLine("Co dalej chcesz robić?");
            Console.WriteLine("1. Zagrać jeszcze raz");
            Console.WriteLine("2. Powrócić do Menu");
            Console.WriteLine("3. Wyjść z programu");

            var KeyChosenByPlayer = Console.ReadKey().Key;

            if (KeyChosenByPlayer == ConsoleKey.D1)
                return OptionExecuting();

            if (KeyChosenByPlayer == ConsoleKey.D2)
                return OptionsCategory.Game;

            Console.Clear();

            if (KeyChosenByPlayer == ConsoleKey.D3)
            {         
                var ExitFromProgram = new ExitOption();
                return ExitFromProgram.OptionExecuting();
            }

            else
                return UserDecidesWhatToDoNext();
        }
    }
}
