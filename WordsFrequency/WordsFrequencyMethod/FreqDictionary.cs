using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsFrequencyMethod
{

    public class FreqDictionary
    {
        
        private static Dictionary<string, int> CreateFreqDictionary(string text)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //Функция Split возвращает текст без символов, которые указаны в параметрах
            string[] words = text.Split(new[] { '.', ',', '"', '\'', '!', '?', ' ', ':', '-', '\r', '\n', '\0', '[', ']' }
            //ToLower возвращает слово в нижнем регистре; 
            //ToArray позволяет представить набор как массив, и работать с ним как с массивом.
            , StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();

            //Функция Distinct позволяет нам получить набор уникальных слов в общем наборе слов
            string[] unique_words = words.Distinct().ToArray();
            foreach (string word in unique_words)
            {
                int count = words.Count(x => x.Equals(word));
                dict.Add(word, count);
            }

            return dict;

        }

    } 
}
