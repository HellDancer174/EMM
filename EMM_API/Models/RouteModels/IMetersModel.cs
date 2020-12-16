using System;

namespace EMM_API.Models.RouteModels
{
    public interface IMetersModel
    {
        void Rebuild(Meters meters);
        void Publish(Action<int, int, int, int, int, int, int> instructionsForMe);
        TResult Publish<TResult>(Func<int, int, int, int, int, int, int, TResult> instructions);
    }
}