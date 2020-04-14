using System;

namespace MultiGry.PaperRockScissors
{
    class GetterNumberOfRounds
    {
        public byte GetNumberOfRoundsFromUser()
        {
            Console.Clear();
            Console.Write("Podaj ilość rund, w których zmierzysz się z botem: ");
            try
            {
                return TryGetNumberOfRoundsFromUser();
            }
            catch (OverflowException)
            {
                DisplayMessage("Nie można rozegrać tylu rund! Dozwolona " +
                               "wartość to 1-50.");
            }
            catch (FormatException)
            {
                DisplayMessage("Wartość jest nieprawidłowa");       
            }

            return GetNumberOfRoundsFromUser();
        }

        private byte TryGetNumberOfRoundsFromUser()
        {
            byte Rounds = byte.Parse(Console.ReadLine());
            if (Rounds <= 0 || Rounds > 50)
                throw new OverflowException();

            return Rounds;
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }
    }
}
