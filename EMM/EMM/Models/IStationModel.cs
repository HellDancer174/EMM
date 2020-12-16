using System;

namespace EMM.Models
{
    public interface IStationModel
    {
        void Rebuild(Station station);
        void Publish(Action<int, string, DateTime, DateTime, TimeSpan> instructionsForMe);
        TResult Publish<TResult>(Func<int, string, DateTime, DateTime, TimeSpan, TResult> instructions);
    }
}