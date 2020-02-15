using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class HangmanGame : IMenuOption
    {
        public string NameOption => "Wisielec";

        string[] draw =
        {
            @"  ___________",
            @"  |    |    |",
            @"  |    |    |",
            @"  |    |    |",
            @"  |    O    |",
            @"  |   /|\   |",
            @"  |  / | \  |",
            @"  |    |    |",
            @"  |    |    |",
            @"  |   / \   |",
            @"  |  /   \  |",
            @"  |         |",
            @"  |         |",
            @" /|\       /|\",
            @"/ | \     / | \"
        };

        public OptionsCategory OptionExecuting()
        {
            return OptionsCategory.NormalOption;
        }
    }
}
