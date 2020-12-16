using System;
using System.Collections.Generic;

namespace EMM.Models
{
    public interface IRouteModel
    {
        void Rebuild(Route route);
        bool Check();
        bool IsEmpty();
        void Publish(Action<DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool> instructionsForMe);
        TResult Publish<TResult>(Func<int, DateTime, DateTime, IEnumerable<Locomotive>, IEnumerable<Passanger>, IEnumerable<Train>, IEnumerable<Station>, string, bool,TResult> instructions);
    }
}