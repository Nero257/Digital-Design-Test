﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WordsFrequencyMethod;


namespace WordsFrequency
{
    class Program
    {
                
        static void Main(string[] args)
        {
            
           Console.WriteLine("Введите путь к файлу и нажмите Enter:");

            //Считываем путь в переменную path через консоль 
            string path = Console.ReadLine();

            //Считываем весь текст из файла с указанным путем
            string text = File.ReadAllText(path);

            //Проверка на пустой файл
            if (text != "") {
                Type t = typeof(FreqDictionary);
                MethodInfo mi = t.GetTypeInfo().GetDeclaredMethod("CreateFreqDictionary");
                Dictionary<string, int> dict = (Dictionary<string, int>)mi.Invoke(t, new object[] {text});

                //Запрашиваем путь для будущего файла
                Console.WriteLine("Введите путь к папке файла:");
                path = Console.ReadLine();

                //Используем StreamWriter для создания и записи текста в файл
                using StreamWriter outputFile = new StreamWriter(path + "\\WordsFrequency.txt");
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
