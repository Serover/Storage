using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Green_Storage
{
    //Class for handling articles, including dictonary for storage
    public class ArticleManagement
    {
        private static Dictionary<int, Article> articles = new Dictionary<int, Article>();

        ///<SUMMARY>
        /// WILL OVERWRITE EXISTING
        ///</SUMMARY>
        public static void AddArticle(Article art) //Method for creating article
        {
            //WILL OVERWRITE EXISTING ARTICLE NUMBER
            articles[art.ArticleNr] = art;
        }

        public static void ClearAllArticles()
        {
            articles.Clear();
        }

        public static int FindValidKey()
        {
            int l = 0;
            while (true)
            {
                if (!articles.ContainsKey(l))
                {
                    return l;
                }
                else
                    l++;
            }
        }

        public static List<Article> ReturnFullArticle()
        {
            Dictionary<int, Article>.ValueCollection values = articles.Values;
            List<Article> arts = new();

            foreach (Article art in values)
            {
                arts.Add(art);
            }
            return arts;
        }

        public static Article UseDic(int id)
        {
            Article item;
            if (articles.TryGetValue(id, out item))
                item = articles[id];
            else
                item = null;

            return item;
        }
    }
}
