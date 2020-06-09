using System;

namespace MultiGry.GuessingNumbers
{
    public class PerformerGame : IPerformerGame
    {
        public byte NumberToGuess { set; get; }
        private int UserAttempt;
        private byte UsersProposal;
        private GetterProposalFromUser GetterProposal;

        public PerformerGame()
        {           
            NumberToGuess = 0;

            GetterProposal = new GetterProposalFromUser();
        }

        public PerformerGame(GetterProposalFromUser GetterProposal)
        {
            NumberToGuess = 0;

            this.GetterProposal = GetterProposal;
        }

        /// <exception cref = "InvalidOperationException">
        /// if you have not entered a number drawn in the NumberToGuess field 
        /// or entered a value outside of the range of 1 to 100
        /// </exception>
        /// <returns> number of user attempts </returns>
        public int GameProcessing()
        {
            UserAttempt = 1;

            if (NumberToGuess < 1 || NumberToGuess > 100)
                throw new InvalidOperationException("Wylosowana liczba jest w złym przedziale");

            UserAttemptsToGuessNumber();

            return UserAttempt;
        }

        private void UserAttemptsToGuessNumber()
        {
            Console.WriteLine("Wybierz liczbę z przedziału od 1 do 100:");

            do
            {
                Console.Write("(Próba " + UserAttempt + ") Podaj liczbę: ");
                UsersProposal = GetterProposal.GetProposalFromUser();
                ProcessingUserProposals();
            }
            while (UsersProposal != NumberToGuess);
        }

        private void ProcessingUserProposals()
        {
            if (UsersProposal != NumberToGuess && UsersProposal != 0)
            {
                string Message = UsersProposal > NumberToGuess ? "Za dużo!"
                                                               : "Za mało!";
                Console.WriteLine(Message);
                System.Threading.Thread.Sleep(1500);

                ++UserAttempt;
            }
        }
    }
}
