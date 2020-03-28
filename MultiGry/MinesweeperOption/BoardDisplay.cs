using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class BoardDisplay
    {
        private readonly char[,] DisplayedBoard;
        private readonly int VerticalDimensionOfBoard;
        private readonly int HorizontalDimensionOfBoard;

        public BoardDisplay(char[,] DisplayedBoard)
        {
            this.DisplayedBoard = DisplayedBoard;
            VerticalDimensionOfBoard = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimensionOfBoard = MinesweeperGame.HorizontalDimensionOfBoard;
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
            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                Console.Write(i + 1 + "|");
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                {
                    SetColorForCharacter(i, j);
                    Console.Write(DisplayedBoard[i, j] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        private void SetColorForCharacter(int i, int j)
        {
            switch (DisplayedBoard[i, j])
            {
                case '*': Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 'C': Console.ForegroundColor = ConsoleColor.Yellow; break;
                case '1': Console.ForegroundColor = ConsoleColor.Blue; break;
                case '2': Console.ForegroundColor = ConsoleColor.Green; break;
                case '3': Console.ForegroundColor = ConsoleColor.Red; break;
                case '4': Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case '5':
                case '6':
                case '7':
                case '8':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
        }
    }
}
