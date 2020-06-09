using System;

namespace MultiGry.Minesweeper
{
    public class MinesCounter
    {
        private char[,] displayedBoard;
        private readonly char[,] actualBoardContent;
        private readonly Rect exposedFields;

        public MinesCounter(char[,] displayedBoard, char[,] actualBoardContent, Rect squareOfExposedFields)
        {
            this.displayedBoard = displayedBoard;
            this.actualBoardContent = actualBoardContent;
            exposedFields = squareOfExposedFields;
        }

        public void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int i = exposedFields.Top; i <= exposedFields.Bottom; ++i)
            {
                for (int j = exposedFields.Left; j <= exposedFields.Right; ++j)
                {
                    if (CanNumberOfMinesBeDisplayedInField(i, j))
                        DisplayNumberOfMinesInField(i, j);
                }
            }
        }

        private bool CanNumberOfMinesBeDisplayedInField(int i, int j) => 
            IsThereFieldWithSuchIndex(i, j) && actualBoardContent[i, j] != MinesweeperGame.BombSign;

        private bool IsThereFieldWithSuchIndex(int i, int j) =>
            i >= 0 && i < MinesweeperGame.VerticalDimensionOfBoard && 
            j >= 0 && j < MinesweeperGame.HorizontalDimensionOfBoard;

        public int DisplayNumberOfMinesInField(int verticalIndex, int horizontalIndex)
        {
            int numberDisplayedInGivenField = CalculateHowManyMinesAreAroundField(verticalIndex, horizontalIndex);

            if (numberDisplayedInGivenField != 0)
                displayedBoard[verticalIndex, horizontalIndex] = numberDisplayedInGivenField.ToString()[0];

            else
                displayedBoard[verticalIndex, horizontalIndex] = MinesweeperGame.EmptyFieldSign;

            return numberDisplayedInGivenField;
        }

        private int CalculateHowManyMinesAreAroundField(int verticalIndex, int horizontalIndex)
        {
            int numberDisplayedInGivenField = 0;
            for (int i = verticalIndex - 1; i <= verticalIndex + 1; ++i)
            {
                for (int j = horizontalIndex - 1; j <= horizontalIndex + 1; ++j)
                {
                    if (IsThereBombInTheField(i, j))
                        ++numberDisplayedInGivenField;
                }
            }
            return numberDisplayedInGivenField;
        }

        private bool IsThereBombInTheField(int i, int j) => 
            IsThereFieldWithSuchIndex(i, j) && actualBoardContent[i, j] == MinesweeperGame.BombSign;
    }
}
