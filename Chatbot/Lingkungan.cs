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
        // Directory Location   ===================================
        public static double getLambda(int x)
        {
            if (x > 1)
            {
                return 0.4;
            }
            return 0.6;
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

        // File Location        ===================================
        public static string getInvertedIndexLocation()
        {
            return getEnvirontment() + "InvertedIndex.xml";
        }
        public static string getStopWordLocation()
        {
            return getEnvirontment() + "Stopword.txt";
        }


        #endregion
    }
}
