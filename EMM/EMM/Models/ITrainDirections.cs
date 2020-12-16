using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public interface ISubStationer
    {
        IEnumerable<Station> CutStations();
    }
}
