using System;

namespace MultiGry.GuessingPIN
{
    class GuessingPIN_Game : IMenuOption
    {
        public string NameOption => "Zgadywanie PINu";
        private const int MaximumNumberOfAttempts = 7;
        private GameDuration Timer;
        private int[] RandomPINnumbers;
        private PerformerRoundOfGame PerformerRounds;

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            Timer.Start();
            PlayingGame();
            Timer.Stop();
            DisplayResults();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            Timer = new GameDuration();

            RandomPINnumbers = new int[4];
            var random = new Random();
            for (int i = 0; i < RandomPINnumbers.Length; ++i)
                RandomPINnumbers[i] = random.Next(0, 10);
        }

        private void PlayingGame()
        {
            PerformerRounds = new PerformerRoundOfGame(RandomPINnumbers);
            do
            {
                PerformerRounds.RoundProcessing();
                Console.Clear();
            }
            while (IsNotGameOver());
        }

        private bool IsNotGameOver() => 
            PerformerRounds.UserAttempt != MaximumNumberOfAttempts && 
            PerformerRounds.AreAllDigitsGuessed() == false;

        private void DisplayResults()
        {
            if (PerformerRounds.AreAllDigitsGuessed() == false)
                Console.WriteLine("Nie zgadłeś PINu! Twój czas: " + 
                                  Timer.GetTimeInTextVersion());

            else
                Console.WriteLine("Zgadłeś PIN w ciągu " + Timer.GetTimeInTextVersion() + 
                                  " oraz w próbie " + PerformerRounds.UserAttempt);

            DisplayPIN();
        }

        private void DisplayPIN()
        {
            Console.Write("PIN: ");
            foreach (var item in RandomPINnumbers)
                Console.Write(item);

            Console.ReadKey();
        }
    }
}
