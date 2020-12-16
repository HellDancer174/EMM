using EMM.Helpers;
using EMM.Services;
using EMM.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EMM.ViewModels
{
    public class RegisterVM : BaseSettingsVM
    {
        private ApiServices apiServices = new ApiServices();
        private IDictionary<string, double> positionRate;
        private RegisterPageCatcher catcher;
        public RegisterVM(ICommandPage page, IDictionary<string, double> positionRate):base(page)
        {
            this.positionRate = positionRate;
            Positions = new ObservableCollection<string>(this.positionRate.Keys);
            catcher = new RegisterPageCatcher(new HttpResponseCather(new HttpPageCatcher(new PageTryCatcher(page))), this);
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value == email) return;
                email = value;
                OnPropertyChanged();
            }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (value == password) return;
                password = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (value == confirmPassword) return;
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        protected override void SetPosition(string value)
        {
            base.SetPosition(value);
            if (positionRate.ContainsKey(position))
            {
                var newRate = positionRate[position];
                Rate = Convert.ToString(newRate);
            }
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command(async() => 
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    //{
                        page.PrintErorAsync("Нет подключение к интернену");
                        //return;
                    //}
                    else await Register();
                    //if (IsSuccess)
                    //{
                    //    Settings.Username = this.Email;
                    //    Settings.Password = this.Password;
                    //    page.GoBackAsync();
                    //    return;
                    //}
                    //else
                    //{
                    //    page.PrintErorAsync("Проверьте правильность введенных данных");
                    //    return;
                    //}
                    
                });
            }
        }

        public bool IsSuccess { get; set; }

        private async Task Register()//ДОПИЛИТЬ!!!!
        {
            bool loginEmailChecker = ConfirmPassword != Password || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(ConfirmPassword) || !IsValidEmail(Email);
            if (loginEmailChecker && String.IsNullOrEmpty(Qualification) && String.IsNullOrEmpty(Position))
                page.PrintErorAsync("Проверьте правильность введенных данных");
            else await catcher.ExecuteAsync(async() => await apiServices.RegisterAsync(Email, Password, ConfirmPassword, Convert.ToDouble(Rate), Position, Qualification));
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

        }
    }
}
