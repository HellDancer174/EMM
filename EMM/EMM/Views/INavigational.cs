using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.Views
{
    public interface INavigational: ICommandPage
    {
        Task PopAsync();
        Task PushAsync(Page page);
    }
}