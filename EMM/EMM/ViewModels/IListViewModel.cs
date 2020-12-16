using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public interface IListViewModel
    {
        Command Load { get; set; }

        Task ExecuteLoadItemsCommand();
    }
}