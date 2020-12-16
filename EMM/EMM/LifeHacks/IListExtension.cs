using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeHacks
{
    public static class IListExtension
    {
        public static void AddRange<T>(this IList<T> list, params T[] objects)
        {
            foreach (T obj in objects)
            {
                list.Add(obj);
            }
        }
        public static IEnumerable<TResult> Select<T,TResult>(this IList<T> list, int start, int finish, Func<T, TResult> selector)
        {
            if (start > finish) throw new ArgumentException("Конечный индекс меньше стартового");
            if(start<0||finish<0) throw new ArgumentException("Индекс меньше нуля");
            if (finish > list.Count) throw new ArgumentException("Конечный индекс больше list.Count");
            if (start > list.Count) throw new ArgumentException("Стартовый индекс больше list.Count");

            for (int i=start;i<=finish;i++)
            {
                yield return selector(list[i]);
            }
        }
        public static TimeSpan Sum(this IEnumerable<TimeSpan> list)
        {
            var result = default(TimeSpan);
            foreach (var time in list)
            {
                result += time;
            }
            return result;
        }


    }
}
