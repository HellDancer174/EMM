using System;

namespace EMM.Models
{
    public class DateTimeCalc
    {
        public double CalcNightTime(DateTime start, DateTime finish)
        {
            var offset = 0;
            var nightTimeStartHour = 20;
            var nightTimeStartMinute = 0;
            var nightTimeEndHour = 4;
            var nightTimeEndMinute = 0;
            if (start >= finish)
                return 0;
            if (start.Hour < nightTimeEndHour) offset = -1;
            var nightStart = start.Date.AddDays(offset).AddHours(nightTimeStartHour).AddMinutes(nightTimeStartMinute);
            var nightEnd = nightStart.Date.AddDays(1).AddHours(nightTimeEndHour).AddMinutes(nightTimeEndMinute);

            double nightHours = 0;
            while (finish > nightStart)
            {
                if (nightStart < start)
                    nightStart = start;
                if (finish < nightEnd)
                    nightEnd = finish;
                nightHours += (nightEnd - nightStart).TotalHours;
                nightStart = nightStart.Date.AddDays(1).AddHours(nightTimeStartHour).AddMinutes(nightTimeStartMinute);
                nightEnd = nightStart.Date.AddDays(1).AddHours(nightTimeEndHour).AddMinutes(nightTimeEndMinute);
            }

            return nightHours;
        }

    }
}