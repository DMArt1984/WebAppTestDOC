using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTestDOC
{
    
    static public class DocManager // Менеджер документов
    {
        
        // храним в словаре, но по идее надо в базе данных
        static Dictionary<string, Document> documents = new Dictionary<string, Document>();

        // готов ли документ (подписан двумя сторонами)?
        static public bool IsReady(string code) => documents.ContainsKey(code) ? documents[code].IsReady() : false; 
        
        // 1 шаг
        static public string GetFirstData(string fileName, string phone, string IIN, string fullName)
        {
            // здесь можно записать в базу
            // какую базу используем? нужен ли EF ?
            // ...код...

            // генерируем строку, какой нужен формат строки? осмысленный или fiureiuteruf7f457435erfiure
            // используем токены?
            DateTimeOffset now = DateTimeOffset.UtcNow;
            long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
            string code = $"{unixTimeMilliseconds}x{fullName}i{IIN}";
            
            Document document = new Document(fileName, "Договор №ххх", code);

            AddDocument(code, document);

            return code;
        }

        // 2 шаг
        static public Document GetSecondData(string code)
        {
            if (documents.ContainsKey(code))
            {
                // в коммерческой версии здесь будет немного сложнее, например, работа с базой...

                return documents[code];
            }
            return null; 
        }

        
        static public void AddDocument(string code, Document document)
        {
            // Можно делать разное с документом...
            documents.Add(code, document);
        }

        static public Document GetDocument(string code)
        {
            // Можно делать разное с документами...
            return documents.ContainsKey(code) ? documents[code] : null;
        }

        // 3 шаг) Подписание первой стороной
        static public bool SetLeft(string code)
        {
            if (!documents.ContainsKey(code))
                return false;

            // Работа с документом
            // ...код...
            // Какую библиотеку работы с PDF используем? платную?

            if (!documents[code].left && !documents[code].right)
            {
                // подписание
                // ...код...
                documents[code].left = true; // 1 = Подписание первой стороной
            }

            return true;
        }

        // 4 шаг) Подписание второй стороной
        static public bool SetRight(string code)
        {
            if (!documents.ContainsKey(code))
                return false;

            if (documents[code].left) // если одна сторона подписала
            {
                if (!documents[code].right) // а вторая еще нет
                {
                    // подписание
                    // ...код...
                    documents[code].right = true; // 2 = Подписание второй стороной
                }
                return true;
            } else
            {
                return false;
            }
        }

    }
}
