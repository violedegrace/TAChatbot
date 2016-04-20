using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    public interface IEngine
    {
        void dataIndexing(List<string> loc);
        void createInvertedIndex();
    }
}
