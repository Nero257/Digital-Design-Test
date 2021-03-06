﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WordsFrequencyMethod;
using System.Diagnostics;

namespace WordsFrequency
{
    class Program
    {
        public static Dictionary<string, int> DictWithService(string text)
        {   

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            using (var client = new WordsFrequencyNew.FDService.FreqServiceClient())
            {
                list = client.GetFrequencyDict(text).ToList<KeyValuePair<string, int>>();
            }

            //Not the best but simple List -> Dictionary
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in list)
            {
                if (pair.Key != null & pair.Key != "") 
                    dict.Add(pair.Key, pair.Value);
            }
            return dict;
            
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Введите путь к файлу и нажмите Enter:");

            //Считываем путь в переменную path через консоль 
            string path = Console.ReadLine();

            //Считываем весь текст из файла с указанным путем
            string text = File.ReadAllText(path);

            //Проверка на пустой файл
            if (text != "")
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                Type t = typeof(FreqDictionary);
                MethodInfo mi = t.GetTypeInfo().GetDeclaredMethod("CreateFreqDictionary");

                //Добавляем Stopwatch класс
                Stopwatch swatch = new Stopwatch();

                //Reflection method Dictionary
                swatch.Start();
                //Dictionary<string, int> dict = (Dictionary<string, int>)mi.Invoke(null , new object[] {text});
                swatch.Stop();
                Console.WriteLine(swatch.ElapsedMilliseconds.ToString() + " - время приватного метода");

                //Parallel method service dictionary
                swatch.Restart();
                dict = DictWithService(text).ToDictionary(x=>x.Key, x=>x.Value);
                swatch.Stop();
                Console.WriteLine(swatch.ElapsedMilliseconds.ToString() + " - время параллельного публичного метода из сервиса");

                //Запрашиваем путь для будущего файла
                Console.WriteLine("Введите путь к папке файла:");
                path = Console.ReadLine();

                //Используем StreamWriter для создания и записи текста в файл
                using (StreamWriter outputFile = new StreamWriter(path + "\\WordsFrequency.txt"))
                {
                    foreach (var elem in dict.OrderByDescending(x => x.Value))
                    {
                        //Файл будет сохранен после завершения работы программы
                        outputFile.WriteLine(elem.Key + " " + elem.Value.ToString());
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
