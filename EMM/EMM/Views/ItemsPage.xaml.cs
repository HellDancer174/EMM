using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EMM.Models;
using EMM.Views;
using EMM.ViewModels;
using EMM.Services;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage, INavigational, ICommandPage
    {
        private RoutesVM viewModel;
        private LocalRoutesVM localViewModel;
        private bool local;
        public ItemsPage(Routes pack)
        {
            InitializeComponent();
            BindingContext = viewModel = new RoutesVM(pack, this);
            local = false;
        }
        public ItemsPage(RoutesVM routes)
        {
            InitializeComponent();
            BindingContext = viewModel = routes;
            local = false;
        }


        public ItemsPage(LocalRoutes routes)
        {
            InitializeComponent();
            BindingContext = localViewModel = new LocalRoutesVM(routes, this);
            local = true;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var route = args.SelectedItem as StringFromRoute;
            if (route == null) return;
            //var routeDetail = route.ToRouteDetailVM();
            if (local == false) await Navigation.PushAsync(new ItemDetailPage(route.ToRouteDetailVM()));
            else if (local == true)
            {
                //await Navigation.PushAsync(new LocalRouteEditPage(route.ToLocalEditVM()));
                var localedit = route.ToLocalEditVM(Navigation);
            } 

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (!local)
            {
                await Navigation.PushAsync(new RouteEditPage());
            }
            else if(local)
            {
                var localnew = new LocalNewRouteVM(new Route(), Navigation);
                //await Navigation.PushAsync(new RouteEditPage(true));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (local==false && viewModel.StringRoutes.Count == 0)
                viewModel.Load.Execute(null);
            else if(local==true && localViewModel.StringRoutes.Count == 0)
            {

                localViewModel.Load.Execute(null);
                
            }
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("Ошибка", message, "Ok");
        }

        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task PushAsync(Page page) => await Navigation.PushAsync(page);
        public async Task PopAsync() => await Navigation.PopAsync();
    }
}