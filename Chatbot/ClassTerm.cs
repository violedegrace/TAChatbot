using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chatbot
{
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

        private List<int> _bobot;
        public List<int> Bobot
        {
            get { return _bobot; }
            set { _bobot = value; }
        }
                
        public Term()
        {
            Index = new List<Location>();
        }
    

    }
}
