using System;

namespace EMM.Models
{
    public interface IWorkTimeModel
    {
        TimeSpan CalcNonWorkTime();
        TimeSpan CalcTime();
        TimeSpan CalcTime(DateTime start);
        TimeSpan CalcWorkTime();
        TimeSpan Difference(DateTime start);
        TimeSpan FinishDifference(DateTime finish);
        bool IsWork();
    }
}