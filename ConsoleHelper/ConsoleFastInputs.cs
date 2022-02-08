using System;
using System.Collections.Generic;

namespace ConsoleHelper
{
    public class ConsoleFastInputs
    {
        public static char GetKeyFast(string viableInputs)
        {
            while (true)
            {
                Char inputChar = Console.ReadKey(true).KeyChar;

                if (viableInputs.Contains(inputChar))
                {
                    return inputChar;
                }

                Console.WriteLine("Please insert valid command");
            }
        }

        public static ConsoleKeyInfo GetKeyFast(string viableInputs, List<ConsoleKey> keyinfos)
        {
            while (true)
            {
                ConsoleKeyInfo inputChar = Console.ReadKey(true);

                if (viableInputs.Contains(inputChar.KeyChar))
                {
                    return inputChar;
                }
                else
                {
                    for (int i = 0; i < keyinfos.Count; i++)
                    {
                        if (inputChar.Key == keyinfos[i])
                        {
                            return inputChar;
                        }
                    }
                }

                Console.WriteLine("Please insert valid command");
            }
        }

        public static ConsoleKeyInfo GetKeyFast(List<ConsoleKey> keyinfos)
        {
            while (true)
            {
                ConsoleKeyInfo inputChar = Console.ReadKey(true);

                for (int i = 0; i < keyinfos.Count; i++)
                {
                    if (inputChar.Key == keyinfos[i])
                    {
                        return inputChar;
                    }
                }

                Console.WriteLine("Please insert valid command");
            }
        }

        public static string GetViableInput(Type type)
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                try
                {
                    Convert.ChangeType(input, type);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FORMATING ERROR, TRY AGAIN");
                    continue;
                }
                break;
            }
            return input;
        }

        public static string GetViableInput(Type type, bool allowNull)
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) && allowNull == false)
                {
                    Console.WriteLine("NULL VALUE NOT ACCEPTED");
                    continue;
                }
                if (string.IsNullOrEmpty(input) && allowNull == true)
                    return null;

                try
                {
                    Convert.ChangeType(input, type);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FORMATING ERROR, TRY AGAIN");
                    continue;
                }
                break;
            }
            return input;
        }
    }
}
