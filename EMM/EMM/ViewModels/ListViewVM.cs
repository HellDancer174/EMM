using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public abstract class ListViewVM : BaseViewModel, IListViewModel
    {
        public Command Load { get; set; }
        private ICommandPage page;
        protected IRoutes routes;
        private HttpResponseCather cather;
        public ListViewVM(IRoutes routes, ICommandPage page)
        {
            this.page = page;
            this.routes = routes;
            //cather = new HttpResponseCather(new HttpPageCatcher(new PageTryCatcher(page)));
            Load = new Command(async () => await ExecuteLoadItemsCommand());
            //Rebuild();
        }


        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            if (CheckConnection())
            {
                page.PrintErorAsync("Отсутствует подключение к интернету");
                IsBusy = false;
                return;
            }

            try
            {
                await Rebuild();
            }
            catch /*(Exception ex)*/
            {
                page.PrintErorAsync("Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, проверки сертификата сервера или времени ожидания");
            }
            finally
            {
                IsBusy = false;
            }

        }

        protected virtual bool CheckConnection() => !CrossConnectivity.Current.IsConnected;

        protected abstract Task Rebuild();
    }
}
