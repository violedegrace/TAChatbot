using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    public class Location
    {
        private int _domainID;
        public int DomainID
        {
            get { return _domainID; }
            set { _domainID = (value>=0?value:-1); }
        }
        private int _infID;
        public int InfID
        {
            get { return _infID; }
            set { _infID = (value >= 0 ? value : -1); }
        }
        private int _InfDetilID;
        public int InfDetilID
        {
            get { return _InfDetilID; }
            set { _InfDetilID = (value >= 0 ? value : -1); }
        }
        private int _idx;
        public int Idx
        {
            get { return _idx; }
            set { _idx = (value >= 0 ? value : -1); }
        }
        public Location(int dom,int inf, int det, int idx)
        {
            DomainID = dom;
            InfID = inf;
            InfDetilID = det;
            Idx = idx;
        }
    }
}
