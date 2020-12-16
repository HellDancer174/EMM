using System;
using System.Collections.Generic;

namespace EMM_API.Models.RouteModels
{
    public interface IRouteModel
    {
        void Rebuild(Route route);
        bool Check();
        void Publish(Action<DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool> instructionsForMe);
        TResult Publish<TResult>(Func<int, DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool, TResult> instructions);
    }
}