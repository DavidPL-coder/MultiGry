using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class Program
    {
        static List<IMenuOption> GetListOfMenuOption()
        {
            var options = new List<IMenuOption>();

            options.Add(new PaperRockScissorsGame());
            options.Add(new ExitOption());

            return options;
        }

        static void Main(string[] args)
        {
            MainMenu Menu = new MainMenu(GetListOfMenuOption());

            Menu.ExecutingTheMainMenuOperation();
        }
    }
}
