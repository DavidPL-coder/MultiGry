using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;

namespace MultiGry.Tests
{
    [TestFixture]
    public class MainMenuTests
    {
        [Test]
        public void ExecutingMenuOperation_ProvidingConstructorWithListWithFewerThan1Items_ThrowsException()
        {
            var ListOfOptions = new List<IMenuOption>();
            var Menu = new Menu.MainMenu(ListOfOptions);
            
            Assert.Throws<InvalidOperationException>(Menu.ExecutingMenuOperation);
        }

        [Test]
        public void ExecutingMenuOperation_ProvidingConstructorWithListWithMoreThan9Items_ThrowsException()
        {
            var ListOfOptions = new List<IMenuOption>
            {
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new BinaryClock.BinaryClockOption(),
                new Exit.ExitOption()
            };
            var Menu = new Menu.MainMenu(ListOfOptions);

            Assert.Throws<InvalidOperationException>(Menu.ExecutingMenuOperation);
        }

        [Test]
        public void ExecutingMenuOperation_PassingListOfOptionsInWhichLastOptionIsNotOptionToExitToConstructor_ThrowsException()
        {
            var ListOfOptions = new List<IMenuOption>
            {
                new Exit.ExitOption(),
                new BinaryClock.BinaryClockOption()
            };
            var Menu = new Menu.MainMenu(ListOfOptions);

            Assert.Throws<InvalidOperationException>(Menu.ExecutingMenuOperation);
        }

        [Test]
        public void ExecutingMenuOperation_MethodCall_CallsDisplayingMenuMethodOnce()
        {
            var MockOfMenuDisplay = new Mock<Menu.IMenuDisplay>();
            var MockOfConsole = new Mock<IFakeConsole>();
            var MockOfSelectorOption = new Mock<Menu.ISelectorOption>();
            // returning OptionsCategory.ExitTheProgram via SelectingOption 
            // will cause the internal loop (in ExecutingMenuOperation / EventLoop) to be executed only once:
            MockOfSelectorOption.Setup(m => m.SelectingOption()).Returns(OptionsCategory.ExitTheProgram);

            var ListOfOptions = new List<IMenuOption>
            {
                new Exit.ExitOption(),
            };
            var Menu = new Menu.MainMenu(ListOfOptions, MockOfMenuDisplay.Object, 
                                         MockOfConsole.Object, MockOfSelectorOption.Object);

            Menu.ExecutingMenuOperation();

            MockOfMenuDisplay.Verify(m => m.DisplayingMenu(), Times.Once());
        }

        [Test]
        public void ExecutingMenuOperation_MethodCall_CallsSelectingOptionMethodOnce()
        {
            var MockOfMenuDisplay = new Mock<Menu.IMenuDisplay>();
            var MockOfConsole = new Mock<IFakeConsole>();
            var MockOfSelectorOption = new Mock<Menu.ISelectorOption>();
            MockOfSelectorOption.Setup(m => m.SelectingOption()).Returns(OptionsCategory.ExitTheProgram);

            var ListOfOptions = new List<IMenuOption>
            {
                new Exit.ExitOption(),
            };
            var Menu = new Menu.MainMenu(ListOfOptions, MockOfMenuDisplay.Object,
                                         MockOfConsole.Object, MockOfSelectorOption.Object);

            Menu.ExecutingMenuOperation();

            MockOfSelectorOption.Verify(m => m.SelectingOption(), Times.Once());
        }

        [Test]
        public void ExecutingMenuOperation_CallingMethodWithDifferentOptionCategories_FinishesItsOperationsForExitTheProgramCategory()
        {
            var MockOfMenuDisplay = new Mock<Menu.IMenuDisplay>();
            var MockOfConsole = new Mock<IFakeConsole>();
            var MockOfSelectorOption = new Mock<Menu.ISelectorOption>();
            MockOfSelectorOption.SetupSequence(m => m.SelectingOption())
                .Returns(OptionsCategory.NotSelectedYet)
                .Returns(OptionsCategory.CanceledExit)
                .Returns(OptionsCategory.NormalOption)
                .Returns(OptionsCategory.Wrong)
                .Returns(OptionsCategory.ExitTheProgram)
                .Returns(OptionsCategory.NormalOption);

            var ListOfOptions = new List<IMenuOption>
            {
                new Exit.ExitOption(),
            };
            var Menu = new Menu.MainMenu(ListOfOptions, MockOfMenuDisplay.Object,
                                         MockOfConsole.Object, MockOfSelectorOption.Object);

            Menu.ExecutingMenuOperation();

            // return values ​​for 6 calls were specified for the SelectingOption method. 
            // Returns ExitFromProgram for the 5th call. If the Tested method terminates 
            // its operation for the 5th call, it means that the method works correctly:
            MockOfSelectorOption.Verify(m => m.SelectingOption(), Times.Exactly(5));
        }
    }
}