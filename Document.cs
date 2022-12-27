using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTestDOC
{
    public class Document // Документ
    {
        public string name { get; set; } // название
        public string description { get; set; } // описание
        public string fileName { get; set; } // имя файла
        public bool left { get; set; } // документ подписан первой стороной
        public bool right { get; set; } // документ подписан второй стороной
        int item1 { get; set; } // можно что-то еще хранить
        int item2 { get; set; } // можно что-то еще хранить
        int item3 { get; set; } // можно что-то еще хранить

        public Document(string fileName, string name = "", string description = "")
        {
            this.fileName = fileName;
            this.name = name;
            this.description = description;
            left = false;
            right = false;
            item1 = 1;
            item2 = 2;
            item3 = 3;
        }

        public bool IsReady() => left && right;
    }
}
