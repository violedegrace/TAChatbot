using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chatbot
{
    public class EngineMixtureLanguageModel : IEngine
    {
        public dbDataContext db;
        private bool InetCon=false;
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
            tbInfDetail datadetil=null;
            if (CheckInternetConnectionByPing() && CheckInternetConnectionbyWebPage())
                InetCon = true;
            if (args.ToLower().Equals("all"))
                InfoList = db.tbInformasis.ToList();
            else
                InfoList = db.tbInformasis.Where(x => x.Indexed == 0).ToList();

            for (int i = 0; i < InfoList.Count; i++)
            {
                if (File.Exists(InfoList[i].Lokasi))// && InfoList[i].Indexed==0)
                {
                    InfoList[i].Indexed = 1;
                    string text = File.ReadAllText(InfoList[i].Lokasi);
                    string[] Fragment = text.Split('.').ToArray();
                    for (int j = 0; j < Fragment.Length; j++)
                    {
                        if (!String.IsNullOrWhiteSpace(Fragment[j]))
                        {
                            datadetil = CreateDataDetil(Fragment[j]);
                            if (datadetil != null && db.tbInfDetails.Where(x=>x.info.ToString()==datadetil.info.ToString()).FirstOrDefault()==null)
                                InfoList[i].tbInfDetails.Add(datadetil);
                            createInvertedIndex(Fragment[j], InfoList[i].DomainID, InfoList[i].Id, j);
                        }
                    }

                    File.Move(InfoList[i].Lokasi, Lingkungan.getDataCache() + InfoList[i].tbDomain.Name + "\\" + InfoList[i].Judul);
                    InfoList[i].Lokasi = Lingkungan.getDataCache() + InfoList[i].tbDomain.Name + "\\" + InfoList[i].Judul;
                }
            }
            if (File.Exists(Lingkungan.getInvertedIndexLocation()))
            {
                HitungPembobotanKata();
            }
            InetCon = false;
            db.SubmitChanges();

        }
        private tbInfDetail CreateDataDetil(string input)
        {
            tbInfDetail baru = new tbInfDetail();
            baru.info = input;
            //cari penghubung, awal dan akhir
            try
            {
                string[] konjungsi = { "jika", "ketika", "tetapi", "seandainya", "supaya", "walaupun", "seperti", "karena", "sehingga", "bahwa", "dan", "atau", "adalah", "ataupun" };
                string separator = "";
                foreach (string item in konjungsi)
                {
                    if (input.ToLower().Contains(item) == true)
                    {
                        separator = item;
                        break;
                    }
                }
                if (separator != "")
                {
                    baru.Awal=(input.Substring(0, input.IndexOf(separator)));
                    baru.Akhir=(input.Substring(input.IndexOf(separator) + separator.Length + 1, input.Length - (input.IndexOf(separator) + separator.Length + 1)));
                    baru.Penghubung=(separator);
                }
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
            List<string> stopwords = Lingkungan.getStopWordList();

            List<Term> InvertedIndex = Lingkungan.LoadInvertedIndex();
            if (InvertedIndex == null)
                InvertedIndex = new List<Term>();

            Term kata = null;
            for (int i = 0; i < words.Count; i++)
            {
                kata = InvertedIndex.Where(x => x.Word.ToLower().Equals(words[i].ToLower())).FirstOrDefault();
                if (kata == null){
                    kata = new Term();
                    kata.Word = words[i].ToLower();
                    kata.Jenis = JenisKata.Unknown;
                    InvertedIndex.Add(kata);
                }
                if (kata.Jenis == JenisKata.Unknown)
                {
                    if (InetCon)
                        ScraptDataFromWebKBBI(kata.Word);
                    if (File.Exists(Lingkungan.getDataCacheKata() + kata.Word + ".html"))
                    {
                        kata.Jenis = GetJenisKataFromScraptFile(kata.Word);
                    }                    
                }

                if (stopwords!=null && stopwords.Where(x=>x.ToLower().Equals(words[i].ToLower())).Count()>0)
                {
                    kata.StopWord = true;
                }
                else
                {
                    kata.StopWord = false;
                }
                kata.Index.Add(new Location(domain, infID, InfDetID, i));
            }
            Lingkungan.SaveInvertedIndex(InvertedIndex);
            //hitung pembobotan
        }
        public void HitungPembobotanKata()
        {
            List<Term> InvertedIndex = Lingkungan.LoadInvertedIndex();
            if (InvertedIndex==null)
                InvertedIndex = new List<Term>();
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

            for (int i = 1; i < DomainCount.Count; i++)
            {
                if (DomainCount[i]==0)
                {
                    for (int j = 1; j < DomainCount.Count; j++)
                    {
                        DomainCount[i]++;
                    }
                    break;
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
        public tbInfDetail[] PencarianInformasi(Dialogue inpt, int domain,List<Term> extra)
        {
            List<tbInfDetail> dataDomain = db.tbInfDetails.Where(x=>x.tbInformasi.DomainID==domain).ToList();
            List<decimal> bobot = new List<decimal>();
            List<Term> qry = inpt.StringToTerm();
            if (extra != null)
            {
                foreach (var item in extra)
                {
                    if (qry.Where(x=>x.Word==item.Word).FirstOrDefault()==null)
                    {
                        qry.Add(item);
                    }
                }
            }
            int TermCount = 0;
            foreach (var item in Lingkungan.LoadInvertedIndex())
                TermCount += item.Index.Count;
            decimal termC = (decimal)TermCount;

            decimal b;
            for (int i = 0; i < dataDomain.Count; i++)
            {
                string[] fragment = dataDomain[i].info.Split(' ').Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();
                b = 1;
                foreach (var item in qry)
                {
                    decimal FragmentTermcounter = Convert.ToDecimal(fragment.Where(x => x.ToLower().Contains(item.Word.ToLower())).Count());
                    decimal FragmentCounter = Convert.ToDecimal(fragment.Count());
                    decimal itemCounter = Convert.ToDecimal(item.Bobot.Count);
                    b = b * ((FragmentTermcounter / FragmentCounter) * Convert.ToDecimal(Lingkungan.getLambda(1)) +
                        (itemCounter / termC) * Convert.ToDecimal(Lingkungan.getLambda(0)));
                }
                bobot.Add(b);
            }

            List<tbInfDetail> top10 = new List<tbInfDetail>();
            while (dataDomain.Count > 0 && top10.Count < 10)
            {
                int idx = bobot.LastIndexOf(bobot.Max());
                top10.Add(dataDomain[idx]);
                bobot.RemoveAt(idx);
                dataDomain.RemoveAt(idx);
            }
            return top10.ToArray();
        }

        public bool ScraptDataFromWebKBBI(string kata)
        {
            try
            {
                string url = "http://kbbi.web.id/" + kata;
                //                string cachePath = ImportantLocation.getWordScrapedCacheLocation();
                string cachePath = Lingkungan.getDataCacheKata();
                string Data = "";
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                if (Request != null && Response != null)
                {
                    Stream receiveStream = Response.GetResponseStream();
                    StreamReader ReaderStream = null;
                    if (Response.CharacterSet == null)
                        ReaderStream = new StreamReader(receiveStream);
                    else
                        ReaderStream = new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    Data = ReaderStream.ReadToEnd();
                    ReaderStream.Close();
                    System.IO.FileInfo file = new System.IO.FileInfo(cachePath + kata.ToLower() + ".html");
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    System.IO.File.WriteAllText(file.FullName, Data);
                }
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
        }

        public JenisKata GetJenisKataFromScraptFile(string kata)
        {
            JenisKata retur = JenisKata.Unknown;
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            string location = Lingkungan.getDataCacheKata() + kata.ToLower() + ".html";
            htmldoc.Load(location);
            List<string> toParse2 = new List<string>();
            try
            {
                foreach (HtmlNode node in htmldoc.DocumentNode.SelectNodes("//textarea[@id='jsdata']"))
                {
                    toParse2.AddRange(Regex.Split(node.ChildNodes[0].InnerHtml, @"[^A-Za-z0-9]").Where(i => i != string.Empty).ToList());
                }
                int x = 0;
                for (int i = 0; i < toParse2.Count - 1; i++)
                {
                    if (toParse2[i].ToLower().Equals(kata.ToLower()))
                    {
                        x = i;
                        break;
                    }
                }
                if (x > 0)
                {
                    for (int j = x; j < toParse2.Count - 2; j++)
                    {
                        if (toParse2[j - 2].ToLower().Equals("em") && toParse2[j - 2].ToLower().Equals("em"))
                        {
                            if (toParse2[j].ToLower().Equals("n"))
                            {
                                retur = JenisKata.Benda;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("v"))
                            {
                                retur = JenisKata.Kerja;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("a"))
                            {
                                retur = JenisKata.Sifat;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("pron"))
                            {
                                retur = JenisKata.Ganti;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("adv"))
                            {
                                retur = JenisKata.Keterangan;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("p"))
                            {
                                retur = JenisKata.Tugas;
                                break;
                            }
                            else if (toParse2[j].ToLower().Equals("num"))
                            {
                                retur = JenisKata.Bilangan;
                                break;
                            }
                            //else if (toParse2[j].ToLower().Equals("aa"))
                            //{
                            //    retur = JenisKata.Ganti;
                            //    break;
                            //}
                        }
                    }
                }
                return retur;
            }
            catch (Exception)
            {
                return retur;
            }
        }

        public static bool CheckInternetConnectionbyWebPage()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public bool CheckInternetConnectionByPing()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
