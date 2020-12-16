using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHacks
{
    public static class StringExtension
    {
        public static string Format(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        public static int Parse(this string number)
        {
            return int.Parse(number);
        }
    }
}
