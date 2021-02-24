using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace WordsFrequency
{
    class Program
    {
        class Element
        {//Класс Элемент представляет собой комбинацию Слово-Повторение набора слов в тексте
            public string Name { get; set; }

            public int Frequency { get; set; }

            public Element(string n, int f) { Name = n; Frequency = f; }

        }

        

        static void Main(string[] args)
        {
            //Создаем справочник экземпляр класса Элемент
            List<Element> Dictionary = new List<Element>();
            Console.WriteLine("Введите путь к файлу и нажмите Enter:");
            
            //Считываем путь в переменную path через консоль 
            string path = Console.ReadLine();
            
            //Считываем весь текст из файла с указанным путем
            string text = File.ReadAllText(path);

            //Проверка на пустой файл
            if (text != "")
            {
                //Функция Split возвращает текст без символов, которые указаны в параметрах
                string[] words = text.Split(new[] { '.', ',' , '"', '\'', '!', '?', ' ', ':', '-', '\r', '\n', '\0', '[', ']' }
                //ToLower возвращает слово в нижнем регистре; 
                //ToArray позволяет представить набор как массив, и работать с ним как с массивом.
                , StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();

                //Функция Distinct позволяет нам получить набор уникальных слов в общем наборе слов
                string[] unique_words = words.Distinct().ToArray();
                foreach (string word in unique_words)
                {
                    int count = words.Count(x => x.Equals(word));
                    Dictionary.Add(new Element(word, count));
                }

                //Запрашиваем путь для будущего файла
                Console.WriteLine("Введите путь к папке файла:");
                path = Console.ReadLine();

                //Используем StreamWriter для создания и записи текста в файл
                using StreamWriter outputFile = new StreamWriter(path + "\\WordsFrequency.txt");
                { foreach (Element elem in Dictionary.OrderByDescending(x => x.Frequency))
                    {
                        //Файл будет сохранен после завершения работы программы
                        outputFile.WriteLine(elem.Name + " " + elem.Frequency.ToString());
                    } 
                }

                
            }
            else
            {
                Console.WriteLine("Файл пуст.");
                Console.ReadLine();
            }
            
        } 
    }
}
