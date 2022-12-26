using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTestDOC
{
    public class Document
    {
        public string name { get; set; }
        public string description { get; set; }
        public string fileName { get; set; }
        public bool ready { get; set; } // документ подписан
        public int item1 { get; set; } // можно что-то еще хранить
        public int item2 { get; set; } // можно что-то еще хранить
        public int item3 { get; set; } // можно что-то еще хранить

        public Document(string fileName, string name = "", string description = "")
        {
            this.fileName = fileName;
            this.name = name;
            this.description = description;
            ready = false;
            item1 = 1;
            item2 = 2;
            item3 = 3;
        }
    }
}
