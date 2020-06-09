using NUnit.Framework;
using System;
using System.IO;
using Moq;
using MultiGry.Minesweeper;

namespace MultiGry.Tests
{
    [TestFixture]
    class BoardDisplayTests
    {
        [Test]
        public void DisplayContent_MethodCall_DisplaysRightBoard()
        {
            var Sqrt = MinesweeperGame.SquareSign;
            var DisplayedBoard = new char[,]
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

            var MockOfConsole = new Mock<IFakeConsole>();
            var MockOfGetterColor = new Mock<IGetterColorForCharacter>();
            var BoardDisplay = new BoardDisplay(DisplayedBoard, MockOfConsole.Object, MockOfGetterColor.Object);

            var Output = new StringWriter();
            Console.SetOut(Output);
            
            var ExpectedOutput = 
            $"  1 2 3 4 5 6 7 8 " + Environment.NewLine +
            $"  - - - - - - - - " + Environment.NewLine +                               
            $"1|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                                
            $"2|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                               
            $"3|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                             
            $"4|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                               
            $"5|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                                 
            $"6|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                                
            $"7|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine +                                 
            $"8|{Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} {Sqrt} " + Environment.NewLine;

            BoardDisplay.DisplayContent();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void DisplayContent_MethodCall_CallsGetColorForCharacterMethodRightNumberOfTimes()
        {
            var DisplayedBoard = new char[MinesweeperGame.VerticalDimensionOfBoard, MinesweeperGame.HorizontalDimensionOfBoard];
            var MockOfConsole = new Mock<IFakeConsole>();
            var MockOfGetterColor = new Mock<IGetterColorForCharacter>();
            var BoardDisplay = new BoardDisplay(DisplayedBoard, MockOfConsole.Object, MockOfGetterColor.Object);

            BoardDisplay.DisplayContent();

            int ExpectedNumberOfCalls = MinesweeperGame.VerticalDimensionOfBoard * MinesweeperGame.HorizontalDimensionOfBoard; // we call the method for each element
            MockOfGetterColor.Verify(m => m.GetColorForCharacter(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(ExpectedNumberOfCalls));
        }
    }
}
