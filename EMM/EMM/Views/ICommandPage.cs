using Xamarin.Forms;

namespace EMM.Views
{
    public interface ICommandPage
    {
        void PrintErorAsync(string message);
        void GoBackAsync();

    }
}