using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class GuessingNumbersGame : IMenuOption
    {
        public string NameOption => "Zgadywanie liczb";
        private int NumberToGuess;
        private int UserAttempt;
        private int UsersProposal;

        public GuessingNumbersGame() => 
            UserAttempt = 1;

        public OptionsCategory OptionExecuting()
        {
            NumberDraw();
            UserAttemptsToGuessNumber();
            ResultDisplay();

            return OptionsCategory.Game;
        }


        private void NumberDraw()
        {
            Random random = new Random();
            NumberToGuess = random.Next(1, 101);
        }

        private void UserAttemptsToGuessNumber()
        {
            Console.WriteLine("Wybierz liczbę z przedziału od 1 do 100:");

            do
            {
                Console.Write("(Próba " + UserAttempt + ") Podaj liczbę: ");
                GetProposalFromUser();
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
                Console.WriteLine("To nie jest liczba!" + "\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Podana liczba jest poza dozwolonym przedziałem (zakres wynosi 1 - 100)!" + "\n");
            }
        }

        private void TryGetProposalFromUser()
        {
            UsersProposal = int.Parse(Console.ReadLine());

            if (UsersProposal < 1 || UsersProposal > 100)
                throw new OverflowException();

            if (UsersProposal != NumberToGuess)
            {
                string FailedGuessMessage = UsersProposal > NumberToGuess ? "Za dużo!" : "Za mało!";
                Console.WriteLine(FailedGuessMessage + "\n");
                ++UserAttempt;
            }
        }

        private void ResultDisplay()
        {
            Console.WriteLine("Odgadłeś tę liczbę w próbie " + UserAttempt);
        }
    }
}
