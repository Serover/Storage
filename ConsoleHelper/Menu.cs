using System;
using System.Collections.Generic;

namespace ConsoleHelper
{

    public class Menu
    {
        const ConsoleKey breakKey = ConsoleKey.Escape;
        const ConsoleKey confirmKey = ConsoleKey.Enter;

        const ConsoleKey upKey = ConsoleKey.UpArrow;
        const ConsoleKey downKey = ConsoleKey.DownArrow;

        public const int breakValue = -1;

        // Do i  want Action / Actions?
        List<string> menuOptionDescription = new();
        string header = "";
        string footer = "";
        int menuIndex = 0;

        public Menu()
        {
            this.menuOptionDescription = new List<string>();
        }

        public Menu(List<string> menuOptions)
        {
            this.menuOptionDescription = menuOptions;
        }

        public Menu(List<string> menuOptions, string header)
        {
            this.menuOptionDescription = menuOptions;
            this.header = header;
        }

        public Menu(List<string> menuOptions, string header, string footer)
        {
            this.menuOptionDescription = menuOptions;
            this.header = header;
            this.footer = footer;
        }

        ///<SUMMARY>
        /// END STATEMENT WILL RETURN -1
        ///</SUMMARY>
        public int Run()
        {
            if (menuOptionDescription == null)
                menuOptionDescription = new List<string>();

            menuIndex = 0;
            ConsoleKeyInfo keyPressed;
            while (true)
            {
                if (menuOptionDescription.Count > 0)
                    menuIndex = Math.Clamp(menuIndex, 0, menuOptionDescription.Count - 1);

                Console.Clear();
                ShowMenu();

                List<ConsoleKey> validKeys = new List<ConsoleKey>(new ConsoleKey[] { downKey, upKey, breakKey, confirmKey });
                keyPressed = ConsoleFastInputs.GetKeyFast(validKeys);

                if (keyPressed.Key == downKey)
                {
                    menuIndex++;
                }
                else if (keyPressed.Key == upKey)
                {
                    menuIndex--;
                }
                else if (keyPressed.Key == breakKey)
                {
                    // mby something else is better? like null?
                    return breakValue;
                }
                else if (keyPressed.Key == confirmKey && menuOptionDescription.Count > 0)
                {
                    return menuIndex;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine(header);

            for (int i = 0; i < menuOptionDescription.Count; i++)
            {
                string front = ""; // mabey cache for performance?
                if (menuIndex == i)
                {
                    front = " ->: ";
                }

                Console.WriteLine($"{front}{menuOptionDescription[i]}");
            }

            Console.WriteLine(footer);
        }
    }
}
