using System;
using NUnit.Framework;
using Moq;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class ExitOptionTests
    {
        [Test]
        public void OptionExecuting_DisplayingQuestionRegardingExitFromProgram_DisplaysAppropriateMessage()
        {
            // Key press simulation:
            var FakeConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey = new ConsoleKeyInfo('0', ConsoleKey.D0, false, false, false);
            FakeConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey);

            var Option = new Exit.ExitOption(FakeConsole.Object);
            var Output = new StringWriter();
            Console.SetOut(Output);
            string ExpectedMessage = "Czy napewno chcesz wyjść z programu?" + Environment.NewLine +
                                     "(naciśnij enter aby wyjść, bądź inny klawisz aby anulować)" 
                                     + Environment.NewLine;

            Option.OptionExecuting();

            Assert.AreEqual(ExpectedMessage, Output.ToString());
        }

        [Test]
        public void OptionExecuting_SimulationOfPressingEnterKey_ReturnsOptionsCategoryExitTheProgram()
        {
            var FakeConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForEnter = new ConsoleKeyInfo((char) 13, ConsoleKey.Enter, false, false, false);
            FakeConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForEnter);
            var Option = new Exit.ExitOption(FakeConsole.Object);

            OptionsCategory Result = Option.OptionExecuting();

            Assert.AreEqual(OptionsCategory.ExitTheProgram, Result);
        }

        [Test]
        public void OptionExecuting_SimulationOfPressingKeyOtherThanEnter_ReturnsOptionsCategoryCanceledExit()
        {
            var FakeConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey = new ConsoleKeyInfo('0', ConsoleKey.D0, false, false, false);
            FakeConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey);
            var Option = new Exit.ExitOption(FakeConsole.Object);

            OptionsCategory Result = Option.OptionExecuting();

            Assert.AreEqual(OptionsCategory.CanceledExit, Result);
        }
    }
}
