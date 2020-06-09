using System;
using System.Text;

namespace MultiGry.Minesweeper
{
    public class BoardDisplay
    {
        private readonly char[,] displayedBoard;
        private IFakeConsole dummyConsole;
        private IGetterColorForCharacter getterColor;

        public BoardDisplay(char[,] displayedBoard)
        {
            this.displayedBoard = displayedBoard;
            dummyConsole = new FakeConsole();
            getterColor = new GetterColorForCharacter(displayedBoard);
        }

        public BoardDisplay(char[,] displayedBoard, IFakeConsole dummyConsole, IGetterColorForCharacter getterColor)
        {
            this.displayedBoard = displayedBoard;
            this.dummyConsole = dummyConsole;
            this.getterColor = getterColor;
        }

        public void DisplayContent()
        {
            dummyConsole.Clear();
            DisplayTopOfBoard();
            DisplayVerticalBoardLines();
        }

        private void DisplayTopOfBoard()
        {
            var numbers = new StringBuilder("  ");
            var line = new StringBuilder("  ");
            for (int i = 1; i <= MinesweeperGame.HorizontalDimensionOfBoard; ++i)
            {
                numbers.Append($"{i} ");
                line.Append($"- ");
            }
            Console.WriteLine($"{numbers}");
            Console.WriteLine($"{line}");
        }

        private void DisplayVerticalBoardLines()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
            {
                Console.Write(i + 1 + "|");
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                {
                    Console.ForegroundColor = getterColor.GetColorForCharacter(i, j);
                    Console.Write(displayedBoard[i, j] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
}
