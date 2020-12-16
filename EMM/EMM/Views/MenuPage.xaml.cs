using EMM.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EMM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage;
        List<HomeMenuItem> menuItems;
        public MenuPage(MainPage mainPage)
        {
            InitializeComponent();
            RootPage = mainPage;

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem("round_description_24", MenuItemType.Browse, "Маршруты"),
                //new HomeMenuItem("round_alarm_24", MenuItemType.About, "О нас"),
                new HomeMenuItem("round_schedule_24", MenuItemType.Browse, "Рабочее время"),
                new HomeMenuItem("round_ev_station_24", MenuItemType.Browse, "Расход ТЭР"),
                new HomeMenuItem("round_speed_24", MenuItemType.Browse, "Техническая скорость"),
                new HomeMenuItem("baseline_text_snippet_24", MenuItemType.Browse, "Расчетные листы"),
                new HomeMenuItem("round_build_24", MenuItemType.Browse, "Настройки"),
                new HomeMenuItem("round_exit_to_app_24", MenuItemType.Browse, "Выход")

            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                //var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu((HomeMenuItem)e.SelectedItem);
            };
        }
    }
}