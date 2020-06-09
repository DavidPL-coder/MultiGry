using System;

namespace MultiGry.GuessingNumbers
{
    public class GetterProposalFromUser
    {
        private IFakeConsole DummyConsole;

        public GetterProposalFromUser() => 
            DummyConsole = new FakeConsole();

        public GetterProposalFromUser(IFakeConsole DummyConsole) => 
            this.DummyConsole = DummyConsole;

        // if the value provided by the user is correct then the method will 
        // return a value from 1 to 100. If this is incorrect, it will return 0
        public byte GetProposalFromUser()
        {
            try
            {
                return TryGetProposalFromUser();
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

            return 0; 
        }

        /// <exception cref = "OverflowException">
        /// when the value provided by the user is outside the range from 1 to 100
        /// </exception>
        /// <exception cref = "FormatException">
        /// when the value provided by the user is not a number
        /// </exception>
        private byte TryGetProposalFromUser()
        {
            var UsersProposal = byte.Parse(DummyConsole.ReadLine());

            if (UsersProposal < 1 || UsersProposal > 100)
                throw new OverflowException();

            return UsersProposal;
        }
    }
}
