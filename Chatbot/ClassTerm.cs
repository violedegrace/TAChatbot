using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chatbot
{
    public enum JenisKata
    {
        /* Summary
         * Enum untuk menentukan Sifat/Jenis dari kata yang ada di DataBase
        */
        Unknown, Benda, Kerja, Sifat, Keterangan, Ganti, Bilangan,
        Tugas, Tanya
    }
    public class Term
    {
        private string _kata;
        public string Word
        {
            get { return _kata; }
            set { _kata = value; }
        }

        private List<Location> _index;
        public List<Location> Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private List<double> _bobot;
        public List<double> Bobot
        {
            get { return _bobot; }
            set { _bobot = value; }
        }

        private bool _stopWord;
        public bool StopWord
        {
            get { return _stopWord; }
            set { _stopWord = value; }
        }

        private JenisKata _jenis;
        public JenisKata Jenis
        {
            get { return _jenis; }
            set { _jenis = value; }
        }
                
        public Term()
        {
            Index = new List<Location>();
            Jenis = JenisKata.Unknown;
        }
        public Term(string str):this()
        {
            Word = str;
        }
    

    }
}
