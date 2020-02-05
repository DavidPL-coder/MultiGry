using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class PaperRockScissorsGame : IMenuOption
    {
        public string NameOption => "Papier, kamień, nożyce";

        public OptionsCategory OptionExecuting()
        {
            Console.WriteLine("hhhhh");
            System.Threading.Thread.Sleep(2500);
            return OptionsCategory.Game;
            // throw new NotImplementedException();
        }
    }
}
