using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_Storage
{
    public class Category
    {
        private static List<string> categories = new List<string>();

        public static void AddCategory(string newCat)
        {
            if (!categories.Contains(newCat))
            {
                categories.Add(newCat);
            }
        }
        public static void ClearCategories()
        {
            categories.Clear();
        }
        public static List<string> GetCategories()
        {
            return new List<string>(categories);
        }
    }
}