using System;

namespace MultiGry.PaperRockScissors
{
    class PerformerRounds
    {
        private byte Rounds;
        private int CurrentRound;
        public HandShapes OptionSelectedByUser { private set; get; }
        public HandShapes OptionDrawnByComputer { private set; get; }
        private int RoundResult;
        public int Draws { private set; get; }
        public int UserPoints { private set; get; }
        public int ComputerPoints { private set; get; }

        public PerformerRounds(byte Rounds)
        {
            this.Rounds = Rounds;
            Draws = 0;
            UserPoints = 0; 
            ComputerPoints = 0;
        }

        public void PlayingRounds()
        {
            for (CurrentRound = 1; CurrentRound <= Rounds; ++CurrentRound)
            {
                DisplayingHandShapeSelection();
                GetSelectingOption();
                PlayerSelectionProcessing();
            }
        }

        private void DisplayingHandShapeSelection()
        {
            Console.Clear();
            Console.WriteLine("Wybierz:");
            Console.WriteLine("1. Papier");
            Console.WriteLine("2. Kamień");
            Console.WriteLine("3. Nożyce");
        }

        private void GetSelectingOption() =>
            OptionSelectedByUser = (HandShapes) (Console.ReadKey(true).Key - 
                                                 ConsoleKey.D0);

        private void PlayerSelectionProcessing()
        {
            if (CheckSelectedRightOption())
            {
                ComputerSelectionDraw();
                DisplayResultOfRound();
                AwardOfPoints();
            }

            else
            {
                DisplayMessageAboutWrongSelection();               
                --CurrentRound; // by reducing "CurrentRound" we enable the user 
                                // to re-select options within this round
            }

            System.Threading.Thread.Sleep(1500);
        }

        private bool CheckSelectedRightOption() =>
            (int) OptionSelectedByUser >= 1 && (int) OptionSelectedByUser <= 3;

        private void ComputerSelectionDraw()
        {
            var random = new Random();
            OptionDrawnByComputer = (HandShapes) random.Next(1, 4);
        }

        private void DisplayResultOfRound()
        {
            var RoundResultDisplay = new RoundResultDisplay(this);
            RoundResult = RoundResultDisplay.DisplayResult();
        }

        private void AwardOfPoints()
        {
            switch (RoundResult)
            {
                case -1: ++ComputerPoints; break;
                case 0: ++Draws; break;
                case 1: ++UserPoints; break;
            }
        }

        private void DisplayMessageAboutWrongSelection() => 
            Console.WriteLine("Niewłaściwa opcja! Możesz wybrać tylko" +
                              " Papier(1), Kamień(2), Nożyce(3).");
    }
}
