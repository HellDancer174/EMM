using System;

namespace EMM_API.Models.RouteModels
{
    public interface IPassangerModel
    {
        void Rebuild(Passanger passanger);
        void Publish(Action<int, DateTime, DateTime> instructionsForMe);
        TResult Publish<TResult>(Func<int, int, DateTime, DateTime, TResult> instructions);
    }
}