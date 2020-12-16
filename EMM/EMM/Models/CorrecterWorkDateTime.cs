using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class CorrecterWorkDateTime
    {
        public void Correct(out DateTime outStart, out DateTime outFinish, int month, int year, DateTime start, DateTime finish)
        {
            if (month <= 0 || month > 12 || (start.Month != month && finish.Month != month) || (start.Year != year && finish.Year != year))
            {
                outStart = new DateTime();
                outFinish = new DateTime();
            }
            else if (start.Month != month && finish.Month == month && finish.Year == year)
            {
                outFinish = finish;
                outStart = new DateTime(finish.Year, month, 1);
            }
            else if (start.Month == month && finish.Month != month && start.Year == year)
            {
                outFinish = new DateTime(start.Year, month, DateTime.DaysInMonth(start.Year, month), 23, 59, 59);
                outStart = start;
            }
            else if (start.Year != year || finish.Year != year)
            {
                outStart = new DateTime();
                outFinish = new DateTime();
            }
            else
            {
                outStart = start;
                outFinish = finish;
            }

        }
    }
}
