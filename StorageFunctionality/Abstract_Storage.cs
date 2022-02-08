using System.Collections.Generic;

namespace Green_Storage
{
    public abstract class Abstract_Storage
    {
        public Abstract_Storage(string name = null, int space = 0, int weight = 0)
        {
            this.name = name;
            this.space = space;
            this.weight = weight;
        }

        private string name;
        protected int space;
        int weight;

        public string Name { get { return name; } private set { name = value; } }
        public int Weight { get { return weight; } private set { weight = value; } }
        public int Space { get { return space; } private set { space = value; } }

        public abstract int GetSpaceLeft();
        public abstract List<Container> GetArticleNr(int nr);

        /* public abstract List<int> ReturnArticle(); */

        public abstract List<Article> GetItems();

        public abstract List<Abstract_Storage> GetAllChildrens();

        public abstract bool AddStorageObject(Abstract_Storage item);

        public abstract List<string> SearchForExisting(int ID, string path);

        public abstract List<(Container, string)> SearchForSpace(int ID, string path);
    }
}