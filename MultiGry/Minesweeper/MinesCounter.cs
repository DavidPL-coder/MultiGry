namespace MultiGry.Minesweeper
{
    class MinesCounter
    {
        private char[,] DisplayedBoard;
        private readonly char[,] ActualBoardContent;
        private readonly Rect ExposedFields;
        private readonly int VerticalDimension;
        private readonly int HorizontalDimension;

        public MinesCounter(MinesweeperGame Game, Rect SquareOfExposedFields)
        {
            DisplayedBoard = Game.DisplayedBoard;
            ActualBoardContent = Game.ActualBoardContent;
            ExposedFields = SquareOfExposedFields;

            VerticalDimension = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimension = MinesweeperGame.HorizontalDimensionOfBoard;
        }

        public void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int i = ExposedFields.Top; i <= ExposedFields.Bottom; ++i)
            {
                for (int j = ExposedFields.Left; j <= ExposedFields.Right; ++j)
                    if (CanNumberOfMinesBeDisplayedInField(i, j))
                        DisplayNumberOfMinesInField(i, j);
            }
        }

        private bool CanNumberOfMinesBeDisplayedInField(int i, int j) => 
            IsThereFieldWithSuchIndex(i, j) && 
            ActualBoardContent[i, j] != MinesweeperGame.BombSign;

        private bool IsThereFieldWithSuchIndex(int i, int j) =>
            i >= 0 &&
            i < VerticalDimension &&
            j >= 0 &&
            j < HorizontalDimension;

        public int DisplayNumberOfMinesInField(int VerticalIndex, int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = 
                CalculateHowManyMinesAreAroundField(VerticalIndex, HorizontalIndex);

            if (NumberDisplayedInGivenField != 0)
                DisplayedBoard[VerticalIndex, HorizontalIndex] = 
                    NumberDisplayedInGivenField.ToString()[0]; // Convert int to char

            else
                DisplayedBoard[VerticalIndex, HorizontalIndex] = 
                    MinesweeperGame.EmptyFieldSign;

            return NumberDisplayedInGivenField;
        }

        private int CalculateHowManyMinesAreAroundField(int VerticalIndex, 
                                                        int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = 0;
            for (int i = VerticalIndex - 1; i <= VerticalIndex + 1; ++i)
            {
                for (int j = HorizontalIndex - 1; j <= HorizontalIndex + 1; ++j)
                    if (IsThereBombInTheField(i, j))
                        ++NumberDisplayedInGivenField;
            }
            return NumberDisplayedInGivenField;
        }

        private bool IsThereBombInTheField(int i, int j) => 
            IsThereFieldWithSuchIndex(i, j) && 
            ActualBoardContent[i, j] == MinesweeperGame.BombSign;
    }
}
