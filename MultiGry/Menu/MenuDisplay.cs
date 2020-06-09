using System;
using System.Collections.Generic;

namespace MultiGry.Menu
{
    public class MenuDisplay : IMenuDisplay
    {
        private List<IMenuOption> MenuOptions;

        public MenuDisplay(List<IMenuOption> MenuOptions) =>
            this.MenuOptions = MenuOptions;

        public void DisplayingMenu()
        {
            Console.WriteLine("Wybierz jedną z poniższych gier/aplikacji " +
                              "naciskają odpowiedni klawisz:");

            for (int i = 0; i < MenuOptions.Count; ++i)
                Console.WriteLine((i + 1) + ". " + MenuOptions[i].NameOption);
        }
    }
}
