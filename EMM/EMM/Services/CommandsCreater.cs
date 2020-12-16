using EMM.ViewModels;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace EMM.Services
{
    public class CommandsCreater
    {
        public Command Create(Action execute)
        {
            return new Command((obj) => execute());
        }

        public Command CreateForRoute(Action execute, Func<bool> canExecute, ICommandPage page)
        {
            return new Command(() => ExecuteForRoute(execute, canExecute, CrossConnectivity.Current.IsConnected, page));
        }

        public Command CreateForLocalRoute(Action execute, Func<bool> canExecute, ICommandPage page)
        {
            return new Command(() => ExecuteForRoute(execute, canExecute, true, page));
        }
        public Command CreateForActivityIndicator(Action execute, LoginVM loginVM)
        {
            return new Command(() => ExectuteForActivityIndicator(loginVM, execute));
        }

        private void ExectuteForActivityIndicator(LoginVM loginVM, Action execute)
        {
            if (loginVM.IsBusy == true) return;
            loginVM.IsBusy = true;
            loginVM.IsVisible = true;
            loginVM.IsEnable = true;
            if (execute != null)
            {
                var resultobj = execute.BeginInvoke(null, null);
                execute.EndInvoke(resultobj);
            }
            loginVM.IsBusy = false;
            loginVM.IsVisible = false;
            loginVM.IsEnable = false;
        }

        private void ExecuteForRoute(Action execute, Func<bool> canExecute, bool connected, ICommandPage page)
        {
            string message = String.Empty;
            if (canExecute.Invoke() == false) message = "Проверьте правильность введенных данных";
            if (connected == false) message = "Отсутствует поключение к интернету";
            if (message != String.Empty)
            {
                page.PrintErorAsync(message);
                return;
            }
            Execute(execute, page);
        }

        private void Execute(Action execute, ICommandPage page)
        {
            if (execute != null) execute.Invoke();
            page.GoBackAsync();
        }
    }
}
