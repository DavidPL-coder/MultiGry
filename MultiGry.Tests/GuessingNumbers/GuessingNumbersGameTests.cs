using System;
using NUnit.Framework;
using Moq;
using System.IO;

namespace MultiGry.Tests
{
    [TestFixture]
    class GuessingNumbersGameTests
    {
        [TestCase(0)]
        [TestCase(101)]
        public void OptionExecuting_SimulationsOfDrawingNumberOutsideTheRangeOf1To100_ThrowsException
        (byte NumberDrawn)
        {
            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            MockOfNumberGenerator.Setup(m => m.GetNumberBetween1And100()).Returns(NumberDrawn);
            var PerformerGame = new GuessingNumbers.PerformerGame();

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               PerformerGame, null, null);

            Assert.Throws<InvalidOperationException>(() => Game.OptionExecuting());
        }

        [TestCase(1)]
        [TestCase(100)]
        public void OptionExecuting_SimulationsOfDrawingNumberFrom1To100_DoesNotThrowException
        (byte NumberDrawn)
        {
            // draw simulation:
            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            MockOfNumberGenerator.Setup(m => m.GetNumberBetween1And100()).Returns(NumberDrawn);

            // simulation of choosing the right number. This is for the method to end. 
            // We're just testing if an exception is thrown, so don't pay attention to it
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns(NumberDrawn.ToString());
            var GetterProposal = new GuessingNumbers.GetterProposalFromUser(MockOfConsole.Object);
            var PerformerGame = new GuessingNumbers.PerformerGame(GetterProposal);

            // things about the end of the method / game (do not pay attention to it):
            var MockOfResultDisplay = new Mock<GuessingNumbers.IResultDisplay>();
            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(OptionsCategory.NormalOption);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               PerformerGame, 
                                                               MockOfResultDisplay.Object, 
                                                               MockOfProgramExecution.Object);

