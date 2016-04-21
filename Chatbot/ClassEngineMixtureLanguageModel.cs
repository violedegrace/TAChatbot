using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chatbot
{
    public class EngineMixtureLanguageModel : IEngine
    {
        public dbDataContext db;
        public EngineMixtureLanguageModel(dbDataContext database)
        {
            if (database != null)
                db = database;
            else
                db = new dbDataContext();
        }
        public void dataIndexing(string args)
        {
            List<tbInformasi> InfoList = null;// db.tbInformasis.ToList();
            int prev=0;
            tbInfDetail datadetil=null;

            
            if (args.ToLower().Equals("all"))
            {
                InfoList = db.tbInformasis.ToList();
            }
            for (int i = 0; i < InfoList.Count; i++)
            {
                if (File.Exists(InfoList[i].Lokasi))
                {
                    InfoList[i].Indexed = 1;
                    string text = File.ReadAllText(InfoList[i].Lokasi);
                    string[] Fragment = text.Split('.').ToArray();
                    for (int j = 0; j < Fragment.Length; j++)
                    {
                        if (!String.IsNullOrWhiteSpace(Fragment[j]))
                        {
                            datadetil = CreateDataDetil(Fragment[j]);
                            if (datadetil != null)
                                InfoList[i].tbInfDetails.Add(datadetil);
                            //createInvertedIndex(string data,int domain,int infID, int InfDetID)
                            createInvertedIndex(Fragment[j], InfoList[i].DomainID, InfoList[i].Id, j);
                        }
                    }
                }
            }
            db.SubmitChanges();
            System.Windows.Forms.MessageBox.Show("Indexing Selesai");

        }

        private tbInfDetail CreateDataDetil(string data)
        {
            try
            {
                tbInfDetail baru =db.tbInfDetails.Where(x=>x.info.ToLower()==data.ToLower()).FirstOrDefault();
                if (baru == null)
                    baru = new tbInfDetail();
                baru.info = data;
                //cari penghubung, awal dan akhir
                return baru;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void createInvertedIndex(string data,int domain,int infID, int InfDetID)
        {
            List<string> words = Regex.Split(data, @"[^A-Za-z0-9]").Where(i => i != string.Empty).ToList();
            List<Term> InvertedIndex = Lingkungan.LoadInvertedIndex();
            if (InvertedIndex == null)
                InvertedIndex = new List<Term>();

            Term kata = null;
            for (int i = 0; i < words.Count; i++)
            {
                kata = InvertedIndex.Where(x => x.Word.ToLower().Equals(words[i].ToLower())).FirstOrDefault();
                if (kata == null){
                    kata = new Term();
                    kata.Word = words[i];
                    InvertedIndex.Add(kata);
                }
                kata.Index.Add(new Location(domain, infID, InfDetID, i));
                Lingkungan.SaveInvertedIndex(InvertedIndex);
            }

            Lingkungan.SaveInvertedIndex(InvertedIndex);
        }
    }
}
