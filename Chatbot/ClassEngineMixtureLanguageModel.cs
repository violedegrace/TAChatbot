using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tempChatbot
{
    public class EngineMixtureLanguageModel : IEngine
    {
        dbDataContext db;
        public EngineMixtureLanguageModel(dbDataContext database)
        {
            if (database != null)
                db = database;
            else
                db = new dbDataContext();
        }
        public void dataIndexing(List<string> loc)
        {
            foreach (var item in loc)
            {
                
            }
        }

        private tbDomain cariDomain(string loc)
        {
            tbDomain dom=null;
            if (loc!=null)
            {
                //foreach (var item in collection)
                //{
                    
                //}
                return dom;
            }
            else
            {
                return null;
            }
        }
        public void createInvertedIndex()
        {
        }
    }
}
