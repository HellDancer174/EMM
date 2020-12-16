using System;

namespace EMM_API.Models.RouteModels
{
    public interface ITrainModel
    {
        void Rebuild(Train train);
        void Publish(Action<int, int, string, string, Locomotive, string, int, int, int> instructionsForMe);
        TResult Publish<TResult>(Func<int, int, string, string, Locomotive, string, int, int, int, TResult> instructions);
    }
}