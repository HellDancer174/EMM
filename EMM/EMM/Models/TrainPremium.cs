using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class TrainPremium
    {
        private TimeSpan workTime;
        private bool freight;
        private bool heavy;
        private bool @long;
        private bool longest;
        private bool firstConnect;
        private bool lastConnect;
        public TrainPremium(int number, string type, int length, TimeSpan workTime)
        {
            this.workTime = workTime;
            if (((number > 1000 && number < 2999) || (number > 9000 && number < 9899))) freight = true;
            else freight = false;
            if(freight == true)
            {
                if (type == "ТД" && length < 90)
                {
                    heavy = true;
                    @long = true;
                }
                else if (type == "ТД" && length >= 90)
                {
                    heavy = true;
                    longest = true;
                }
                else if (type == "Т") heavy = true;
                else if (type == "Д" && length < 90) @long = true;
                else if (type == "Д" && length >= 90) longest = true;
                else if (type == "СГ") firstConnect = true;
                else if (type == "С") lastConnect = true;
            }
        }
        public double CalcHeavyRate(double rate)
        {
            if (!freight) return 0;
            if (heavy && !firstConnect && !lastConnect) return (rate * 0.10) * workTime.TotalHours;
            else return 0;

        }
        public double CalcLongRate(double rate)
        {
            if (!freight) return 0;
            if (@long) return rate * 0.10 * workTime.TotalHours;
            else if (longest) return rate * 0.15 * workTime.TotalHours;
            else return 0;
        }
        public double CalcFirstConnect(double rate)
        {
            if (firstConnect) return (rate * 0.30) * workTime.TotalHours;
            else return 0;
        }
        public double CalcLastConnect(double rate)
        {
            if (lastConnect) return (rate * 0.15) * workTime.TotalHours;
            else return 0;
        }




    }
}
