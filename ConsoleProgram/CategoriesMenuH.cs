using System;
using Green_Storage;
using System.Collections.Generic;
using ConsoleHelper;

namespace ConsoleProgram
{
    public static class CategoriesMenuH
    {

        public static void runThis()
        {
            CategoriesMenu();
        }

        public static void CategoriesMenu()
        {
            while (true)
            {
                Menu articleMenu = new Menu(new List<String> { "Browse Categories", "Add Category" }, "Categories Menu");
                int option = articleMenu.Run();

                switch (option)
                {
                    case -1:
                        return;

                    case 0:
                        BrowseCategories();
                        break;

                    case 1:
                        AddCategory();
                        break;
                }
            }

            static void BrowseCategories()
            {
                Console.Clear();
                List<string> allCategories = Category.GetCategories();

                //TODO ADD better browse Functionaltiy
                for (int i = 0; i < allCategories.Count; i++)
                {
                    Console.WriteLine(allCategories[i] + " " + i);
                }
                Console.ReadLine();
            }

            static void AddCategory()
            {
                Console.Clear();
                string nameOfCategory;

                Console.WriteLine("You're adding a new category, press anything to continue or press Escape to Exit");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }

                Console.WriteLine("Please insert the Name of the category");
                Console.Write("Name: ");
                nameOfCategory = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(string), false);

                Category.AddCategory(nameOfCategory);
            }
        }
    }
}
