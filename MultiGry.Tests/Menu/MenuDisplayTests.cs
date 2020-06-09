using NUnit.Framework;
using Moq;
using System.IO;
using System;
using System.Collections.Generic;

namespace MultiGry.Tests
{
    class MenuDisplayTests
    {
        [Test]
        public void DisplayingMenu_MethodCall_DisplaysAppropriateMessage()
        {
            var ListOfOptions = new List<IMenuOption>
            {
                new BinaryClock.BinaryClockOption(),
                new Exit.ExitOption(),            
            }; 
            string ExpectedMessage = "Wybierz jedną z poniższych gier/aplikacji " +
                                     "naciskają odpowiedni klawisz:" + Environment.NewLine +
                                     "1. " + ListOfOptions[0].NameOption + Environment.NewLine +
                                     "2. " + ListOfOptions[1].NameOption + Environment.NewLine;

            var Output = new StringWriter();
            Console.SetOut(Output);      
            var MenuDisplay = new Menu.MenuDisplay(ListOfOptions);

            MenuDisplay.DisplayingMenu();

            Assert.AreEqual(ExpectedMessage, Output.ToString());
        }
    }
}
