using System;
using Green_Storage;
using System.Collections.Generic;
using ConsoleHelper;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage testStorage = StartData();

            Storage myStorage = null; // testStorage; //new Storage(storageName); testStorage;

            while (true)
            {
                Menu newMenu = new Menu(new List<String> { "Storage", "Article", "Categories \n", "Load", "Save" }, "Start Menu");
                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        Environment.Exit(0);
                        break;
                    case 0:
                        myStorage = StorageMenuH.runThis(myStorage);
                        break;
                    case 1:
                        ArticleMenuH.runThis();
                        break;
                    case 2:
                        CategoriesMenuH.runThis();
                        break;
                    case 3:
                        // myStorage = LoadStorageData();
                        myStorage = LoadMenu(myStorage);
                        break;
                    case 4:
                        SaveMenu(myStorage);
                        // SaveStorageData(myStorage);
                        break;
                }
            }
        }
        private static void SaveMenu(Storage myStorage)
        {
            while (true)
            {
                Menu newMenu = new Menu(new List<String> { "Save Storage", " Save Article", "Save Categories" }, "Save Menu");
                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        return;

                    case 0:
                        SaveStorageData(myStorage);
                        break;
                    case 1:
                        SaveArticleData();
                        break;
                    case 2:
                        SaveCategorieData();
                        break;
                }
            }
        }
        private static Storage LoadMenu(Storage myStorage)
        {
            while (true)
            {
                Menu newMenu = new Menu(new List<String> { "Load Storage", " Load Articles ", "Load Categories" }, "Load Menu");
                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        return myStorage;

                    case 0:
                        myStorage = LoadStorageData();
                        break;
                    case 1:
                        LoadArticleData();
                        break;
                    case 2:
                        LoadCategorieData();
                        break;
                }
            }
        }

        private static void SaveArticleData()
        {
            string path;
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the dictonary you'd like to save too ");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                if (Path.IsPathRooted(path))
                {
                    FileAttributes attr = File.GetAttributes(path);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        System.Console.WriteLine("FileName you'd like");
                        Console.Write("FileName :");
                        string fileName = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                        path = path + @"\" + fileName;
                        break;
                    }
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }

            List<Article> articles = ArticleManagement.ReturnFullArticle();
            for (int i = 0; i < articles.Count; i++)
            {
                System.Console.WriteLine(articles[i].Name);
            }
            string jsonString = JsonSerializer.Serialize(articles);
            File.WriteAllText(path, jsonString);

            Console.Clear();
            System.Console.WriteLine("SAVE SUCESSFULL");
            System.Console.WriteLine(jsonString);
            Console.ReadLine();
        }

        private static void SaveCategorieData()
        {
            string path;
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the dictonary you'd like to save too ");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                if (Path.IsPathRooted(path))
                {
                    FileAttributes attr = File.GetAttributes(path);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        System.Console.WriteLine("FileName you'd like");
                        Console.Write("FileName :");
                        string fileName = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                        path = path + @"\" + fileName;
                        break;
                    }
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }
            string jsonString = JsonSerializer.Serialize(Category.GetCategories());
            File.WriteAllText(path, jsonString);

            Console.Clear();
            System.Console.WriteLine("SAVE SUCESSFULL");
            System.Console.WriteLine(jsonString);
            Console.ReadLine();
        }

        private static void SaveStorageData(Storage myStorage)
        {
            string path;
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the dictonary you'd like to save too ");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                if (Path.IsPathRooted(path))
                {
                    FileAttributes attr = File.GetAttributes(path);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        System.Console.WriteLine("FileName you'd like");
                        Console.Write("FileName :");
                        string fileName = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                        path = path + @"\" + fileName;
                        break;
                    }
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }
            string jsonString = JsonSerializer.Serialize(myStorage as Storage);

            File.WriteAllText(path, jsonString);

            Console.Clear();
            System.Console.WriteLine("SAVE SUCESSFULL");
            System.Console.WriteLine(jsonString);
            Console.ReadLine();
        }

        private static void LoadArticleData()
        {
            string path = "";
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the path you'd like to load (Dont forget filename)");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                // How do i check file is json file or json valid?
                if (File.Exists(path))
                {
                    System.Console.WriteLine("Valid Path");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }

            //File.ReadLines
            string jsonString = File.ReadAllText(path);
            List<String> jsonStrings = File.ReadLines(path).ToList();

            Console.ReadLine();
            List<Article> tempArticles = new List<Article>();
            tempArticles = JsonSerializer.Deserialize<List<Article>>(jsonString);

            ArticleManagement.ClearAllArticles();
            for (int i = 0; i < tempArticles.Count; i++)
            {
                ArticleManagement.AddArticle(tempArticles[i]);
                System.Console.WriteLine("ADDED " + tempArticles[i].ArticleNr);
            }

            Console.Clear();
            System.Console.WriteLine("LOAD SUCESFULL");
            Console.ReadLine();
        }

        private static void LoadCategorieData()
        {
            string path = "";
            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the path you'd like to load (Dont forget filename)");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                // How do i check file is json file or json valid?
                if (File.Exists(path))
                {
                    System.Console.WriteLine("Valid Path");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }
            string jsonString = File.ReadAllText(path);
            List<String> jsonStrings = File.ReadLines(path).ToList();

            Console.ReadLine();
            List<string> tempStrings = new List<string>();
            tempStrings = JsonSerializer.Deserialize<List<string>>(jsonString);

            Category.ClearCategories();

            for (int i = 0; i < tempStrings.Count; i++)
            {
                Category.AddCategory(tempStrings[i]);
                System.Console.WriteLine("ADDED " + tempStrings[i]);
            }

            Console.Clear();
            System.Console.WriteLine("LOAD SUCESFULL");
            Console.ReadLine();
        }

        private static Storage LoadStorageData()
        {
            Storage tempStorage = new Storage();
            string path = "";

            while (true)
            {
                Console.Clear();
                System.Console.WriteLine("Please type the path you'd like to load (Dont forget filename)");
                Console.Write("Path :");
                path = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                // How do i check file is json file or json valid?
                if (File.Exists(path))
                {
                    System.Console.WriteLine("Valid Path");
                    break;
                }
                else
                {
                    System.Console.WriteLine("Invalid Path");
                    Console.ReadLine();
                }
            }

            //File.ReadLines
            string jsonString = File.ReadAllText(path);
            List<String> jsonStrings = File.ReadLines(path).ToList();

            Console.ReadLine();
            tempStorage = JsonSerializer.Deserialize<Storage>(jsonString);

            Console.Clear();
            System.Console.WriteLine("LOAD SUCESFULL");
            Console.ReadLine();

            return tempStorage;
        }

        private static Storage StartData()
        {
            //ArticleManagement.SerializeArticle();
            Storage myStorage = new Storage("Main", "Holds All Storages");

            Storage house1 = new Storage("House 1", "Left hous next to the red container");
            Storage house2 = new Storage("House 2", "House next to the blue truck");

            Storage hylla2 = new Storage("Hylla 2", "House next to the blue truck");
            Storage hylla3 = new Storage("Hylla 3", "House next to the blue truck");

            Storage hylla1 = new Storage("Hylla1");
            Container space1 = new Container("C1", 25, 20);
            Container space2 = new Container("C2", 25, 20);
            Container space3 = new Container("C3", 25, 20);
            Container space4 = new Container("C4", 25, 20);
            Container space5 = new Container("C5", 25, 20);

            Article art1 = new(10, 20, 55634604, "A box");
            Article art2 = new(10, 20, 32132132, "Another box");

            ArticleManagement.AddArticle(art1);

            space1.AddStorageObject(art1.ArticleNr);

            myStorage.AddStorageObject(house1);
            house1.AddStorageObject(hylla1);
            house1.AddStorageObject(hylla3);
            hylla3.AddStorageObject(space5);
            hylla1.AddStorageObject(space1);
            hylla1.AddStorageObject(space2);
            hylla1.AddStorageObject(space3);
            house2.AddStorageObject(hylla2);
            hylla2.AddStorageObject(space4);
            myStorage.AddStorageObject(house2);

            return myStorage;
        }
    }
}
