using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.Views
{
	public class NullPage : ContentPage, INavigational, ICommandPage
	{
		public NullPage ()
		{
		}

        public void GoBackAsync()
        {
            
        }

        public Task PopAsync()
        {
            return Task.CompletedTask;
        }

        public void PrintErorAsync(string message)
        {
            
        }

        public Task PushAsync(Page page)
        {
            return Task.CompletedTask;
        }
    }
}