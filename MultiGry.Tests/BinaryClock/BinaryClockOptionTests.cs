using NUnit.Framework;
using Moq;
using System;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class BinaryClockOptionTests
    {
        [Test]
        public void OptionExecuting_CallingMethodWithOnlyOneIterationOfItsInnerLoop_DisplaysCurrentTime()
        {
            // key click simulation after the second method call. Thanks to this, the inner loop is made only once:
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.SetupSequence(m => m.KeyAvailable())
                .Returns(false)
                .Returns(true);

            var Output = new StringWriter();
            Console.SetOut(Output);
            var BinaryHour = Convert.ToString(DateTime.Now.Hour, 2);
            var BinaryMinute = Convert.ToString(DateTime.Now.Minute, 2);
            var BinarySecond = Convert.ToString(DateTime.Now.Second, 2);
            string ExpectedOutput = "Godzina: " + BinaryHour + Environment.NewLine +
                                    "Minuty:" + BinaryMinute + Environment.NewLine +
                                    "Sekundy: " + BinarySecond + Environment.NewLine;

            var TimeDisplay = new BinaryClock.TimeDisplay();

            var BinaryClock = new BinaryClock.BinaryClockOption(MockOfConsole.Object, TimeDisplay);

            BinaryClock.OptionExecuting();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        // one second of time is a break in the operation of the method 
        // by performing System.Threading.Thread.Sleep, and the 
        // rest is time to perform the remaining actions
        [Timeout(1250)] 
        [Test]
        public void OptionExecuting_CallingMethodWithOnlyOneIterationOfItsInnerLoop_MethodDoesNotPerformItsActionForTooLong()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.SetupSequence(m => m.KeyAvailable())
                .Returns(false)
                .Returns(true);

            var BinaryClock = new BinaryClock.BinaryClockOption(MockOfConsole.Object,
                                                                new BinaryClock.TimeDisplay());

            BinaryClock.OptionExecuting();
        }

        [Test]
        public void OptionExecuting_CallingMethod_ReturnsOptionsCategoryNormalOption()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.KeyAvailable()).Returns(true);
            var BinaryClock = new BinaryClock.BinaryClockOption(MockOfConsole.Object, null);

            OptionsCategory Result = BinaryClock.OptionExecuting();

            Assert.AreEqual(OptionsCategory.NormalOption, Result);
        }

    }
}
