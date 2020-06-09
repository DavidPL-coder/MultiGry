using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry.BinaryClock
{
    public class BinaryNumberConverter
    {
        // this method should be called for numbers greater than 0. 
        // When calling this function, only the first parameter should be given
        public string ConvertToBinaryNumber(int DecimalNumber,
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
