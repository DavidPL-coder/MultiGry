using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MultiGry.Tests
{
    [TestFixture]
    class GetterColorForCharacterTests
    {
        [TestCase(Minesweeper.MinesweeperGame.BombSign, ConsoleColor.DarkRed)]
        [TestCase(Minesweeper.MinesweeperGame.FlagSign, ConsoleColor.Yellow)]
        [TestCase('1', ConsoleColor.Blue)]
        [TestCase('2', ConsoleColor.Green)]
        [TestCase('3', ConsoleColor.Red)]
        [TestCase('4', ConsoleColor.DarkCyan)]
        [TestCase('5', ConsoleColor.Magenta)]
        [TestCase('6', ConsoleColor.Magenta)]
        [TestCase('7', ConsoleColor.Magenta)]
        [TestCase('8', ConsoleColor.Magenta)]
        [TestCase(Minesweeper.MinesweeperGame.SquareSign, ConsoleColor.White)]
        [TestCase(Minesweeper.MinesweeperGame.EmptyFieldSign, ConsoleColor.White)]
        public void GetColorForCharacter_ProvideTwoDimensionalArrayWithOneElementThatHasAppropriateValue_ReturnsAppropriateValue
        (char ValueOfElement, ConsoleColor ExpectedColor)
        {
            var Board = new char[1, 1];
            Board[0, 0] = ValueOfElement;
            var GetterColor = new Minesweeper.GetterColorForCharacter(Board);

            var ActualColor = GetterColor.GetColorForCharacter(0, 0);

            Assert.AreEqual(ExpectedColor, ActualColor);
        }
    }
}
