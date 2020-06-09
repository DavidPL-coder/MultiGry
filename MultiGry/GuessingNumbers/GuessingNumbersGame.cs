using System;

namespace MultiGry.GuessingNumbers
{
    public class GuessingNumbersGame : IMenuOption
    {
        public string NameOption => "Zgadywanie liczb";  
        private INumberGenerator NumberGenerator;
        private IPerformerGame PerformerGame;
        private IResultDisplay ResultDisplay;
        private IDecisionOnFurtherCourseOfProgram ProgramExecution;

        public GuessingNumbersGame()
        {
            NumberGenerator = new NumberGenerator();
            PerformerGame = new PerformerGame();
            ResultDisplay = new ResultDisplay(new FakeConsole());
            ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
        }

        public GuessingNumbersGame(INumberGenerator NumberGenerator, 
                                   IPerformerGame PerformerGame,
                                   IResultDisplay ResultDisplay,
                                   IDecisionOnFurtherCourseOfProgram ProgramExecution)
        {
            this.NumberGenerator = NumberGenerator;
            this.PerformerGame = PerformerGame;
            this.ResultDisplay = ResultDisplay;
            this.ProgramExecution = ProgramExecution;
        }

        /// <exception cref = "InvalidOperationException">
        /// if the number drawn is out of range from 1 to 100
        /// </exception>
        public OptionsCategory OptionExecuting()
        {
            //SetDefaults();

            PerformerGame.NumberToGuess = NumberGenerator.GetNumberBetween1And100();

            int UserAttempt = PerformerGame.GameProcessing();
            ResultDisplay.DisplayOnlyResult(UserAttempt);

            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        //private void SetDefaults()
        //{
        //    UserAttempt = 1;

        //    var random = new Random(); 
        //    NumberToGuess = (byte) random.Next(1, 101);

        //    GetterProposal = new GetterProposalFromUser();
        //}

        //private void UserAttemptsToGuessNumber()
        //{
        //    Console.WriteLine("Wybierz liczbę z przedziału od 1 do 100:");

        //    do
        //    {
        //        Console.Write("(Próba " + UserAttempt + ") Podaj liczbę: ");
        //        UsersProposal = GetterProposal.GetProposalFromUser();
        //        ProcessingUserProposals();
        //    }
        //    while (UsersProposal != NumberToGuess);
        //}

        //private void ProcessingUserProposals()
        //{
        //    if (UsersProposal != NumberToGuess)
        //    {
        //        string Message = UsersProposal > NumberToGuess ? "Za dużo!" 
        //                                                       : "Za mało!";
        //        Console.WriteLine(Message);
        //        System.Threading.Thread.Sleep(1500);

        //        ++UserAttempt;
        //    }
        //}

        //private void DisplayOnlyResult()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Odgadłeś tę liczbę w próbie " + UserAttempt);
        //    Console.ReadKey();
        //}
    }
}
