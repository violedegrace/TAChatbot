using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    public class EngineActuator
    {
        IEngine model;
        List<string> locations;
        dbDataContext db;
        List<tbInformasi> tobeIndexed;
        public EngineActuator(string args, dbDataContext database)
        {
            db = database;
            model = new EngineMixtureLanguageModel(db);
        }

        public void RebuildDatabase()
        {
            if (model==null)
            {
                model = new EngineMixtureLanguageModel(new dbDataContext());
            }
            Indexing("all");
        }

        public void Indexing(string arg)
        {
            //args terdiri dari all atau indexed
            model.dataIndexing(arg);
        }
        public void FileCrawler(string args)
        {
            //Crawler
            List<string> LokasiFile = new List<string>();
            //menyimpan daftar folder yang akan di crawl
            Queue<string> ListLocation = new Queue<string>();
            //menimpan daftar file yang akan di index
            List<string> FileLocation = new List<string>();

            string process; // variabel pemrosesan
            string location;
            if (args=="cache")
            {
                location = Lingkungan.getDataCache(); // menyimpan lokasi cache yang akan dicrawl
            }
            else
            {
                location = Lingkungan.getDataBaru(); // menyimpan lokasi Data baru yang akan dicrawl
            }
            if ((File.Exists(location) || Directory.Exists(location)) && string.IsNullOrWhiteSpace(location) == false)
            {
                ListLocation.Enqueue(location);
                do
                {
                    process = ListLocation.Dequeue();
                    if (File.GetAttributes(process) == FileAttributes.Directory)
                    {
                        foreach (string item in Directory.GetDirectories(process))
                            ListLocation.Enqueue(item);
                        foreach (string item2 in Directory.GetFiles(process))
                            FileLocation.Add(item2);
                    }
                } while (ListLocation.Count > 0);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Gagal Melakukan Crawling");
            }
            locations = FileLocation;
        }
    }
}
