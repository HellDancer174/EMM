using EMM.Services;
using EMM.ViewModels;
using EMM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Helpers
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User: IEquatable<User>
    {
        [JsonProperty]
        protected string position;
        [JsonProperty]
        protected double rate;
        [JsonProperty]
        protected int qualificationClass;
        private IDictionary<int, int> qualificationPremiumPairs = new Dictionary<int, int>() { { 1, 20 }, { 2, 10 }, { 3, 5 }, { 4, 0 }, { 5, 5 }, { 6, 0 } };
        protected IDictionary<int, string> qualificationNames = new Dictionary<int, string>() { { 1, "1 класс" }, { 2, "2 класс" }, { 3, "3 класс" }, { 4, "Без класса" }, { 5, "С правами управления" }, { 6, "Без прав управления" } };
        public User(): this("Помощник машиниста электровоза (пассажирское движение)", 144.2, 6)
        {
        }
        public User(string position, double rate, int qualificationClass)
        {
            this.position = position;
            this.rate = rate;
            if (qualificationClass > 6 || qualificationClass < 1) this.qualificationClass = 6;
            else this.qualificationClass = qualificationClass;
        }
        public User(string position, double rate, string qualification)
        {
            this.position = position;
            this.rate = rate;
            this.qualificationClass = HandleQualification(qualification);
        }
        public async Task Rebuild(User other, ICommandPage page)
        {
            if (Equals(other)) return;
            position = other.position;
            rate = other.rate;
            qualificationClass = other.qualificationClass;
            var sevices = new ApiServices();
            var catcher = new PrintErorCatcher(new HttpResponseCather(new HttpPageCatcher(new PageTryCatcher(page))));
            await catcher.ExecuteAsync(async () => await sevices.UpdateUserAsync(Settings.AccessToken, this));
        }
        protected int HandleQualification(int qualificationClass)
        {
            if (qualificationClass > 6 || qualificationClass < 1) qualificationClass = 6;
            return qualificationClass;
        }
        protected int HandleQualification(string qualificationClass)
        {
            return HandleQualification(qualificationNames.Values.ToList().IndexOf(qualificationClass) + 1);
        }

        public User(User user)
        {
            position = user.position;
            rate = user.rate;
            qualificationClass = HandleQualification(user.qualificationClass);
        }
        public int CreateQualificationPercent(int disel, int electro)
        {
            int percent = qualificationPremiumPairs[qualificationClass];
            if (disel >= 6 && electro >= 6 && (qualificationClass != 6 && qualificationClass != 4)) percent += 5;
            return percent;
        }
        public SettingsVM CreateSettingsVM(INavigational page)
        {
            return new SettingsVM(page, this, position, qualificationNames[qualificationClass], rate);
        }

        public bool Equals(User other)
        {
            return other.position == position && other.qualificationClass == qualificationClass && other.rate == rate;
        }
    }
}
