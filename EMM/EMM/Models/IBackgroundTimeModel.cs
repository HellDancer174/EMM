using System;

namespace EMM.Models
{
    public interface IBackgroundTimeModel
    {
        void Rebuild(BackgroundTime time);
        void Publish(Action<DateTime, DateTime, DateTime, DateTime> instructions);
        TResult Publish<TResult>(Func<int, DateTime, DateTime, DateTime, DateTime, TResult> instructions);
    }
}