            Assert.DoesNotThrow(() => Game.OptionExecuting());
        }

        [Test]
        public void OptionExecuting_GamePlaySimulation_DisplaysRightContentInConsole()
        {
            byte NumberDrawn = 34;
            byte NumberGreaterThanDrawnAtRandom = 47;
            byte NumberLessThanRandom = 23;
            byte NumberEqualToDrawnNumber = NumberDrawn;

            // draw simulation:
            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            MockOfNumberGenerator.Setup(m => m.GetNumberBetween1And100()).Returns(NumberDrawn);

            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.SetupSequence(m => m.ReadLine())
                .Returns(NumberGreaterThanDrawnAtRandom.ToString())
                .Returns(NumberLessThanRandom.ToString())
                .Returns(NumberEqualToDrawnNumber.ToString());

            var GetterProposal = new GuessingNumbers.GetterProposalFromUser(MockOfConsole.Object);
            var PerformerGame = new GuessingNumbers.PerformerGame(GetterProposal);

            // remember that the given numbers will also be displayed in the console, 
            // but they will not be visible in the console buffer 
            // (the buffer apparently stores only what was displayed via Write(Line)):
            var Output = new StringWriter();
            Console.SetOut(Output);
            string ExpectedOutput = "Wybierz liczbę z przedziału od 1 do 100:" + Environment.NewLine +
                                    "(Próba 1) Podaj liczbę: " +
                                    "Za dużo!" + Environment.NewLine +
                                    "(Próba 2) Podaj liczbę: " +
                                    "Za mało!" + Environment.NewLine +
                                    "(Próba 3) Podaj liczbę: ";

            // things about the end of the method / game (do not pay attention to it):
            var MockOfResultDisplay = new Mock<GuessingNumbers.IResultDisplay>();
            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(OptionsCategory.NormalOption);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               PerformerGame,
                                                               MockOfResultDisplay.Object,
                                                               MockOfProgramExecution.Object);
            Game.OptionExecuting();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OptionExecuting_SimulationOfGameInWhichUserProvidesIncorrectValues_DisplaysRightContentInConsole()
        {
            byte NumberDrawn = 45;
            string NumberGreaterThan100 = "123";
            string NumberLessThan1 = "0";
            string NotNumber = "text";
            byte NumberEqualToDrawnNumber = NumberDrawn;

            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            MockOfNumberGenerator.Setup(m => m.GetNumberBetween1And100()).Returns(NumberDrawn);

            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.SetupSequence(m => m.ReadLine())
                .Returns(NumberGreaterThan100)
                .Returns(NumberLessThan1)
                .Returns(NotNumber)
                .Returns(NumberEqualToDrawnNumber.ToString());

            var GetterProposal = new GuessingNumbers.GetterProposalFromUser(MockOfConsole.Object);
            var PerformerGame = new GuessingNumbers.PerformerGame(GetterProposal);

            var Output = new StringWriter();
            Console.SetOut(Output);
            string ExpectedOutput = "Wybierz liczbę z przedziału od 1 do 100:" + Environment.NewLine +
                                    "(Próba 1) Podaj liczbę: " +
                                    "Podana liczba jest poza dozwolonym przedziałem (zakres wynosi 1 - 100)!" + 
                                    Environment.NewLine +
                                    "(Próba 1) Podaj liczbę: " +
                                    "Podana liczba jest poza dozwolonym przedziałem (zakres wynosi 1 - 100)!" + 
                                    Environment.NewLine +
                                    "(Próba 1) Podaj liczbę: " +
                                    "Nieprawidłowa wartość!" + Environment.NewLine +
                                    "(Próba 1) Podaj liczbę: ";

            var MockOfResultDisplay = new Mock<GuessingNumbers.IResultDisplay>();

            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(OptionsCategory.NormalOption);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               PerformerGame,
                                                               MockOfResultDisplay.Object,
                                                               MockOfProgramExecution.Object);
            Game.OptionExecuting();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OptionExecuting_GamePlaySimulation_EndsItsOperationsWhenDrawnNumberIsGuessed()
        {
            byte NumberDrawn = 68;
            byte NumberOtherThanRandomNumber = 47;
            byte NumberEqualToDrawnNumber = NumberDrawn;

            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            MockOfNumberGenerator.Setup(m => m.GetNumberBetween1And100()).Returns(NumberDrawn);

            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.SetupSequence(m => m.ReadLine())
                .Returns(NumberOtherThanRandomNumber.ToString())
                .Returns(NumberEqualToDrawnNumber.ToString())
                .Returns(NumberOtherThanRandomNumber.ToString())
                .Returns(NumberOtherThanRandomNumber.ToString());

            var GetterProposal = new GuessingNumbers.GetterProposalFromUser(MockOfConsole.Object);
            var PerformerGame = new GuessingNumbers.PerformerGame(GetterProposal);

            var MockOfResultDisplay = new Mock<GuessingNumbers.IResultDisplay>();
            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(OptionsCategory.NormalOption);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               PerformerGame,
                                                               MockOfResultDisplay.Object,
                                                               MockOfProgramExecution.Object);
            Game.OptionExecuting();

            // if ReadLine is called only twice, it means 
            // that the tested method ends when the number is guessed:
            MockOfConsole.Verify(m => m.ReadLine(), Times.Exactly(2));
        }

        [Test]
        public void OptionExecuting_GamePlaySimulation_DisplaysResultCorrectly
        ([Random(3, 10, 1)] int UserAttempts)
        {
            var MockOfNumberGenerator = new Mock<INumberGenerator>();

            var MockOfPerformerGame = new Mock<GuessingNumbers.IPerformerGame>();
            MockOfPerformerGame.Setup(m => m.GameProcessing()).Returns(UserAttempts);

            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(OptionsCategory.NormalOption);

            var MockOfConsole = new Mock<IFakeConsole>();

            var Output = new StringWriter();
            Console.SetOut(Output);
            string ExpectedOutput = "Odgadłeś tę liczbę w próbie " + UserAttempts + Environment.NewLine;

            var ResultDisplay = new GuessingNumbers.ResultDisplay(MockOfConsole.Object);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               MockOfPerformerGame.Object,
                                                               ResultDisplay,
                                                               MockOfProgramExecution.Object);

            Game.OptionExecuting();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OptionExecuting_MethodCalls_ReturnsSameValueAsUserDecidesWhatToDoNextMethod
        ([Values] OptionsCategory ReturnValue)
        {
            var MockOfNumberGenerator = new Mock<INumberGenerator>();
            var MockOfPerformerGame = new Mock<GuessingNumbers.IPerformerGame>();
            var MockOfResultDisplay = new Mock<GuessingNumbers.IResultDisplay>();

            var MockOfProgramExecution = new Mock<IDecisionOnFurtherCourseOfProgram>();
            MockOfProgramExecution.Setup(m => m.UserDecidesWhatToDoNext()).Returns(ReturnValue);

            var Game = new GuessingNumbers.GuessingNumbersGame(MockOfNumberGenerator.Object,
                                                               MockOfPerformerGame.Object,
                                                               MockOfResultDisplay.Object,
                                                               MockOfProgramExecution.Object);
            var Result = Game.OptionExecuting();

            Assert.AreEqual(ReturnValue, Result);
        }
    }
}
