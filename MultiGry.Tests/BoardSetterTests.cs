using NUnit.Framework;
using MultiGry.Minesweeper;

namespace MultiGry.Tests
{
    [TestFixture]
    class BoardSetterTests
    {
        [Test]
        public void CreateBoard_MethodCalls_CreatesDisplayedBoardThatIsFilledWithSquares()
        {
            var DisplayedBoard = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var ActualBoardContent = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var Sqrt = MinesweeperGame.SquareSign;
            var ExpectedDisplayedBoard = new char[,]
            {
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt },
                { Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt, Sqrt }
            };

            BoardSetter.CreateBoard(DisplayedBoard, ActualBoardContent);

            Assert.AreEqual(ExpectedDisplayedBoard, DisplayedBoard);
        }

        [Test]
        public void CreateBoard_MethodCalls_CreatesActualBoardContentThatIsFilledWithCapitalsO()
        {
            var DisplayedBoard = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var ActualBoardContent = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var Empty = MinesweeperGame.EmptyFieldSign;
            var ExpectedBoardContent = new char[,]
            {
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                { Empty, Empty, Empty, Empty, Empty, Empty, Empty, Empty }
            };

            BoardSetter.CreateBoard(DisplayedBoard, ActualBoardContent);

            Assert.AreEqual(ExpectedBoardContent, ActualBoardContent);
        }
    }
}
