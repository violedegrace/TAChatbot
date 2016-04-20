using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempChatbot
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
        public Term()
        {
            Index = new List<Location>();
        }
    

    }
}
