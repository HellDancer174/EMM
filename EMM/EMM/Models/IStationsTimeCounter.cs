using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public interface IStationsTimeCounter
    {
        double GetDownTime(IEnumerable<Station> stations);
        double GetFullTime(IEnumerable<Station> stations);
    }
}
