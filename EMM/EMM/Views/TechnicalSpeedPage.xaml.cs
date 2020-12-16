using EMM.Services;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TechnicalSpeedPage : ContentPage, ICommandPage
    {
        private TechnicalSpeeds viewModel;
        public TechnicalSpeedPage(Routes routes)
        {
            InitializeComponent();
            BindingContext = viewModel = new TechnicalSpeeds(routes, this);
        }

        public async void GoBackAsync()
        {
            await Navigation.PopAsync();
        }

        public async void PrintErorAsync(string message)
        {
            await DisplayAlert("Ошибка", message, "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(viewModel.TechSpeed == null || viewModel.TechSpeed.Count==0) viewModel.Load.Execute(null);
        }

    }
}