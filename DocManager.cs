using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTestDOC
{
    
    static public class DocManager
    {
        
        // переменные для теста
        static string lastCode;
        static string lastFileName;
        static Document lastDocument;
        static byte step; // этапы подписания

        static public bool Ready() => step == 2; 
        
        static public string GetFirstData(string fileName, string phone, string IIN, string fullName)
        {
            // здесь можно записать в базу
            // какую базу используем? нужен ли EF ?
            // ...код...

            // генерируем строку, какой нужен формат строки? осмысленный или fiureiuteruf7f457435erfiure
            string Code = $"{fileName.Length}p{phone}i{IIN}x{fullName}";
            Code = Code.Replace("+", String.Empty);
            // используем токены?

            // это только для теста
            step = 0; // 0 = никто не подписал
            lastCode = Code;
            lastFileName = fileName;

            return Code;
        }

        static public Document GetSecondData(string code)
        {
            if (code == lastCode)
            {
                // в коммерческой версии здесь будет немного сложнее, например, работа с базой...

                Document document = new Document(lastFileName, "Договор №ххх", code);
                AddDocument(document);
                return document;
            }
            return null; 
        }

        static public void AddDocument(Document document)
        {
            // Можно делать разное с документом...
            lastDocument = document;
        }

        static public Document GetDocument(string code)
        {
            if (code != lastCode) // симуляция проверки
                return null;

            // Можно делать разное с документами...

            return lastDocument;
        }

        // Подписание первой стороной
        static public bool SetLeft(string code)
        {
            if (code != lastCode) // симуляция проверки
                return false;

            // Работа с документом
            // ...код...
            // Какую библиотеку работы с PDF используем? платную?

            step = 1; // 1 = Подписание первой стороной
            return true;
        }

        // Подписание второй стороной
        static public bool SetRight(string code)
        {
            if (code != lastCode) // симуляция проверки
                return false;

            if (step == 1) // если одна сторона подписала, то и вторая тоже
            {
                // Работа с документом
                // ...код...
                lastDocument.ready = true;

                step = 2; // 2 = Подписание второй стороной
                return true;
            } else
            {
                return false;
            }
        }

    }
}
