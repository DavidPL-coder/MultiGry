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
        private HandShapes OptionSelectedByUser;
        private HandShapes OptionDrawnByComputer;
        private int Draws;
        private int UserPoints;
        private int ComputerPoints;

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            GetNumberOfRoundsFromUser();
            PlayingRounds();
            DisplayResultsOfGame();
            Console.ReadKey();     

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }


        private void SetDefaults() => 
            Draws = UserPoints = ComputerPoints = 0;

        private void GetNumberOfRoundsFromUser()
        {
            Console.Clear();
            Console.Write("Podaj ilość rund, w których zmierzysz się z botem: ");
            try
            {
                TryGetNumberOfRoundsFromUser();
            }
            catch (OverflowException)
            {
                DisplayMessage("Nie można rozegrać tylu rund! Dozwolona wartość 1-255.");
                GetNumberOfRoundsFromUser();
            }
            catch (FormatException)
            {
                DisplayMessage("Wartość jest nieprawidłowa");
                GetNumberOfRoundsFromUser();
            }     
        }

        private void TryGetNumberOfRoundsFromUser()
        {
            Rounds = byte.Parse(Console.ReadLine());
            if (Rounds == 0)
                throw new OverflowException();
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void PlayingRounds()
        {
            for (CurrentRound = 1; CurrentRound <= Rounds; ++CurrentRound)
            {                
                DisplayingHandShapeSelection();
                GetSelectingOption();

                if (!CheckSelectedRightOption())
                    DisplayMessageAboutWrongSelection();   

                else
                {
                    ComputerSelectionDraw();
                    CheckWhoWin();
                    System.Threading.Thread.Sleep(1500);
                }            
            }
        }

        private void DisplayingHandShapeSelection()
        {
            Console.Clear();
            Console.WriteLine("Wybierz:");
            Console.WriteLine("1. Papier");
            Console.WriteLine("2. Kamień");
            Console.WriteLine("3. Nożyce");
        }

        private void GetSelectingOption() =>
            OptionSelectedByUser = (HandShapes)(Console.ReadKey(true).Key - ConsoleKey.D0);

        private bool CheckSelectedRightOption() => 
            (int)OptionSelectedByUser >= 1 && (int)OptionSelectedByUser <= 3;

        private void DisplayMessageAboutWrongSelection()
        {
            Console.WriteLine("Niewłaściwa opcja! Możesz wybrać tylko Papier(1), Kamień(2), Nożyce(3).");
            --CurrentRound;                          // they reduce "CurrentRound" we prevent us from going to the next round and this means that the user can repeat his choice.
            System.Threading.Thread.Sleep(2000);
        }

        private void ComputerSelectionDraw()
        {
            var random = new Random();
            OptionDrawnByComputer = (HandShapes)random.Next(1, 4);
        }

        private void CheckWhoWin()
        {
            if (OptionSelectedByUser == OptionDrawnByComputer)
            {
                Console.WriteLine("Remis!");
                ++Draws;
            }

            else if (OptionSelectedByUser == HandShapes.Paper && OptionDrawnByComputer == HandShapes.Rock)
            {
                Console.WriteLine("WYGRYWASZ (bot wybrał kamień)");
                ++UserPoints;
            }

            else if (OptionSelectedByUser == HandShapes.Rock && OptionDrawnByComputer == HandShapes.Paper)
            {
                Console.WriteLine("PRZEGRYWASZ (bot wybrał papier)");
                ++ComputerPoints;
            }

            else if (OptionSelectedByUser == HandShapes.Rock && OptionDrawnByComputer == HandShapes.Scissor)
            {
                Console.WriteLine("WYGRYWASZ (bot wybrał nożyczki)");
                ++UserPoints;
            }

            else if (OptionSelectedByUser == HandShapes.Scissor && OptionDrawnByComputer == HandShapes.Rock)
            {
                Console.WriteLine("PRZEGRYWASZ (bot wybrał kamień)");
                ++ComputerPoints;
            }

            else if (OptionSelectedByUser == HandShapes.Paper && OptionDrawnByComputer == HandShapes.Scissor)
            {
                Console.WriteLine("PRZEGRYWASZ (bot wybrał nożyczki)");
                ++ComputerPoints;
            }

            else if (OptionSelectedByUser == HandShapes.Scissor && OptionDrawnByComputer == HandShapes.Paper)
            {
                Console.WriteLine("WYGRYWASZ (bot wybrał papier)");
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
    }
}
