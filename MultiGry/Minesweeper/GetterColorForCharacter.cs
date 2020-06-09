using System;

namespace MultiGry.Minesweeper
{
    public class GetterColorForCharacter : IGetterColorForCharacter
    {
        private readonly char[,] displayedBoard;

        public GetterColorForCharacter(char[,] displayedBoard) => 
            this.displayedBoard = displayedBoard;

        public ConsoleColor GetColorForCharacter(int i, int j)
        {
            switch (displayedBoard[i, j])
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
