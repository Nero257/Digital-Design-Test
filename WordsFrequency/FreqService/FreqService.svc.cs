using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace FreqService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class FreqService : IFreqService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public Dictionary<string, int> GetFrequencyDict(string text)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            //Функция Split возвращает текст без символов, которые указаны в параметрах
            string[] words = text.Split(new[] { '.', ',', '"', '\'', '!', '?', ' ', ':', ';', '-', '\r', '\n', '\0', '[', ']', ')', '(' }
            //ToLower возвращает слово в нижнем регистре; 
            //ToArray позволяет представить набор как массив, и работать с ним как с массивом.
            , StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower()).ToArray();

            //Функция Distinct позволяет нам получить набор уникальных слов в общем наборе слов
            string[] unique_words = words.Distinct().ToArray();

            //Используем потоковое выполнение статического класса parallel
            Parallel.ForEach(unique_words, word =>
            {
                int count = words.Count(x => x.Equals(word));
                dict.Add(word, count);
            });

            return dict;
        }
    }
}
