using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Green_Storage
{
    [Serializable]
    public class Storage : Abstract_Storage
    {
        string description;
        /// <summary>
        ///Weight in Kilo
        ///</summary> 
        //TODO MAKE RELEVANT
        float maxWeight; // -1 = infinity?

        [JsonConstructor]
        public Storage(List<Storage> ChildsS, List<Container> ChildsC, string Name, int Space = 0, int Weight = 0) : base(Name, Space, Weight)
        {
            childs = new List<Abstract_Storage>();

            if (ChildsS != null)
            {
                // is storage
                childs.AddRange(ChildsS);
            }
            else
            {
                childs.AddRange(ChildsC);
            }
        }

        public Storage(string Name) : base(Name)
        {

            childs = new List<Abstract_Storage>();
        }

        public List<Abstract_Storage> childs;

        public List<Storage> ChildsS
        {
            get
            {
                if (childs.Count > 0)
                {
                    if (childs[0] is Storage)
                    {
                        List<Storage> returnList = new();
                        for (int i = 0; i < childs.Count; i++)
                        {
                            returnList.Add(childs[i] as Storage);
                        }
                        return returnList;
                    }
                }

                return null;
            }
            private set
            {
                ;
            }
        }

        public List<Container> ChildsC
        {
            get
            {
                if (childs.Count > 0)
                {
                    if (childs[0] is Container)
                    {
                        List<Container> returnList = new();
                        for (int i = 0; i < childs.Count; i++)
                        {
                            returnList.Add(childs[i] as Container);
                        }
                        return returnList;
                    }
                }

                return null;
            }
            private set
            {; }
        }

        public Storage(string name = null, string description = null, float maxWeight = -1) : base(name)
        {
            this.description = description;
            this.maxWeight = maxWeight;

            childs = new List<Abstract_Storage>();
        }

        public override int GetSpaceLeft()
        {
            int total = 0;
            foreach (var item in childs)
            {
                total += item.GetSpaceLeft();
            }
            return total;
        }

        public override List<Article> GetItems()
        {
            throw new NotImplementedException();
        }

        public override List<Container> GetArticleNr(int nr)
        {
            throw new NotImplementedException();
        }

        public override List<Abstract_Storage> GetAllChildrens()
        {
            throw new NotImplementedException();
        }

        public List<Abstract_Storage> GetChildren()
        {
            return (childs == null) ? new List<Abstract_Storage>() : childs;
        }

        public List<Abstract_Storage> GetContainers()
        {
            List<Abstract_Storage> list = new();

            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i] is Container)
                {
                    list.Add(childs[i]);
                }
            }

            return list;
        }

        public override List<String> SearchForExisting(int ID, string path)
        {
            List<String> returnList = new List<String>();

            for (int i = 0; i < childs.Count; i++)
            {
                List<String> tempList = new List<String>();

                if (childs[i].SearchForExisting(ID, path + " -> " + childs[i].Name) != null)
                    tempList = childs[i].SearchForExisting(ID, path + " -> " + childs[i].Name);

                for (int j = 0; j < tempList.Count; j++)
                    returnList.Add(tempList[j]);
            }

            return returnList;
            //throw new NotImplementedException();
        }

        public override List<(Container, string)> SearchForSpace(int ID, string path)
        {
            List<(Container, string)> returnList = new();

            for (int i = 0; i < childs.Count; i++)
            {
                List<(Container, string)> tempList = new List<(Container, string)>();

                if (childs[i].SearchForSpace(ID, path + " -> " + childs[i].Name) != null)
                    tempList = childs[i].SearchForSpace(ID, path + " -> " + childs[i].Name);

                for (int j = 0; j < tempList.Count; j++)
                    returnList.Add(tempList[j]);
            }

            return returnList;
        }

        public override bool AddStorageObject(Abstract_Storage item)
        {
            // MAKE SURE THEY'RE OF THE SAME TYPE (DO WE WANT IT?)
            if (childs != null && childs.Count > 0)
            {
                if (childs[0].GetType() == item.GetType())
                {
                    childs.Add(item);
                    return true;
                }
                else
                    return false;
            }
            else
                childs.Add(item); return true;
        }

        public void DeleteChild(Abstract_Storage child)
        {
            childs.Remove(child);
        }

        /* public override List<int> ReturnArticle()
        {
            throw new NotImplementedException();
        } */
    }
}