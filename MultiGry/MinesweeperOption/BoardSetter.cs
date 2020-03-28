using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class BoardSetter
    {
        public char[,] DisplayedBoard { private set; get; }
        public char[,] ActualBoardContent { private set; get; }
        private readonly int VerticalDimensionOfBoard;
        private readonly int HorizontalDimensionOfBoard;

        public BoardSetter(MinesweeperGame Game)
        {
            VerticalDimensionOfBoard = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimensionOfBoard = MinesweeperGame.HorizontalDimensionOfBoard;
        }

        public void CreateBoard()
        {
            DisplayedBoard = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];
            ActualBoardContent = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];

            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                {
                    DisplayedBoard[i, j] = '■';
                    ActualBoardContent[i, j] = 'O';
                }
            }
        }
    }
}
