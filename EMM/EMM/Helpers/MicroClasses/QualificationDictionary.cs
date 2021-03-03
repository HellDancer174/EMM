using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EMM.Helpers.MicroClasses
{
    public class QualificationDictionary
    {
        protected IDictionary<string, int> qualificationNumbers = new Dictionary<string, int>() { {"1 класс", 1 }, { "2 класс", 2 }, { "3 класс", 3 }, { "Без класса", 4 }, { "С правами управления", 5 }, { "Без прав управления", 6 } };
        protected IDictionary<int, string> qualificationNames = new Dictionary<int, string>() { { 1, "1 класс" }, { 2, "2 класс" }, { 3, "3 класс" }, { 4, "Без класса" }, { 5, "С правами управления" }, { 6, "Без прав управления" } };
        private IDictionary<int, int> qualificationPremiumPairs = new Dictionary<int, int>() { { 1, 20 }, { 2, 10 }, { 3, 5 }, { 4, 0 }, { 5, 5 }, { 6, 0 } };
        public string this[int key]
        {
            get
            {
                if (qualificationNames.ContainsKey(key)) return qualificationNames[key];
                else throw new ArgumentOutOfRangeException(String.Format("Недопустимы класс квалификации. QNumber = {0}", key));
            }
        }

        public int this[string key]
        {
            get
            {
                if (qualificationNumbers.ContainsKey(key)) return qualificationNumbers[key];
                else throw new ArgumentOutOfRangeException(String.Format("Недопустимы класс квалификации. QName = {0}", key));
            }
        }

        public int Premium(int number)
        {
            return qualificationPremiumPairs[number];
        }
        public int Premium(string name)
        {
            return qualificationPremiumPairs[this[name]];
        }


    }
}
