using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class Qualificator
    {
        private IDictionary<string, int> qualifications;
        public Qualificator()
        {
            qualifications = new Dictionary<string, int>() { {"1 класс", 1 }, { "2 класс", 2 }, { "3 класс", 3 }, { "Без класса", 4 }, { "С правами управления", 5 }, { "Без прав управления", 6 } };
        }
        public int CreateID(string qualificationClass, string position)
        {
            if (qualifications.ContainsKey(qualificationClass)) return qualifications[qualificationClass];
            else if (position.StartsWith("Машинист")) return 4;
            else if (position.StartsWith("Помощник")) return 6;
            else return -1;
        }
    }
}