using System.Collections.Generic;
using System.Text.Json.Serialization;
using ConsoleHelper;

namespace Green_Storage
{
    public class Article
    {
        private string name;
        private int articleNr;
        private List<string> categories = new();
        private int weight;
        private int space;

        public string Name { get { return name; } }
        public int ArticleNr { get { return articleNr; } }
        public List<string> Categories
        {
            get
            {
                if (categories != null)
                    return new List<string>(categories);
                else
                    return null;
            }
        }

        //TODO MAKE WEIGHT RELEVANT
        public int Weight { get { return weight; } }
        public int Space { get { return space; } }

        public Article ReturnForJson()
        {
            return this;
        }

        public Article(int space, int weight = -1, int articleNr = 0, string name = "", List<string> categories = null)
        {
            // this.height = height;
            // this.width = width;
            // this.length = length;

            if (categories == null)
                categories = new List<string>();
            else
                this.categories = categories;

            this.space = space;
            this.weight = weight;

            this.articleNr = articleNr;
            this.name = name;
        }

        public override string ToString()
        {
            string s = "Categories: \n";

            if (categories != null)
                for (int i = 0; i < categories.Count; i++)
                {
                    s += categories[i] + "\n";
                }

            return $"Article {ArticleNr} \"{Name}\"" +
            $"\n{Weight.ToString()} kg" +
             $"\n{Space.ToString()} Size\n" + s;
        }
    }
}