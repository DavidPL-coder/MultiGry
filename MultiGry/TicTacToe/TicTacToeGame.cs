using System;

namespace MultiGry.TicTacToe
{
    class TicTacToeGame : IMenuOption
    {
        public string NameOption => "Kółko i krzyżyk dla dwóch osób";
        public const char SharpSign = 'x';
        public const char CircleSign = 'o';
        private GameDuration Timer;
        private PerformerRounds PerformerRounds;

        public OptionsCategory OptionExecuting()
        {
            Timer = new GameDuration();
            Timer.Start();
            PlayingGame();
            Timer.Stop();
            DisplayResult();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void PlayingGame()
        {
            PerformerRounds = new PerformerRounds();
            PerformerRounds.DrawPlayer();

            while (PerformerRounds.IsGameStillGoingOn())                                                              
                PerformerRounds.PlayingOneTurn();              
        }

        private void DisplayResult()
        {
            if (PerformerRounds.Winner == PlayerType.Circle)
                Console.WriteLine("Wygrało kółko!");

            else if (PerformerRounds.Winner == PlayerType.Sharp)
                Console.WriteLine("Wygrał krzyżyk!");

            else
                Console.WriteLine("Remis!");

            Console.WriteLine("Czas gry: " + Timer.GetTimeInTextVersion());
            DisplayBoard();
            Console.ReadKey();
        }

        public void DisplayBoard()
        {
            var InterfaceDisplay = new GameInterfaceDisplay(PerformerRounds);
            InterfaceDisplay.DisplayBoard();
        }
    }
}
