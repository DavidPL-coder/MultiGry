using System;

namespace MultiGry.PaperRockScissors
{
    class RoundResultDisplay
    {
        private HandShapes OptionUser;
        private HandShapes OptionComputer;

        public RoundResultDisplay(PerformerRounds PerformerRounds)
        {
            OptionUser = PerformerRounds.OptionSelectedByUser;
            OptionComputer = PerformerRounds.OptionDrawnByComputer;
        }

        /// <returns>
        /// if the method returns 0, it means there is a draw 
        /// If he returns 1, the player wins against the computer. 
        /// If he returns -1, the player loses
        /// </returns>
        public int DisplayResult()
        {
            int Result = ComparingBothHands();

            switch (Result)
            {
                case -1: DisplayFullResult("PRZEGRYWASZ"); break;
                case 0: Console.WriteLine("Remis!"); break;
                case 1: DisplayFullResult("WYGRYWASZ"); break;
            }

            return Result;
        }

        /// <summary>
        /// the method checks who won and returns information about it as int
        /// </summary>
        private int ComparingBothHands()
        {
            if (OptionUser == OptionComputer)
                return 0;

            else if (OptionUser == HandShapes.Scissor &&
                     OptionComputer == HandShapes.Paper)
                return 1;

            else if (OptionUser == HandShapes.Rock &&
                     OptionComputer == HandShapes.Scissor)
                return 1;

            else if (OptionUser == HandShapes.Paper &&
                     OptionComputer == HandShapes.Rock)
                return 1;

            else
                return -1;
        }

        // this method should only be called for 
        // the parameter equal to "PRZEGRYWASZ" or "WYGRYWASZ"
        private void DisplayFullResult(string MainMessage) => 
            Console.WriteLine(MainMessage +
                              " (bot wybrał " + GetNameOfOptionComputer() + ")");

        private string GetNameOfOptionComputer()
        {
            if (OptionComputer == HandShapes.Paper)
                return "Papier";

            else if (OptionComputer == HandShapes.Rock)
                return "Kamień";

            else
                return "Nożycki";
        }
    }
}
