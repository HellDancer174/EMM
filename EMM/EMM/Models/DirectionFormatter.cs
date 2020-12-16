using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class DirectionFormatter
    {
        protected string GetDirection(string first, string last) => String.Format("{0} - {1}", Handle(first), Handle(last));
        protected string GetReverseDirection(string arraval, string depature) => string.Format("{0} - {1}", Handle(depature), Handle(arraval));

        private string Handle(string station)
        {
            if (station.StartsWith("Челябинск")) return "Челябинск";
            else return station;
        }
    }
}
