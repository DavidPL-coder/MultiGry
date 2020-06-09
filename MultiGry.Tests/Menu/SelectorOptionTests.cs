using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class SelectorOptionTests
    {
        [Test]
        public void SelectingOption_BasedOnGivenKeyNumberCorrespondingKeyIsSimulated_CallsAppropriateMethodFromAppropriateOptionListItem
        ([Range(1, 6)] int NumberKey)
        {
            // simulation of pressing the appropriate key:
            var MockOfConsole = new Mock<IFakeConsole>();
            char KeyChar = Convert.ToChar(NumberKey);
            ConsoleKey Key = ConsoleKey.D0 + NumberKey;
            var ConsoleKeyInfoForKey = new ConsoleKeyInfo(KeyChar, Key, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey);

            var ListOfMockOfOptions = new List<Mock<IMenuOption>>
            {
                new Mock<IMenuOption>(),
                new Mock<IMenuOption>(),
                new Mock<IMenuOption>(),
                new Mock<IMenuOption>(),
                new Mock<IMenuOption>(),
                new Mock<IMenuOption>()
            };
            var ListOfOptions = new List<IMenuOption>
            {
                ListOfMockOfOptions[0].Object,
                ListOfMockOfOptions[1].Object,
                ListOfMockOfOptions[2].Object,
                ListOfMockOfOptions[3].Object,
                ListOfMockOfOptions[4].Object,
                ListOfMockOfOptions[5].Object
            };
            ListOfMockOfOptions.ForEach(m => 
                                        m.Setup(m1 => m1.OptionExecuting())
                                        .Returns(OptionsCategory.NormalOption));

            var SelectorOption = new Menu.SelectorOption(ListOfOptions, MockOfConsole.Object);

            SelectorOption.SelectingOption();

            // after pressing the key with the number x the x-mock OptionExecuting method will be called:
            ListOfMockOfOptions[NumberKey - 1].Verify(m => m.OptionExecuting(), Times.Once());
        }

        [Test]
        public void SelectingOption_SimulationOfPressingWrongKey_ReturnsOptionsCategoryWrong()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForWrongKey = new ConsoleKeyInfo('0', ConsoleKey.D0, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForWrongKey);

            var ListOfOptions = new List<IMenuOption>();
            var SelectorOption = new Menu.SelectorOption(ListOfOptions, MockOfConsole.Object);

            OptionsCategory Result = SelectorOption.SelectingOption();

            Assert.AreEqual(OptionsCategory.Wrong, Result);
        }

        [Test]
        public void SelectingOption_SimulationOfPressingCorrectKey_ReturnsSameValueAsOptionExecutingMethod
        ([Values] OptionsCategory ExpectedValue)
        {
            var MockOfOption = new Mock<IMenuOption>();
            MockOfOption.Setup(m => m.OptionExecuting()).Returns(ExpectedValue);

            var ListOfOptions = new List<IMenuOption>
            {
                MockOfOption.Object
            };

            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoForKey = new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoForKey);

            var SelectorOption = new Menu.SelectorOption(ListOfOptions, MockOfConsole.Object);

            var Result = SelectorOption.SelectingOption();

            Assert.AreEqual(ExpectedValue, Result);
        }
    }
}
