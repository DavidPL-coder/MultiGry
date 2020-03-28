using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class MinesCounterOnBoard
    {
        private char[,] DisplayedBoard;
        private readonly char[,] ActualBoardContent;
        private readonly Rect SquareOfExposedFields;
        private readonly int VerticalDimensionOfBoard;
        private readonly int HorizontalDimensionOfBoard;

        public MinesCounterOnBoard(MinesweeperGame Game, Rect SquareOfExposedFields)
        {
            DisplayedBoard = Game.DisplayedBoard;
            ActualBoardContent = Game.ActualBoardContent;
            this.SquareOfExposedFields = SquareOfExposedFields;

            VerticalDimensionOfBoard = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimensionOfBoard = MinesweeperGame.HorizontalDimensionOfBoard;
        }

        public void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int i = SquareOfExposedFields.Top; i <= SquareOfExposedFields.Bottom; ++i)
            {
                for (int j = SquareOfExposedFields.Left; j <= SquareOfExposedFields.Right; ++j)
                {
                    if (IsThereFieldWithSuchIndex(i, j) && ActualBoardContent[i, j] != '*')
                        DisplayNumberOfMinesInField(i, j);
                }
            }
        }

        private bool IsThereFieldWithSuchIndex(int i, int j) =>
            i >= 0 &&
            i < VerticalDimensionOfBoard &&
            j >= 0 &&
            j < HorizontalDimensionOfBoard;

        public int DisplayNumberOfMinesInField(int VerticalIndex, int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = CalculateHowManyMinesAreAroundField(VerticalIndex, HorizontalIndex);

            if (NumberDisplayedInGivenField != 0)
                DisplayedBoard[VerticalIndex, HorizontalIndex] = NumberDisplayedInGivenField.ToString()[0];

            else
                DisplayedBoard[VerticalIndex, HorizontalIndex] = 'O';

            return NumberDisplayedInGivenField;
        }

        private int CalculateHowManyMinesAreAroundField(int VerticalIndex, int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = 0;
            for (int i = VerticalIndex - 1; i <= VerticalIndex + 1; ++i)
            {
                for (int j = HorizontalIndex - 1; j <= HorizontalIndex + 1; ++j)
                {
                    if (IsThereFieldWithSuchIndex(i, j) && ActualBoardContent[i, j] == '*')
                        ++NumberDisplayedInGivenField;
                }
            }
            return NumberDisplayedInGivenField;
        }
    }
}
