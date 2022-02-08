using System;
using Green_Storage;
using System.Collections.Generic;
using ConsoleHelper;

namespace ConsoleProgram
{
    public static class StorageMenuH
    {
        public static Storage runThis(Storage myStorage)
        {
            if (myStorage == null)
            {
                Console.Clear();
                Console.WriteLine("Please insert the name of your wished main branch");
                string storageName = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(string), false);
                myStorage = new Storage(storageName);
            }

            StorageMenu(myStorage);
            return myStorage;
        }

        public static void StorageMenu(Storage myStorage, String header = "")
        {
            // MYSTORAE.NAME
            // GIVE OPTIONS
            // ADD / 
            // BROWSE ETC ETC
            while (true)
            {
                string baseHeader = "Storage Menu ";
                string newHeader = header + " -> " + myStorage.Name;

                Menu newMenu = new Menu(new List<String>
                {
                "Browse Storage",
                "Edit Storage",
                "Search Storage",
                "Quick Insert Item"/* ,
                "Remove Item" */
                }, baseHeader + newHeader);

                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        return;
                    case 0:
                        BrowseStorage(myStorage, newHeader);
                        break;
                    case 1:
                        if (Admin.isAdmin()) EditStorage(myStorage, newHeader);
                        break;
                    case 2:
                        SearchStorage(myStorage, newHeader);
                        break;
                    case 3:
                        AddToStorage(myStorage, newHeader);
                        break;
                        /* case 4:
                            RemoveFromStorage(myStorage, newHeader);
                            break; */
                }
            }
        }

        public static void BrowseStorage(Container myStorage, String header = "")
        {
            Console.Clear();
            System.Console.WriteLine("1");

            List<Article> items = myStorage.GetItems();

            System.Console.WriteLine("2");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(items[i].ToString() + Environment.NewLine);
            }

            System.Console.WriteLine("Container Info");
            System.Console.WriteLine("Total Space: " + myStorage.Space);
            System.Console.WriteLine("Using Space: " + myStorage.UsingSpace);
            System.Console.WriteLine("Space Left: " + (myStorage.Space - myStorage.UsingSpace));
            System.Console.WriteLine("WeightLimit " + myStorage.Weight);
            Console.ReadLine();
        }

        public static void BrowseStorage(Storage myStorage, String header = "")
        {
            while (true)
            {
                List<String> allStorages = new();

                string baseHeader = "Storage Menu ";
                string newHeader = header;

                List<Abstract_Storage> myChilds = myStorage.GetChildren();
                for (int i = 0; i < myChilds.Count; i++)
                {
                    allStorages.Add(myChilds[i].Name);
                }

                Menu newMenu = new Menu(allStorages, baseHeader + newHeader);
                int option = newMenu.Run();

                // Type type = Storage;
                if (option == -1)
                    return;

                if (myChilds[option] is Storage)
                {
                    StorageMenu(myChilds[option] as Storage, newHeader);
                }
                else
                {
                    BrowseStorage(myChilds[option] as Container);
                }
            }
        }

        public static void EditStorage(Storage myStorage, String header = "")
        {
            // Query user what you want to add?
            while (true)
            {
                Menu newMenu = new Menu(new List<String> { "Add Object", "Remove Object" }, "Edit Storage" + header);
                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        return;
                    case 0:
                        AddMenu(myStorage, header);
                        break;
                    case 1:
                        DeleteMenu(myStorage, header);
                        break;
                }
            }

            static void AddMenu(Storage myStorage, string header = "")
            {
                List<String> allStorages = new List<string>();
                List<Abstract_Storage> myChilds = myStorage.GetChildren();
                for (int i = 0; i < myChilds.Count; i++)
                {
                    allStorages.Add(myChilds[i].Name);
                }

                Menu addMenu = new Menu(new List<String> { "Add Storage", "Add Container" }, "Add Storage" + header);
                int option = addMenu.Run();

                switch (option)
                {
                    case -1:
                        break;
                    case 0:
                        AddStorage();
                        break;
                    case 1:
                        AddContainer();
                        break;
                }

                void AddStorage()
                {
                    string description, name;
                    int weight;
                    Console.Clear();

                    Console.WriteLine("Please type the Correct Information");
                    Console.Write("Name: ");
                    name = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), true);

                    Console.Write("Description: ");
                    description = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(string), true);
                    Console.Write("Weight: ");

                    // weight = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true));
                    string value = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true);
                    weight = value == null ? -1 : int.Parse(value);

                    Console.Write("Amount: ");
                    // int amount = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true));
                    value = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true);
                    int amount = value == null ? 1 : int.Parse(value);

                    if (amount > 1)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            Storage newStorage = new Storage(name + i, description, weight);
                            if (!myStorage.AddStorageObject(newStorage))
                            {
                                Console.WriteLine("Not allowed to Mix Containers and Storages");
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Storage newStorage = new Storage(name, description, weight);
                        if (!myStorage.AddStorageObject(newStorage))
                        {
                            Console.WriteLine("Not allowed to Mix Containers and Storages");
                            Console.ReadLine();
                        }
                    }
                }

                void AddContainer()
                {
                    string name;
                    int space, weight;
                    Console.Clear();

                    Console.WriteLine("Please type the Correct Information");
                    Console.Write("Name: ");
                    name = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(String), false);

                    Console.Write("Space: ");
                    space = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

                    Console.Write("Weight: ");
                    weight = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

                    Console.Write("Amount: ");
                    // int amount = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));
                    string value = ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), true);
                    int amount = value == null ? 1 : int.Parse(value);

                    if (amount > 1)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            Container newContainer = new Container(name + i, space, weight);
                            if (!myStorage.AddStorageObject(newContainer))
                            {
                                Console.WriteLine("Not allowed to Mix Containers and Storages");
                                Console.ReadLine();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Container newContainer = new Container(name, space, weight);
                        if (!myStorage.AddStorageObject(newContainer))
                        {
                            Console.WriteLine("Not allowed to Mix Containers and Storages");
                            Console.ReadLine();
                        }
                    }
                }
            }

            static void DeleteMenu(Storage myStorage, string header = "")
            {
                while (true)
                {
                    List<String> allStorages = new List<string>();
                    List<Abstract_Storage> myChilds = myStorage.GetChildren();

                    for (int i = 0; i < myChilds.Count; i++)
                    {
                        allStorages.Add(myChilds[i].Name);
                    }

                    Menu deleteMenu = new Menu(allStorages, "Delete Storage" + header, "ESC to back");
                    int option = deleteMenu.Run();

                    if (option == -1)
                        return;

                    myStorage.DeleteChild(myChilds[option]);
                }
            }
        }

        public static void SearchStorage(Storage myStorage, string header = "")
        {
            while (true)
            {
                Menu newMenu = new Menu(new List<String> { "Search by Name", "Search by ID number" }, "Search Storage" + header);
                int option = newMenu.Run();

                switch (option)
                {
                    case -1:
                        return;
                    case 0:
                        SearchByName(myStorage, header);
                        break;
                    case 1:
                        SearchByID(myStorage, header);
                        break;
                }

                static void SearchByName(Storage myStorage, string header = "")
                {
                    //TODO DO WE WANT THIS ?
                    // use The article list to get the ID and then use search by iD
                    Console.WriteLine("Please insert the Name you'd like to look for");
                    Console.Write("Name: ");

                    string name = (ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(string), false));

                    List<int> allIds = new List<int>();
                    List<Article> allArticles = ArticleManagement.ReturnFullArticle();
                    for (int i = 0; i < allArticles.Count; i++)
                    {
                        if (allArticles[i].Name.Contains(name))
                        {
                            allIds.Add(allArticles[i].ArticleNr);
                        }
                    }
                    if (allIds.Count > 0)
                    {
                        for (int i = 0; i < allIds.Count; i++)
                        {
                            List<string> allFound = myStorage.SearchForExisting(allIds[i], header);
                            foreach (var path in allFound)
                            {
                                Console.WriteLine(path);
                            }
                        }
                    }
                    else
                        System.Console.WriteLine("Nothing Found");

                    Console.ReadLine();
                }

                static void SearchByID(Storage myStorage, string header = "")
                {
                    Console.WriteLine("Please insert the ID you'd like to look for");
                    Console.Write("ID: ");

                    int ID = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

                    // WHAT RESULT DO WE WANT? PATH ? // THE OBJECT "CONTAINER" ??
                    List<string> allFound = myStorage.SearchForExisting(ID, header);

                    if (allFound.Count == 0)
                    {
                        Console.WriteLine("No Find");
                    }
                    else
                    {
                        foreach (var path in allFound)
                        {
                            Console.WriteLine(path);
                        }
                    }
                    Console.ReadLine();
                }
            }
        }

        public static void AddToStorage(Storage myStorage, string header = "")
        {
            Console.WriteLine("Please insert the ID you'd like to add for");
            Console.Write("ID: ");

            int ID = int.Parse(ConsoleHelper.ConsoleFastInputs.GetViableInput(typeof(int), false));

            List<(Container, string)> allFound = myStorage.SearchForSpace(ID, header);

            // Sort by int pick first samllest number?
            allFound.Sort((x, y) => y.Item1.GetSpaceLeft().CompareTo(x.Item1.GetSpaceLeft()));

            for (int i = 0; i < allFound.Count; i++)
            {
                if (ArticleManagement.UseDic(ID).Space <= allFound[i].Item1.GetSpaceLeft())
                {
                    allFound[i].Item1.AddStorageObject(ID);
                    Console.WriteLine("Added Object to " + allFound[i].Item2);
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine("None Suitable Container Found");
        }

        /* public static void RemoveFromStorage(Storage myStorage, string header = "")
        {
            List<Abstract_Storage> containers = myStorage.GetContainers();
            List<string> arts = new();

            foreach (Abstract_Storage item in containers)
            {
                arts.Add(item.ReturnArticle()[0].ToString());
            }

            Menu RemoveMenu = new(arts, "Articles to Remove");
            RemoveMenu.Run();
            Console.ReadLine();
        } */
    }
}
