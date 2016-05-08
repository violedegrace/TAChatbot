using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chatbot
{
    public class Dialogue
    {
        private tbUser _usr;
        public tbUser Usr
        {
            get { return _usr; }
            set { _usr = value; }
        }

        private string _str;
        public string Str
        {
            get { return _str; }
            set { _str = value; }
        }

        private int state;
        public int State
        {
            get { return state; }
            set { state = value; }
        }
        public Dialogue()
        {

        }
        public Dialogue(tbUser us, string str)
        {
            Usr = us;
            Str = str;
        }
        public Dialogue(tbUser usr, string str,int s):this(usr,str)
        {
            state = s;
        }

        public override string ToString()
        {
            return "["+state+"]"+Usr.Name + " : " + Str;
        }
        public bool isQuestion()
        {
            if (Str.Contains('?'))
                return true;
            return false;
        }
        public List<Term> StringToTerm()
        {
            List<Term> data = new List<Term>();
            List<Term> idx = Lingkungan.LoadInvertedIndex();
            foreach (string item in Regex.Split(Str, @"[^A-Za-z0-9]").Where(i => i != string.Empty).ToList())
            {
                Term x = idx.Where(i=>i.Word.ToLower()==item.ToLower()).FirstOrDefault();
                if (x!=null && x.StopWord==false)
                {
                    data.Add(x);   
                }
            }
            idx = null;
            return data;
        }

    }
}
