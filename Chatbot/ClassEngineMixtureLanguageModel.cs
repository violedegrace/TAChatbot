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
            else
            {
                InfoList = db.tbInformasis.Where(x => x.Indexed == 0).ToList();
            }
            for (int i = 0; i < InfoList.Count; i++)
            {
                if (File.Exists(InfoList[i].Lokasi) && InfoList[i].Indexed==0)
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
                            createInvertedIndex(Fragment[j], InfoList[i].DomainID, InfoList[i].Id, j);
                        }
                    }

                    File.Move(InfoList[i].Lokasi, Lingkungan.getDataCache() + InfoList[i].tbDomain.Name + "\\" + InfoList[i].Judul);
                    InfoList[i].Lokasi = Lingkungan.getDataCache() + InfoList[i].tbDomain.Name + "\\" + InfoList[i].Judul;
                }
            }
            db.SubmitChanges();

        }

        private tbInfDetail CreateDataDetil(string data)
        {
            tbInfDetail baru = new tbInfDetail();
            baru.info = data;
            //cari penghubung, awal dan akhir
            return baru;
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
            }
            Lingkungan.SaveInvertedIndex(InvertedIndex);
            //hitung pembobotan
        }
        public void HitungPembobotanKata()
        {
            List<Term> InvertedIndex = Lingkungan.LoadInvertedIndex();
            if (db==null)
                db=new dbDataContext();
            List<tbDomain> Domain = db.tbDomains.ToList(); // seluruh domain yang ada
            List<int> DomainCount = new List<int>(); // Counter setiap domain
            for (int i = 0; i < Domain.Count; i++) // inisialisasi counter domain
                DomainCount.Add(0);

            // Hitung jumlah kata dalam seluruh dokumen dan perdomain
            int TermCount = 0; //counter kata
            foreach (var item in InvertedIndex)
            {
                TermCount += item.Index.Count;
                for (int i = 0; i < Domain.Count; i++)
			    {
			        DomainCount[i]+=item.Index.Where(x=>x.DomainID==Domain[i].Id).Count();
			    }
            }

            //pembobotan
            foreach (var item in InvertedIndex)
            {
                item.Bobot.Clear();
                for (int i = 0; i < Domain.Count; i++)
                {
                    int count = item.Index.Where(x => x.DomainID == Domain[i].Id).Count();
                    double bobot = Math.Log10((Lingkungan.getLambda(0) * (count / TermCount)) +
                        (Lingkungan.getLambda(1) * count / DomainCount[i]));
                    item.Bobot.Add(bobot);
                }
            }

            Lingkungan.SaveInvertedIndex(InvertedIndex);
        }
    }
}
