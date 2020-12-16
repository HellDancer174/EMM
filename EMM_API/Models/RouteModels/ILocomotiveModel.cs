using System;
using System.Collections.Generic;

namespace EMM_API.Models.RouteModels
{
    public interface ILocomotiveModel
    {
        void Rebuild(Locomotive locomotive);
        void Publish(Action<string, string, BackgroundTime, IEnumerable<Meters>, int> instructionsForMe);
        TResult Publish<TResult>(Func<string, string, BackgroundTime, IEnumerable<Meters>, int, TResult> instructions);
    }
}