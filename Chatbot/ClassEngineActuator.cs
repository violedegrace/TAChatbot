using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    public class EngineActuator
    {
        IEngine model;
        List<string> locations;
        public EngineActuator(IEngine e)
        {
            model = e;
        }

        public void cacheFileCrawler()
        {

        }

        public void Indexing()
        {
            if (locations==null)
            {
                cacheFileCrawler();
            }
            model.dataIndexing(locations);
            model.createInvertedIndex();
        }


    }
}
