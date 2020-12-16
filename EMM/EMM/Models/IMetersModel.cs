using System;

namespace EMM.Models
{
    public interface IMetersModel
    {
        void Rebuild(Meters meters);
        void Publish(Action<int, int, int, int, int, int, int> instructionsForMe);
        TResult Publish<TResult>(Func<int, int, int, int, int, int, int, TResult> instructions);
    }
}