using System;
using System.Threading.Tasks;

namespace EMM.ViewModels
{
    public interface IStringFromRoute
    {
        DateTime Date { get; set; }
        string Direction { get; set; }

        void Rebuild();
    }
}