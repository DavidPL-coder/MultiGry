using System;

namespace MultiGry.GuessingNumbers
{
    class GuessingNumbersGame : IMenuOption
    {
        public string NameOption => "Zgadywanie liczb";
        private byte NumberToGuess; 
        private int UserAttempt;  
        private byte UsersProposal; 

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            UserAttemptsToGuessNumber();
            ResultDisplay();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            UserAttempt = 1;

            var random = new Random();
            NumberToGuess = (byte) random.Next(1, 101);
        }

        private void UserAttemptsToGuessNumber()
        {
            Console.WriteLine("Wybierz liczbę z przedziału od 1 do 100:");

            do
            {
                Console.Write("(Próba " + UserAttempt + ") Podaj liczbę: ");
                GetProposalFromUser();
                Console.Clear();
            }
            while (UsersProposal != NumberToGuess);
        }

        private void GetProposalFromUser()
        {
            try
            {
                TryGetProposalFromUser();
            }
            catch (FormatException)
            {                
                Console.WriteLine("Nieprawidłowa wartość!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Podana liczba jest poza dozwolonym " +
                                  "przedziałem (zakres wynosi 1 - 100)!");
            }
        }

        public void TryGetProposalFromUser()
        {
            UsersProposal = byte.Parse(Console.ReadLine());

            if (UsersProposal < 1 || UsersProposal > 100)
                throw new OverflowException();

            if (UsersProposal != NumberToGuess)
            {
                DisplayMessageAboutFailedGuessing();
                ++UserAttempt;
            }
        }

        private void DisplayMessageAboutFailedGuessing()
        {
            string Message = UsersProposal > NumberToGuess ? "Za dużo!" : "Za mało!";
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void ResultDisplay()
        {
            Console.WriteLine("Odgadłeś tę liczbę w próbie " + UserAttempt);
            Console.ReadKey();
        }
    }
}
