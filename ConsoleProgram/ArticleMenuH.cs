using System;
using Green_Storage;
using System.Collections.Generic;
using ConsoleHelper;

namespace ConsoleProgram
{
    public static class ArticleMenuH
    {
        public static void runThis()
        {
            ArticleMenu();
        }

        public static void ArticleMenu()
        {
            while (true)
            {
                Menu articleMenu = new Menu(new List<String> { "Browse Articles", "Add Article" }, "Article Menu");
                int option = articleMenu.Run();

                switch (option)
                {
                    case -1:
                        return;

                    case 0:
                        BrowseArticle();
                        break;

                    case 1:
                        AddArticles();
                        break;
                }
            }

            static void BrowseArticle()
            {
                Console.Clear();
                List<Article> allArticles = ArticleManagement.ReturnFullArticle();

                //TODO ADD better browse Functionaltiy
                for (int i = 0; i < allArticles.Count; i++)
                {
                    Console.WriteLine(allArticles[i].Name + " " + allArticles[i].ArticleNr);
                }

                Console.ReadLine();
            }

            static void AddArticles()
            {
                Console.Clear();
                const string finishWord = "Done";

                string name;
                int? tempArtNr;
                int weight = 0, space = 0, artNr = 0;

                Console.WriteLine("Please insert data to add a new article.");
                Console.WriteLine("Press anything to continue, or Press Escape to cancel.");
                Console.WriteLine();

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }

                Console.Clear();

                Console.WriteLine("Please insert the Name");
                Console.Write("Name: ");
                name = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(string), false);

                Console.WriteLine("Please insert the Weight (kg) of the item");
                Console.Write("Weight: ");
                weight = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

                Console.WriteLine("Please insert the Height (m) of the item");
                Console.Write("Space: ");
                space = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

                Console.Write("Please insert the Article number (no value to auto assign)");
                string value = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true);
                tempArtNr = value == null ? null : int.Parse(value);

                artNr = tempArtNr == null ? ArticleManagement.FindValidKey() : tempArtNr.Value;

                List<String> allCatsString = new();
                List<String> chosenCats = new();
                allCatsString = Category.GetCategories();
                allCatsString.Add(finishWord);

                // Ask for genres
                while (true)
                {
                    // run all genresstring - allgenres contains
                    Menu genreMenu = new Menu(allCatsString, "Select Which kind / kinds of Item this is");
                    int genreOption = genreMenu.Run();

                    if (allCatsString[genreOption] == finishWord || genreOption == Menu.breakValue)
                        break;

                    string temp = allCatsString[genreOption];
                    chosenCats.Add(temp);
                    allCatsString.Remove(temp);
                }

                Article myItem = new Article(space, weight, artNr, name, chosenCats);
                ArticleManagement.AddArticle(myItem);
            }
        }
    }
}
