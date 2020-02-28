using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        private char[,] DisplayedBoard;
        private char[,] ActualBoardContent;
        private const int VerticalDimensionOfBoard = 8;
        private const int HorizontalDimensionOfBoard = 8;
        private ConsoleColor RightTextColor;
        private int RoundNumber;

        public OptionsCategory OptionExecuting()
        {
            RoundNumber = 1;
            SetBoard();

            while (IsGameStillGoingOn())
            {
                DisplayBoard(); 

                if (RoundNumber == 1)                    
                    StartingGame();

                else
                    PlayingRound();
            }

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private bool IsGameStillGoingOn()
        {
            return true;
        }

        private void PlayingRound()
        {
            Console.WriteLine();
        }

        private void StartingGame()
        {
            DisplayFieldIndexQuery();
            string[] IndexesOfFieldSelectedByUser = Console.ReadLine().Split();

            if (IndexesOfFieldSelectedByUser.Length != 2)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe wartości!");
            }

            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    var Random = new Random();
                    int Vertical = Random.Next(0, VerticalDimensionOfBoard);
                    int Horizontal = Random.Next(0, HorizontalDimensionOfBoard);

                    ActualBoardContent[Vertical, Horizontal] = '⁕';
                }
            }
                
        }

        private void SetBoard()
        {
            DisplayedBoard = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];
            ActualBoardContent = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];

            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                {
                    DisplayedBoard[i, j] = '■';
                    ActualBoardContent[i, j] = '☐';
                }     
            }
        }

        private void DisplayBoard()
        {
            RightTextColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  - - - - - - - -");
            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                Console.Write((i + 1) + "|");
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                    Console.Write(DisplayedBoard[i, j] + " ");

                Console.WriteLine();
            }
        }

        private void DisplayFieldIndexQuery()
        {
            Console.ForegroundColor = RightTextColor;
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji poziomy indeks): ");
        }
    }
}
