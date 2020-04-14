using System;

namespace MultiGry.Minesweeper
{
    class BoardDisplay
    {
        private readonly char[,] DisplayedBoard;
        private readonly int VerticalDimension;
        private readonly int HorizontalDimension;

        public BoardDisplay(char[,] DisplayedBoard)
        {
            this.DisplayedBoard = DisplayedBoard;
            VerticalDimension = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimension = MinesweeperGame.HorizontalDimensionOfBoard;
        }

        public void DisplayContent()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  - - - - - - - -");
            DisplayVerticalBoardLines();
        }

        private void DisplayVerticalBoardLines()
        {
            for (int i = 0; i < VerticalDimension; ++i)
            {
                Console.Write(i + 1 + "|");
                for (int j = 0; j < HorizontalDimension; ++j)
                {
                    Console.ForegroundColor = GetColorForCharacter(i, j);
                    Console.Write(DisplayedBoard[i, j] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        private ConsoleColor GetColorForCharacter(int i, int j)
        {
            switch (DisplayedBoard[i, j])
            {
                case MinesweeperGame.BombSign: return ConsoleColor.DarkRed;
                case MinesweeperGame.FlagSign: return ConsoleColor.Yellow;
                case '1': return ConsoleColor.Blue;
                case '2': return ConsoleColor.Green;
                case '3': return ConsoleColor.Red;
                case '4': return ConsoleColor.DarkCyan;
                case '5':
                case '6':
                case '7':
                case '8':
                    return ConsoleColor.Magenta;

                default: return ConsoleColor.White;
            }
        }
    }
}
