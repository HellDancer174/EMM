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
	public partial class WorkTimesPage : ContentPage, ICommandPage
    {
        private WorkTimes viewModel;
		public WorkTimesPage(Routes routes)
        {
            viewModel = new WorkTimes(routes, this);
            InitializeComponent();
            BindingContext = viewModel;
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
            if (viewModel.WorkTimeStrings ==null || viewModel.WorkTimeStrings.Count == 0) viewModel.Load.Execute(null);
        }


    }
}