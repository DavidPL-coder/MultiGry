using NUnit.Framework;
using MultiGry.Minesweeper;

namespace MultiGry.Tests
{
    [TestFixture]
    class MinesCounterTests
    {
        [Test]
        public void LoadNumberOfMinesIntoDisplayedBoard_CheckIfFieldsHaveBeenExposedCorrectlyAndNumberLoaded()
        {
            var DisplayedBoard = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var ActualBoardContent = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            BoardSetter.CreateBoard(DisplayedBoard, ActualBoardContent);

            // for the test the square will have dimensions of 4x4:
            Rect SquareOfExposedFields;
            SquareOfExposedFields.Left = 5;
            SquareOfExposedFields.Top = 5;
            SquareOfExposedFields.Right = 8;
            SquareOfExposedFields.Bottom = 8;

            // this is what the board should look like:
            // O O O O O O O O
            // O O O O O O O O
            // O O O O O O O O
            // O O O O O O O O
            // O O O O B B O O
            // O O O O O 2 1 O
            // O O O O O 1 1 1
            // O O O O B 1 1 B
            // B is hidden bomb

            ActualBoardContent[4, 5] = MinesweeperGame.BombSign;
            ActualBoardContent[4, 4] = MinesweeperGame.BombSign;
            ActualBoardContent[7, 4] = MinesweeperGame.BombSign;
            ActualBoardContent[7, 7] = MinesweeperGame.BombSign;

            var Counter = new MinesCounter(DisplayedBoard, ActualBoardContent, SquareOfExposedFields);

            Counter.LoadNumberOfMinesIntoDisplayedBoard();

            Assert.AreEqual('2', DisplayedBoard[5, 5]);
            Assert.AreEqual('1', DisplayedBoard[5, 6]);
            Assert.AreEqual(MinesweeperGame.EmptyFieldSign, DisplayedBoard[5, 7]);
            Assert.AreEqual('1', DisplayedBoard[6, 5]);
            Assert.AreEqual('1', DisplayedBoard[6, 6]);
            Assert.AreEqual('1', DisplayedBoard[6, 7]);
            Assert.AreEqual('1', DisplayedBoard[7, 5]);
            Assert.AreEqual('1', DisplayedBoard[7, 6]);
            Assert.AreEqual(MinesweeperGame.SquareSign, DisplayedBoard[7, 7]);
        }
    }
}
