using System;
using NUnit.Framework;
using Moq;

namespace MultiGry.Tests
{
    [TestFixture]
    class MinesSetterTests
    {
        [Test]
        public void SetMinesOnBoard_DrawCoordinatesAndInsertThemInAppropriateFieldsOnBoard_ReplenishesBoardWithBombs()
        {
            var IndexesOfField = Tuple.Create(5, 5);
            var MockOfNumberGenerator = new Mock<INumberGenerator>();

            // each subsequent pair of numbers is the coordinates of the field where the bomb will be.
            // some coordinates were intentionally repeated to check the operation of the tested method.
            MockOfNumberGenerator.SetupSequence(m => m.Next(It.IsAny<int>(), It.IsAny<int>()))
                                 .Returns(0)  // for Vertical
                                 .Returns(0)  // for Horizontal
                                 .Returns(0)  // vertical (repeated)
                                 .Returns(0)  // horizontal (repeated)
                                 .Returns(5)  // vertical (the same like one coordiante of IndexesOfField)
                                 .Returns(5)  // horizontal (the same like one coordiante of IndexesOfField)
                                 .Returns(1)  
                                 .Returns(1)  
                                 .Returns(2)  
                                 .Returns(2); 

            var Vertical = Minesweeper.MinesweeperGame.VerticalDimensionOfBoard;
            var Horizontal = Minesweeper.MinesweeperGame.HorizontalDimensionOfBoard;
            var ActualBoardContent = new char[Vertical, Horizontal];
            var DB = new char[Vertical, Horizontal];
            Minesweeper.BoardSetter.CreateBoard(DB, ActualBoardContent);

            var Setter = new Minesweeper.MinesSetter(IndexesOfField, MockOfNumberGenerator.Object);

            Setter.SetMinesOnBoard(ActualBoardContent, 3);

            Assert.AreEqual(Minesweeper.MinesweeperGame.BombSign, ActualBoardContent[0, 0]);
            Assert.AreEqual(Minesweeper.MinesweeperGame.BombSign, ActualBoardContent[1, 1]);
            Assert.AreEqual(Minesweeper.MinesweeperGame.BombSign, ActualBoardContent[2, 2]);
            Assert.AreNotEqual(Minesweeper.MinesweeperGame.BombSign, ActualBoardContent[5, 5]);
        }
    }
}
