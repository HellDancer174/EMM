using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class ServiceAreaPremium
    {
        private ServiceArea serviceArea;
        private readonly TimeSpan workTime;
        private readonly TimeSpan passWorktime;

        public ServiceAreaPremium(ServiceArea serviceArea, TimeSpan workTime, TimeSpan passWorktime)
        {
            this.serviceArea = serviceArea;
            this.workTime = workTime;
            this.passWorktime = passWorktime;
        }
        public double CalcAreaPremium(double rate)
        {
            var value = serviceArea.ToDistance();
            if (value > 250 && value < 350) return rate * 0.2 * (workTime-passWorktime).TotalHours;
            else if (value > 350 && value < 500) return rate * 0.25 * (workTime - passWorktime).TotalHours;
            else if (value > 500) return rate * 0.35 * (workTime - passWorktime).TotalHours;
            else return 0;
        }
    }
}
