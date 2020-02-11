using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
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
            Console.WriteLine("Godzina: " + ConversionToBinaryNumber(DateTime.Now.Hour));
            Console.WriteLine("Minuty:" + ConversionToBinaryNumber(DateTime.Now.Minute));
            Console.WriteLine("Sekundy: " + ConversionToBinaryNumber(DateTime.Now.Second));
        }

        private string ConversionToBinaryNumber(int DecimalNumber, StringBuilder BinaryNumber = null)
        {
            if (BinaryNumber == null)
                BinaryNumber = new StringBuilder();

            if (DecimalNumber > 0)
            {
                BinaryNumber.Insert(0, DecimalNumber % 2);
                return ConversionToBinaryNumber(DecimalNumber /= 2, BinaryNumber);
            }

            return BinaryNumber.ToString() != "" ? BinaryNumber.ToString() : "0";
        }
    }
}
