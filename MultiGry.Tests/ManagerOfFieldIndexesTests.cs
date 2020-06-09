using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class ManagerOfFieldIndexesTests
    {
        [TestCase("11")]
        [TestCase("1 3")]
        [TestCase("\t5 3")]
        [TestCase("\t4 \t 4\t")]
        [TestCase("8 8")]
        public void CheckIndexesInTextVersion_EnterCordinatesOfFieldInTheCorrectFormatAsInput_ReturnsTrue
        (string CordinatesOfField)
        {
            var Manager = new Minesweeper.ManagerOfFieldIndexes();
            var Input = new StringReader(CordinatesOfField);
            Console.SetIn(Input);
            Manager.UserInputOfFieldIndexesInTextVersion();

            var Result = Manager.CheckIndexesInTextVersion();

            Assert.IsTrue(Result);
        }

        [TestCase("00")]
        [TestCase("-1 -3")]
        [TestCase("5 3 1")]
        [TestCase("4")]
        [TestCase("9 8")]
        [TestCase(". a")]
        public void CheckIndexesInTextVersion_EnterCordinatesOfFieldInWrongFormatAsInput_ReturnsFalse
        (string CordinatesOfField)
        {
            var Manager = new Minesweeper.ManagerOfFieldIndexes();
            var Input = new StringReader(CordinatesOfField);
            Console.SetIn(Input);
            Manager.UserInputOfFieldIndexesInTextVersion();

            var Result = Manager.CheckIndexesInTextVersion();

            Assert.IsFalse(Result);
        }

        [TestCase("11", 0, 0)]
        [TestCase("4 1", 3, 0)]
        [TestCase("\t5 \t 2 \t", 4, 1)]
        public void SetTupleOfIndexes_EnterCordinatesOfFieldInTheCorrectFormatAsInput_SetsTupleOfIndexes
        (string CordinatesOfField, int first, int second)
        {
            var Manager = new Minesweeper.ManagerOfFieldIndexes();
            var Input = new StringReader(CordinatesOfField);
            Console.SetIn(Input);
            Manager.UserInputOfFieldIndexesInTextVersion();

            Manager.SetTupleOfIndexes();

            Assert.AreEqual(Tuple.Create(first, second), Manager.TupleOfIndexes);
        }
    }
}
