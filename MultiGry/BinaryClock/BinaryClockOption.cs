using System;
using System.Text;

namespace MultiGry.BinaryClock
{
    class BinaryClockOption : IMenuOption
    {
        public string NameOption => "Zegar binarny";

        public OptionsCategory OptionExecuting()
        {
            while (!Console.KeyAvailable)
            {
                DisplayCurrentTime();
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }

            return OptionsCategory.NormalOption;
        }

        private void DisplayCurrentTime()
        {
            var BinaryHour = ConvertToBinaryNumber(DateTime.Now.Hour);
            var BinaryMinute = ConvertToBinaryNumber(DateTime.Now.Minute);
            var BinarySecond = ConvertToBinaryNumber(DateTime.Now.Second);

            Console.WriteLine("Godzina: " + BinaryHour);
            Console.WriteLine("Minuty:" + BinaryMinute);
            Console.WriteLine("Sekundy: " + BinarySecond);
        }

        // this method should be called for numbers greater than 0. 
        // When calling this function, only the first parameter should be given
        private string ConvertToBinaryNumber(int DecimalNumber, 
                                             StringBuilder BinaryNumber = null)
        {
            if (BinaryNumber == null)
                BinaryNumber = new StringBuilder();

            if (DecimalNumber > 0)
            {
                BinaryNumber.Insert(0, DecimalNumber % 2);
                return ConvertToBinaryNumber(DecimalNumber / 2, BinaryNumber);
            }

            return BinaryNumber.ToString() != "" ? BinaryNumber.ToString() : "0";
        }
    }
}
