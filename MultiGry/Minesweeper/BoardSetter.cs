namespace MultiGry.Minesweeper
{
    class BoardSetter
    {
        public char[,] DisplayedBoard { private set; get; }
        public char[,] ActualBoardContent { private set; get; }
        private readonly int VerticalDimension;
        private readonly int HorizontalDimension;

        public BoardSetter()
        {
            VerticalDimension = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimension = MinesweeperGame.HorizontalDimensionOfBoard;
        }

        public void CreateBoard()
        {
            DisplayedBoard = new char[VerticalDimension, HorizontalDimension];
            ActualBoardContent = new char[VerticalDimension, HorizontalDimension];

            for (int i = 0; i < VerticalDimension; ++i)
                for (int j = 0; j < HorizontalDimension; ++j)
                {
                    DisplayedBoard[i, j] = MinesweeperGame.SquareSign;
                    ActualBoardContent[i, j] = MinesweeperGame.EmptyFieldSign;
                }
        }
    }
}
