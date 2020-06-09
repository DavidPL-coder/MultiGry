using System;
using NUnit.Framework;
using Moq;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class DecisionOnFurtherCourseOfProgramTests
    {
        [Test]
        public void UserDecidesWhatToDoNext_MethodCall_DisplaysAppropriateMessage()
        {
            // simulation of pressing the 2 key. This is needed to perform the test
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey2 = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey2);

            var Output = new StringWriter();
            Console.SetOut(Output);
            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(null, MockOfConsole.Object, null);
            string ExpectedMessage = "Co dalej chcesz robić?" + Environment.NewLine +
                                     "1. Zagrać jeszcze raz" + Environment.NewLine +
                                     "2. Powrócić do Menu" + Environment.NewLine +
                                     "3. Wyjść z programu" + Environment.NewLine;

            ProgramExecution.UserDecidesWhatToDoNext();

            Assert.AreEqual(ExpectedMessage, Output.ToString());
        }

        [Test]
        public void UserDecidesWhatToDoNext_SimulationOfPressingKey1_ReturnsExpectedValue
        ([Values] OptionsCategory Expected)
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey1 = new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey1);

            var MockOfOption = new Mock<IMenuOption>();
            MockOfOption.Setup(m => m.OptionExecuting()).Returns(Expected);

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(MockOfOption.Object, MockOfConsole.Object, null);

            OptionsCategory Result = ProgramExecution.UserDecidesWhatToDoNext();

            Assert.AreEqual(Expected, Result);
        }

        [Test]
        public void UserDecidesWhatToDoNext_SimulationOfPressingKey2_ReturnsOptionsCategoryNormalOption()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey2 = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey2);
            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(null, MockOfConsole.Object, null);

            OptionsCategory Result = ProgramExecution.UserDecidesWhatToDoNext();

            Assert.AreEqual(OptionsCategory.NormalOption, Result);
        }

        [TestCase(OptionsCategory.ExitTheProgram)]
        [TestCase(OptionsCategory.CanceledExit)]
        public void UserDecidesWhatToDoNext_SimulationOfPressingKey3_ReturnsExpectedValue
        (OptionsCategory Expected)
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey3 = new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey3);

            var MockOfExit = new Mock<IMenuOption>();
            MockOfExit.Setup(m => m.OptionExecuting()).Returns(Expected);

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(null, MockOfConsole.Object, MockOfExit.Object);

            OptionsCategory Result = ProgramExecution.UserDecidesWhatToDoNext();

            Assert.AreEqual(Expected, Result);
        }

        [Test]
        public void UserDecidesWhatToDoNext_SimulationOfPressingThreeKeys_IsCalledThreeTimes()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForWrongKey = new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false);
            var ConsoleKeyInfoForCorrectKey = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);

            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoForWrongKey)
                .Returns(ConsoleKeyInfoForWrongKey)
                .Returns(ConsoleKeyInfoForCorrectKey);

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(null, MockOfConsole.Object, null);

            ProgramExecution.UserDecidesWhatToDoNext();

            // if the UserDecidesWhatToDoNext method is executed 3 times then 
            // the ReadKey method is also executed 3 times:
            MockOfConsole.Verify(m => m.ReadKey(), Times.Exactly(3));
        }
    }
}
