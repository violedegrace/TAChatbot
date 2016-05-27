using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    class DialogueManager
    {
        //cek Topik
        //cari informasi dengan MLM
        //penentuan jawaban dengan pemilihan jawaban
        //pembuatan jawaban
        tbUser Bot;

        public Stack<Dialogue> Conversation;
        dbDataContext db;
        List<tbState> ListOfState;
        List<tbStatePrg> StateMovement;
        
        tbDomain CurrentDomain = null;
        public tbState CurrentState = null;
        Term CurrentTopic = null;
        List<Term> subTopik = null;
        Term newTopic = null;
        List<Term> ExtraInfo = null;
        public tbInfDetail[] LastPossibleAnswer = null;

        string message = "";
        bool selesai = false;
        bool initiate = true;

        public DialogueManager(dbDataContext database,tbUser bot)
        {
            Conversation = new Stack<Dialogue>();
            db = database;
            Bot = bot;
            ListOfState = db.tbStates.ToList();
            StateMovement = db.tbStatePrgs.ToList();
            bool selesai = false;
            bool initiate = true;
        }
        public void ManageDialogue(tbUser usr, string kalimat, EngineActuator act)
        {
            bool masuk = false;
            //inisialisasi
            Random rnd = new Random(DateTime.Now.Millisecond);
            tbLogDetail[] QAStd = null;
            Dialogue I = null, O = null; //Periapan I = Input, O = output;
            try
            {
                //Creating input
                I = new Dialogue(usr, kalimat,(CurrentState==null)?0:CurrentState.Id);
                // inisialisasi program
                if (Conversation.Count < 1)
                {
                    //inisialisasi topik, topik dikosongkan.
                    CurrentTopic = null;
                    //pindah ke 1
                    CurrentState = ListOfState.Where(x => x.Name.ToLower() == "pembukaan").FirstOrDefault();
                }
                else
                {
                    if (I.isQuestion() == false) // apakah masukan berupa pertanyaan?
                    {
                        // jika bukan maka sistem memberikan pengarahan
                        if (CurrentState.Id==3)
                        {
                            if (Conversation.Peek().Str.ToLower().Contains("apakah ingin merubah topik??"))
                            {
                                string jawaban = "";
                                if (I.Str.ToLower().Equals("ya"))
                                {
                                    subTopik=null;
                                    int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                    CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                                    message="Topik percakapan akan diubah.";
                                    masuk = true;
                                }
                                else if (I.Str.ToLower().Equals("tidak"))
                                {
                                    newTopic = null;
                                    jawaban += "Topik percakapan akan diarahkan ke " + CurrentTopic.Word + ".";
                                }
                                else
                                {
                                    newTopic = null;
                                    jawaban += "Masukan tidak dikenali. kembali ke topik sebelumnya";
                                }
                                if (jawaban!="")
                                {
                                    O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                }
                            }
                            if (Conversation.Peek().Str.ToLower().Contains("ditanyakan"))
                            {
                                if (I.Str.ToLower().Contains("tidak"))
                                {
                                    int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                    CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                                    masuk = true;
                                }
                                else if (I.Str.ToLower().Contains("ada"))
                                {
                                    string jawaban = "Silahkan masukkan pertanyaan.";
                                    O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                }
                            }
                        }
                        if (Conversation.Peek().State == 5)
                        {
                            QAStd = db.tbLogDetails.Where(x => x.LogID == 1 && string.Compare(x.Question.ToString().ToLower(), I.Str.ToLower()) == 0).ToArray();
                            if (QAStd.Count() > 0)
                            {
                                CurrentState = ListOfState.Where(x => x.Id == 5).FirstOrDefault();
                                selesai = true;
                            }
                        }
                    }
                    if (I.isQuestion() == true || masuk || initiate || selesai)
                    {
                        //jika sebuah pertanyaan, sistem akan menjawab pertanyaan.
                        // State Pembukaan || id=1
                        if (CurrentState.Id == 1 || CurrentState.Id == 6) // cek input bisa dibalas
                            QAStd = db.tbLogDetails.Where(x => x.LogID == 1 && string.Compare(x.Question.ToString().ToLower(), I.Str.ToLower()) == 0).ToArray();
                        if (CurrentState.Id == 1 && QAStd.Count() > 0)
                        {
                            // membalas sapaan
                            if (QAStd.Count() > 1)
                                O = new Dialogue(Bot, QAStd[rnd.Next(0, QAStd.Count())].Answer, CurrentState.Id);
                            else
                                O = new Dialogue(Bot, QAStd.First().Answer, CurrentState.Id);
                        }
                        else if (CurrentState.Id == 1)
                        {
                            //pindah ke 2
                            int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                            CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                            initiate = false;
                        }
                        // State Inisialiasi Topik || id=2
                        if (CurrentState.Id == 2) //cek state
                        {
                            //Inisialisasi percakapan 
                            I.State = CurrentState.Id;
                            if (CurrentTopic == null)
                            {
                                //cek domain
                                int dom = DomainDetection(I);
                                CurrentDomain = db.tbDomains.Where(x => x.Id == dom).FirstOrDefault();
                                //cek Topik
                                CurrentTopic = TopicDetection(I.StringToTerm(), CurrentDomain.Id);
                                //cari informasi dengan MLM
                                LastPossibleAnswer = act.PencarianInformasi(I, CurrentDomain.Id, null);
                                //penentuan jawaban dengan pemilihan jawaban
                                string jawaban = Pilihjawaban(I, LastPossibleAnswer.First());
                                //pembuatan jawaban                            
                                O = new Dialogue(Bot, jawaban, CurrentState.Id);
                            }
                            else
                            {
                                //pindah ke 3
                                int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                            }
                        }
                        // State Pembahasan Topik || id=3
                        if (CurrentState.Id == 3)
                        {
                            //tetap di 3
                            //cek domain
                            int dom = DomainDetection(I);
                            //cek Topik
                            newTopic = TopicDetection(I.StringToTerm(), CurrentDomain.Id);
                            if (dom != CurrentDomain.Id)
                            {
                                CurrentState = ListOfState.Where(x => x.Id == 4).FirstOrDefault();
                                message = "Masukan berbeda Domain. tidak ada hasil.";
                            }
                            else
                            {
                                //cek hubungan topik lama dan baru;
                                if (CurrentTopic.Word == newTopic.Word)
                                {
                                    //cari informasi dengan MLM
                                    LastPossibleAnswer = act.PencarianInformasi(I, CurrentDomain.Id, null);
                                    //penentuan jawaban dengan pemilihan jawaban
                                    string jawaban = Pilihjawaban(I, LastPossibleAnswer.First());
                                    //pembuatan jawaban                            
                                    O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                }
                                else
                                {
                                    //cek keterkaitan
                                    bool Infdetil = CurrentTopic.Index.Select(x => x.InfDetilID).ToList()
                                        .Intersect(newTopic.Index.Select(y => y.InfDetilID)).ToList().Count() > 0;
                                    if (Infdetil) //topic related
                                    {
                                        //Membuat subtopik
                                        if (subTopik == null)
                                            subTopik = new List<Term>();
                                        subTopik.Add(newTopic);

                                        //membuat informasi tambahan yang diperlukan untuk pencarian
                                        if (ExtraInfo == null)
                                            ExtraInfo = new List<Term>();
                                        ExtraInfo.Add(CurrentTopic);
                                        ExtraInfo.Add(newTopic);

                                        //cari informasi dengan MLM
                                        LastPossibleAnswer = act.PencarianInformasi(I, CurrentDomain.Id, ExtraInfo);
                                        //penentuan jawaban dengan pemilihan jawaban
                                        string jawaban = Pilihjawaban(I, LastPossibleAnswer.First());
                                        //pembuatan jawaban                            
                                        O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                    }
                                    else
                                    {
                                        if (subTopik==null || subTopik.Count < 1)
                                        {
                                            //new topic
                                            string jawaban = "Topik " + newTopic.Word + " tidak ada hubungan dengan topik " + CurrentTopic.Word + ". ";
                                            jawaban += "Apakah ingin merubah topik??";// (jawab dengan 'ya','tidak' atau 'mungkin')";
                                            O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                        }
                                        else
                                        {
                                            //pindah ke 4
                                            int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                            CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                                        }
                                    }
                                }
                            }
                            //                        System.Windows.Forms.MessageBox.Show(CurrentTopic.Word);
                        }
                        // State Pembahasan SubTopik || id=4
                        if (CurrentState.Id == 4)
                        {
                            if (CurrentTopic.Word!=newTopic.Word)
                            {
                                if (subTopik!=null && subTopik.Count>0)
                                {
                                    //tetap di 4, dan kembali ke 3 || kembali ke 3 karena state 4 hanya mengambil topik untuk dibahas
                                    CurrentTopic = subTopik.First();
                                    subTopik.Remove(CurrentTopic);
                                    int prevState = Convert.ToInt32(StateMovement.Where(x => x.NextStateID == CurrentState.Id).Select(x => x.StateID).First());
                                    CurrentState = ListOfState.Where(x => x.Id == prevState).FirstOrDefault();
                                    //penentuan jawaban dengan pemilihan jawaban
                                    string jawaban = "Masih ada topik yang dapat dibahas yaitu mengenai "+CurrentTopic.Word;
                                    jawaban += ". Ada yang ingin ditanyakan mengenai " + CurrentTopic.Word + "?";
                                    //pembuatan jawaban                            
                                    O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                }
                                else
                                {
                                    //pindah ke 5
                                    int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                    CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                                }
                            }
                        }
                        // State Pengalihan topik || id=5
                        if (CurrentState.Id == 5)
                        {
                            if (selesai == true) // lanjut ke 6
                            {
                                //penutup --> pindah ke 6
                                int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).Last());
                                CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                            }
                            else if (masuk == true || (subTopik != null && subTopik.Count() < 1))
                            {
                                //pindah ke 2
                                //pengarahan percakapan kembali ke state 2 untuk emulai topik baru
                                masuk = false;
                                string jawaban = message+" Masukkan yang ingin dibicarakan.";
                                message = "";
                                CurrentTopic = null;
                                O=new Dialogue(Bot,jawaban,CurrentState.Id);
                                int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                            }
                            else if (subTopik == null) // kembali ke 2
                            {
                                string jawaban = "Masukan tidak ada di domain ini dan tidak ada subtopik untuk dibahas. Masukkan hal ingin dibicarakan";
                                O = new Dialogue(Bot, jawaban, CurrentState.Id);
                                int nextState = Convert.ToInt32(StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x => x.NextStateID).First());
                                CurrentState = ListOfState.Where(x => x.Id == nextState).FirstOrDefault();
                            }
                            else 
                            {
                                //fail-safe
                                string jawaban = "Proses pengalihan";
                                O = new Dialogue(Bot, jawaban, CurrentState.Id);
                            }

                        }
                        if (CurrentState.Id == 6)
                        {
                            // membalas sapaan
                            if (QAStd.Count() > 1)
                                O = new Dialogue(Bot, QAStd[rnd.Next(0, QAStd.Count())].Answer, CurrentState.Id);
                            else
                                O = new Dialogue(Bot, QAStd.First().Answer, CurrentState.Id);

                            // State penutup || id=6
                            CurrentState = new tbState();
                            //tutup percakapan.
                            CurrentState.Id = 7;
                            //tutup
                        }

                    }
                }
            }
            catch (Exception e)
            {
                O = new Dialogue(Bot,"Maaf terjadi kesalah sistem. report id : "+CurrentState.Id);
                System.Windows.Forms.MessageBox.Show(e.StackTrace);
            }
            finally
            {
                Conversation.Push(I);
                if (O != null) {
                    Conversation.Push(O);
                }
            }
            // Kirim Pesan ke utama

        }
        private int DomainDetection(Dialogue input)
        {
            List<double> dom = new List<double>();
            for (int i = 0; i < db.tbDomains.Count(); i++)
                dom.Add(0);

            foreach (var item in input.StringToTerm())
            {
                if (item != null)
                {
                    for (int i = 0; i < item.Bobot.Count; i++)
                        dom[i] += item.Bobot[i];
                }
            }
            return dom.LastIndexOf(dom.Max());
        }
        private Term TopicDetection(List<Term> lst,int dom)
        {
            try
            {
                int maxValue = 0;
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[maxValue].Bobot[dom] < lst[i].Bobot[dom])
                    {
                        maxValue = i;
                    }
                }
                return lst[maxValue];
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string Pilihjawaban(Dialogue I,tbInfDetail data)
        {
            List<Term> masukan = I.StringToTerm("all");
            //Konjungsi "jika", "ketika", "tetapi", "seandainya", "supaya", "walaupun", "seperti", "karena",
            //          "sehingga", "bahwa", "dan", "atau", "adalah", "ataupun"
            if (data.Penghubung!=null)
            {
                if (masukan[0].Word.Equals("apa") || masukan[0].Word.Equals("apakah")) // apa apakah
                {
                    if (data.Penghubung.ToLower().Equals("jika") || data.Penghubung.ToLower().Equals("ketika"))
                        return data.Awal;
                    else if (data.Penghubung.ToLower().Equals("adalah"))
                        return data.Akhir;
                    else
                        return data.info;
                }
                else if (masukan[0].Word.Equals("kapan") || masukan[0].Word.Equals("kapankah")) //kapan
                {
                    if (data.Awal.Contains(" jam ") || data.Awal.Contains(" pada ") || data.Awal.Contains(" sewaktu "))
                        return data.Awal;
                    else if (data.Penghubung.ToLower().Equals("ketika") || data.Penghubung.ToLower().Equals("jika") ||
                        data.Akhir.Contains(" jam ") || data.Akhir.Contains(" pada ") || data.Akhir.Contains(" sewaktu "))
                        return data.Akhir;
                    else
                        return data.info;
                }
                else if (masukan[0].Word.Equals("siapa") || masukan[0].Word.Equals("siapakah")) //siapa 
                {
                    if (data.Penghubung.ToLower().Equals("ketika") || data.Penghubung.ToLower().Equals("adalah"))
                        return data.Awal;
                    else if (false)
                        return data.Akhir;
                    else
                        return data.info;
                }
                else if (masukan[0].Word.Equals("bagaimana") || masukan[0].Word.Equals("bagaimanakah")) //bagaimana
                {
                    if (data.Penghubung.ToLower().Equals("jika"))
                        return data.Awal;
                    else if (data.Penghubung.ToLower().Equals("adalah"))
                        return data.Akhir;
                    else
                        return data.info;
                }
                else if (masukan[0].Word.Equals("kenapa") || masukan[0].Word.Equals("mengapa")) //kenapa 
                {
                    if (data.Penghubung.ToLower().Equals("jika"))
                        return data.Awal;
                    else if (false)
                        return data.Akhir;
                    else
                        return data.info;
                }
                else if (masukan[0].Word.Equals("dimana") || masukan[0].Word.Equals("dimanakah")) //dimana
                {
                    if (data.Awal.Contains(" di ") || data.Awal.Contains(" ke ") || data.Awal.Contains(" di"))
                    {
                        return data.Awal;
                    }
                    else
                    {
                        return data.info;
                    }
                }
                else if (masukan[0].Word.Equals("berapa") || masukan[0].Word.Equals("berapakah")) //berapa
                {
                    int y;
                    if (data.Awal.Split(' ').ToList().Where(x => int.TryParse(x, out y) == true).FirstOrDefault() != null)
                    {
                        return data.Awal;
                    }
                    else if (data.Akhir.Split(' ').ToList().Where(x => int.TryParse(x, out y) == true).FirstOrDefault() != null)
                    {
                        return data.Akhir;
                    }
                    return data.info;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(masukan[0].Word);
                    if (data.Penghubung.ToLower().Equals("jika"))
                        return data.Awal;
                    else if (data.Penghubung.ToLower().Equals("adalah"))
                        return data.Akhir;
                    else
                        return data.info;
                }                
            }
            else
            {
                return data.info;
            }
            return null;
        }
    }

    //public void ManageDialogue(tbUser usr, string kalimat, EngineActuator act)
    //{
    //    Random rand = new Random(DateTime.Now.Millisecond);
    //    tbLogDetail[] Pembukaan = null; // kumpulan aksi dan reaksi yang sudah disediakan oleh admin untuk state pembukaan
    //    if (Conversation.Count < 1) //Inisialisasi Program
    //    {
    //        Conversation.Push(new Dialogue(usr, kalimat, ListOfState[0].Id));
    //        CurrentState = ListOfState[0];
    //    }
    //    else
    //    {
    //        //sudah mendapat masukan dari pengguna
    //        Dialogue input = new Dialogue(usr, kalimat);
    //        Dialogue output = null;
    //        try
    //        {
    //            //cek masukan terlalu panjang
    //            if (input.Str.Length > 999)
    //            {
    //                throw new IndexOutOfRangeException();
    //            }
    //            //cek apakah dalam state pembukaan (Menangani loop pada state pembukaan)
    //            if (CurrentState.Name == ListOfState.Where(x => x.Name.ToLower() == "pembukaan").Select(x => x.Name).FirstOrDefault())
    //                Pembukaan = db.tbLogDetails.Where(x => string.Compare(x.Question.ToLower(), input.Str.ToLower()) == 0).ToArray();                    
    //            if (CurrentState.Name == ListOfState.Where(x => x.Name.ToLower() == "pembukaan").Select(x => x.Name).FirstOrDefault()
    //                && Pembukaan.Count()>0)
    //            {
    //                //State Pembukaan
    //                if (Pembukaan.Count() > 0)
    //                {
    //                    if (Pembukaan.Count() < 2)
    //                        output=(new Dialogue(Bot, Pembukaan[0].Answer, CurrentState.Id));
    //                    else
    //                        output=(new Dialogue(Bot, Pembukaan[rand.Next(0, Pembukaan.Count())].Answer, CurrentState.Id));
    //                }
    //                else
    //                {
    //                    output = (new Dialogue(Bot, "Masukan tidak dikenali, hanya menangani masukan dalam bentuk formal", CurrentState.Id));
    //                }
    //            }
    //            else if (CurrentState.Name == ListOfState.Where(x => x.Name.ToLower() == "pembukaan").Select(x => x.Name).FirstOrDefault()
    //                && Pembukaan.Count()<1)
    //            {
    //                //State inisialisasi Topik..
    //                int CurStateId = (int)StateMovement.Where(x => x.StateID == CurrentState.Id).Select(x=>x.NextStateID).FirstOrDefault();
    //                CurrentState = ListOfState.Where(x => x.Id == CurStateId).FirstOrDefault();
    //                //topic detection
    //                    //domain detection
    //                int domIdx = DomainDetection(input);
    //                    //topic detection dengan value terbesar berdasarkan domain
    //                if (CurrentTopic==null)
    //                    CurrentTopic = TopicDetection(input.StringToTerm(), domIdx);
    //                else
    //                {
    //                    Term topic = TopicDetection(input.StringToTerm(), domIdx);
    //                    if (CurrentTopic.Bobot[domIdx] < topic.Bobot[domIdx])
    //                    {
    //                        CurrentTopic = topic;
    //                    }
    //                }
    //                //pencarian informasi dengan MLM
    //                List<tbInfDetail> ListJawaban = act.PencarianInformasi(input, domIdx).ToList();
    //                //penentuan jawaban
    //                //pemilihan bagian jawaban
    //                //pembuatan jawaban
    //                if (ListJawaban!=null && ListJawaban.Count>0)
    //                {
    //                    output = new Dialogue(Bot, ListJawaban.First().info, CurrentState.Id);
    //                }
    //                else
    //                {
    //                    output = new Dialogue(Bot,"Tidak ada data mengenai pertanyaan", CurrentState.Id);
    //                }                        
    //            }
    //            else
    //            {
    //                System.Windows.Forms.MessageBox.Show("Tidak Masuk Manapun");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            if (e is IndexOutOfRangeException)
    //            {
    //                output = (new Dialogue(Bot, "Masukan terlalu panjang", CurrentState.Id));
    //            }
    //            else
    //            {
    //                System.Windows.Forms.MessageBox.Show("Test exception "+e.Message);
    //            }
    //        }
    //        finally
    //        {
    //            Conversation.Push(input);
    //            Conversation.Push(output);
    //        }
    //    }
    //}
}
