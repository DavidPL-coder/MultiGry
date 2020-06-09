namespace MultiGry.Minesweeper
{
    public static class BoardSetter
    {
        /// both arrays retrieved by the method must be of equal length.
        /// First dimension must equal value of MinesweeperGame.VerticalDimensionOfBoard.
        /// Second dimension must equal value of MinesweeperGame.HorizontalDimensionOfBoard
        public static void CreateBoard(char[,] displayedBoard, char[,] actualBoardContent)
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                {
                    displayedBoard[i, j] = MinesweeperGame.SquareSign;
                    actualBoardContent[i, j] = MinesweeperGame.EmptyFieldSign;
                }
        }
    }
}
