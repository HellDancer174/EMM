using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class TechSpeedModel
    {
        private IEnumerable<double> speeds;
        private int trainNumber;
        public TechSpeedModel(IEnumerable<double> speeds, int number)
        {
            this.speeds = speeds;
            trainNumber = number;
        }
        public override string ToString()
        {
            if (speeds.Count() == 0 || speeds.Contains(Double.NaN)) return String.Format("Поезд №{0} - 0 км/ч", trainNumber);
            var firstFlag = true;
            string speedsString = String.Format("Поезд №{0} - ", trainNumber);
            foreach(var speed in speeds)
            {
                if (!firstFlag) speedsString += "/";
                speedsString += Math.Round(speed, 2)+ " км/ч";
                firstFlag = false;
            }
            return speedsString;
        }
    }
}
