using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Green_Storage
{
    // SMALLEST OBJECT?
    // SPACE
    // ART NR

    public class Container : Abstract_Storage
    {
        List<int> artNrs = new();

        [JsonConstructor]
        public Container(string Name, List<int> ArtNrs, int Space, int Weight) : base(Name, Space, Weight)
        {
            this.artNrs = ArtNrs;
        }
        public List<int> ArtNrs { get { return artNrs; } set {; } }


        public Container(string name = null, int space = 0, int weight = 0) : base(name, space, weight)
        {

        }

        public Container(string name = null, int space = 0, int weight = 0, int articleNr = 0) : base(name, space, weight)
        {
            // this.articleNr = articleNr;
        }

        [JsonIgnore]
        public int UsingSpace
        {
            get
            {
                int total = 0;
                for (int i = 0; i < artNrs.Count; i++)
                    total += ArticleManagement.UseDic(artNrs[i]).Space;

                return total;
            }
            set
            {
                ;
            }
        }

        public override List<Article> GetItems()
        {
            List<Article> returnList = new();

            for (int i = 0; i < artNrs.Count; i++)
            {
                returnList.Add(ArticleManagement.UseDic(artNrs[i]));

                if (ArticleManagement.UseDic(artNrs[i]) == null)
                {
                    throw new Exception("NO ITEM WITH THAT ID EXISTS MAKE SURE YOU LOAD ALL THE DATA YOU NEED");
                }
            }

            return returnList;
        }

        public override int GetSpaceLeft()
        {
            return space - UsingSpace;
        }

        public override List<Container> GetArticleNr(int nr)
        {
            throw new NotImplementedException();
        }

        public override List<Abstract_Storage> GetAllChildrens()
        {
            throw new NotImplementedException();
        }

        /* public override List<int> ReturnArticle()
        {
            return artNrs;
        } */

        public override List<String> SearchForExisting(int ID, string path)
        {
            if (this.artNrs.Contains(ID))
                return new List<string>() { path };
            else
                return null;

            //throw new NotImplementedException();
        }

        // RETURN TUPLE, <int, string> // <LEFTOVER SIZE, PATH> then use the size to determain best fit?
        public override List<(Container, string)> SearchForSpace(int ID, string path)
        {
            int articleSpace = ArticleManagement.UseDic(ID).Space;
            //
            if (UsingSpace + articleSpace <= space)
            {
                int spaceAfterAddition = space - UsingSpace - articleSpace;

                return new List<(Container, string)>() { (this, path) };
            }
            else
                return null;

        }

        public override bool AddStorageObject(Abstract_Storage item)
        {
            throw new NotImplementedException();
        }

        public void AddStorageObject(int item)
        {
            if (ArticleManagement.UseDic(item).Space <= space - UsingSpace)
            {
                UsingSpace += ArticleManagement.UseDic(item).Space;
                this.artNrs.Add(item);
            }
            else
                throw new Exception("ADDED OBJECT INTO PLACE NOT FIT" + " StorSpace: " + space + " ART SPACE " + ArticleManagement.UseDic(item).Space + " USINGSPACE " + UsingSpace);

        }

        public void RemoveStorageObject(int item, int count = 1)
        {
            artNrs.Remove(item);
        }
    }
}