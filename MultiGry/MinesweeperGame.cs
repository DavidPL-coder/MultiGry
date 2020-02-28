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
            string[] SelectedIndexes = Console.ReadLine().Split();

            if (SelectedIndexes.Length != 2)
            {
                Console.WriteLine("Wprowadzono nieprawidłowe wartości!");
            }

            else
            {
                var Random = new Random();

                for (int i = 0; i < 10; ++i)
                {
                    int Vertical = Random.Next(0, VerticalDimensionOfBoard);
                    int Horizontal = Random.Next(0, HorizontalDimensionOfBoard);

                    ActualBoardContent[Vertical, Horizontal] = '⁕';
                }

                var IndexesOfSelectedField = new int[]
                {
                    int.Parse(SelectedIndexes[0]),
                    int.Parse(SelectedIndexes[1])
                };

                bool AreFieldsToBeUncoveredUpwards = Random.Next(0, 2) == 1;
                bool AreFieldsToBeUncoveredLeft = Random.Next(0, 2) == 1;

                int VerticalIndexOfBeginningOfSquare;
                int HorizontalIndexOfBeginningOfSquare;
                int VerticalIndexOfEndOfSquare;
                int HorizontalIndexOfEndOfSquare;

                if (AreFieldsToBeUncoveredUpwards)
                {
                    VerticalIndexOfBeginningOfSquare = (IndexesOfSelectedField[0] - 2) < 0 ? 0 : (IndexesOfSelectedField[0] - 2);
                    VerticalIndexOfEndOfSquare = IndexesOfSelectedField[0];
                }

                else
                {
                    VerticalIndexOfBeginningOfSquare = IndexesOfSelectedField[0];
                    VerticalIndexOfEndOfSquare = (IndexesOfSelectedField[0] + 2) > 7 ? 7 : (IndexesOfSelectedField[0] + 2);
                }

                if (AreFieldsToBeUncoveredLeft)
                {
                    HorizontalIndexOfBeginningOfSquare = (IndexesOfSelectedField[1] - 2) < 0 ? 0 : (IndexesOfSelectedField[1] - 2);
                    HorizontalIndexOfEndOfSquare = IndexesOfSelectedField[1];
                }

                else
                {
                    HorizontalIndexOfBeginningOfSquare = IndexesOfSelectedField[1];
                    HorizontalIndexOfEndOfSquare = (IndexesOfSelectedField[1] + 2) > 7 ? 7 : (IndexesOfSelectedField[1] + 2);
                }

                for (int i = VerticalIndexOfBeginningOfSquare; i <= VerticalIndexOfEndOfSquare; ++i)
                {
                    for (int j = HorizontalIndexOfBeginningOfSquare; j < HorizontalIndexOfEndOfSquare; ++j)
                    {
                        if (ActualBoardContent[i, j] != '⁕')
                            DisplayedBoard[i, j] = ActualBoardContent[i, j];
                    }
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
                Console.Write(( i + 1 ) + "|");
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
