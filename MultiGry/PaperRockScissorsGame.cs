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

        public void OptionExecuting()
        {
            Console.WriteLine("hhhhh");
            // throw new NotImplementedException();
        }
    }
}
