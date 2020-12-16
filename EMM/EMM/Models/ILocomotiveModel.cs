using System;
using System.Collections.Generic;

namespace EMM.Models
{
    public interface ILocomotiveModel
    {
        void Rebuild(Locomotive locomotive);
        void Publish(Action<string, string, BackgroundTime, IEnumerable<Meters>, int> instructionsForMe);
        TResult Publish<TResult>(Func<string, string, BackgroundTime, IEnumerable<Meters>, int, TResult> instructions);
    }
}