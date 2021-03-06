﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chatbot
{
    public class Lingkungan
    {
        #region  Data Location
        public static void CreateLocation()
        {
            List<string> lokasi = new List<string>();
            lokasi.Add(getEnvirontment());
            lokasi.Add(getKnowledgeBase());
            lokasi.Add(getDataBaru());
            lokasi.Add(getDataCache());
            lokasi.Add(getDataCacheKata());
            dbDataContext db = new dbDataContext();
            foreach (var item in db.tbDomains.ToList())
            {
                if (item.Id>0)
                {
                    lokasi.Add(getDataCache() + item.Name+"/");
                    //System.Windows.Forms.MessageBox.Show(item.Name);
                }
            }

            FileInfo file;
            foreach (string loc in lokasi)
            {
                file = new FileInfo(loc);
                file.Directory.Create(); // If the directory already exists, this method does nothing.                
            }
        }
        //     StopWords       ===================================
        public static List<string> getStopWordList()
        {
            List<string> sw=null;
            try
            {
                if (File.Exists(@getStopWordLocation()))
                {
                    sw = new List<string>();
                    foreach (var item in File.ReadAllLines(getStopWordLocation()).Where(x=>string.IsNullOrWhiteSpace(x)==false))
                    {
                        sw.Add(item.ToLower());
                    }
                    return sw;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //      Lambda          ===================================
        public static double getLambda(int x)
        {
            if (x > 0)
            {
                return 0.6;
            }
            return 0.4;
        }

        // Directory Location   ===================================
        public static string getEnvirontment()
        {
            return @"C:\Chatbot\Environtment\";
        }
        public static string getKnowledgeBase()
        {
            return @"C:\Chatbot\KnowledgeBase\";
        }
        public static string getDataBaru()
        {
            return @"C:\Chatbot\KnowledgeBase\New Data\";
        }
        public static string getDataCache()
        {
            return @"C:\Chatbot\KnowledgeBase\Cache\";
        }
        public static string getDataCacheKata()
        {
            return getDataCache() + "CacheKata/";
        }

        // File Location        ===================================
        public static string getInvertedIndexLocation()
        {
            return getEnvirontment() + "InvertedIndex.xml";
        }
        public static string getStopWordLocation()
        {
            return getEnvirontment() + "Stopwords.txt";
        }

        // Load Save Term / Inverted Index;
        public static List<Term> LoadInvertedIndex()
        {
            TextReader reader=null;
            try
            {
                List<Term> lst = new List<Term>();
                XmlSerializer des = new XmlSerializer(typeof(List<Term>));
                reader = new StreamReader(getInvertedIndexLocation());
                object a = des.Deserialize(reader);
                des = null;
                reader.Close();
                reader = null;
                des = null;
                return (a as List<Term>);
            }
            catch (Exception e)
            {
                if (reader!=null)
                {
                    reader.Close();
                    reader = null;
                }
                System.Windows.Forms.MessageBox.Show("LoadError : " + e.Message);
                return null;
            }
        }
        public static void SaveInvertedIndex(List<Term> TermCollection)
        {
            List<Term> kataTanya = new List<Term>();
            kataTanya.Add(new Term("apa")); //What
            kataTanya.Add(new Term("apakah"));
            kataTanya.Add(new Term("siapa")); //Who
            kataTanya.Add(new Term("siapakah"));
            kataTanya.Add(new Term("dimana")); //Where
            kataTanya.Add(new Term("dimanakah"));
            kataTanya.Add(new Term("kenapa")); //Why
            kataTanya.Add(new Term("kenapakah"));
            kataTanya.Add(new Term("kapan")); //When
            kataTanya.Add(new Term("kapankah"));
            kataTanya.Add(new Term("bagaimana")); //how
            kataTanya.Add(new Term("bagaimanakah"));
            kataTanya.Add(new Term("berapa")); //how (number)
            kataTanya.Add(new Term("berapakah"));
            try
            {
                foreach (var item in kataTanya)
                {
                    Term y=TermCollection.Where(x=>x.Word.ToLower()==item.Word.ToLower()).FirstOrDefault();
                    if (y==null)
                    {
                        y= new Term(item.Word.ToLower());
                        TermCollection.Add(y);
                    }
                    y.StopWord = true;
                }
                XmlSerializer ser = new XmlSerializer(typeof(List<Term>));
                TextWriter wrt = new StreamWriter(getInvertedIndexLocation());
                List<Term> Index = TermCollection.OrderBy(x=>x.Word).ToList();
                ser.Serialize(wrt, Index);
                wrt.Close();
                wrt = null;
                ser = null;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Save Gagal " + e.Message);
            }
        }


        #endregion
    }
}
