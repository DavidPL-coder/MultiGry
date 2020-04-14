using System;

namespace MultiGry.PaperRockScissors
{
    class PaperRockScissorsGame : IMenuOption
    {
        public string NameOption => "Papier, kamień, nożyce";
        private PerformerRounds PerformerRounds;

        public OptionsCategory OptionExecuting()
        {
            PlayingGame();
            DisplayOnlyResultsOfGame();    

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void PlayingGame()
        {
            byte Rounds = GetNumberOfRoundsFromUser();
            PerformerRounds = new PerformerRounds(Rounds);
            PerformerRounds.PlayingRounds();
        }

        private byte GetNumberOfRoundsFromUser() 
        {
            var Getter = new GetterNumberOfRounds();
            return Getter.GetNumberOfRoundsFromUser();
        }

        private void DisplayOnlyResultsOfGame()
        {
            Console.Clear();
            Console.WriteLine("Wyniki:");
            Console.WriteLine("Twoje punkty: " + PerformerRounds.UserPoints);
            Console.WriteLine("Punkty bota: " + PerformerRounds.ComputerPoints);
            Console.WriteLine("Remisy: " + PerformerRounds.Draws);
            Console.ReadKey();
        }
    }
}
