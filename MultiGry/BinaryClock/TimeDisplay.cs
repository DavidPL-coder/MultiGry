using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry.BinaryClock
{
    public class TimeDisplay
    {
        private BinaryNumberConverter Converter;

        public TimeDisplay() => 
            Converter = new BinaryNumberConverter();

        public void DisplayCurrentTime()
        {
            var BinaryHour = Converter.ConvertToBinaryNumber(DateTime.Now.Hour);
            var BinaryMinute = Converter.ConvertToBinaryNumber(DateTime.Now.Minute);
            var BinarySecond = Converter.ConvertToBinaryNumber(DateTime.Now.Second);

            Console.WriteLine("Godzina: " + BinaryHour);
            Console.WriteLine("Minuty:" + BinaryMinute);
            Console.WriteLine("Sekundy: " + BinarySecond);
        }
    }
}
