using EMM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Helpers.UserDecorators
{
    public class UsersPayRollCreater : BaseUserDecorator
    {
        private int diselCount;
        private int electroCount;

        public UsersPayRollCreater(User user) : base(user)
        {
        }
        public PayRollCreater CreatePayRoll(List<Route> routes, int month, int year, Directions directions)
        {
            var routePremiums = new List<RoutePremium>(10);
            //routePremiums = routes.Where(route => CheckDate(year, route, month)).Select(route => route.CreateRoutePremium(directions, rate, month, year)).Where(routePremium => !routePremium.IsEmpty).ToList();
            foreach(var route in routes)
            {
                if (!CheckDate(year, route, month)) continue;
                else routePremiums.Add(route.CreateRoutePremium(directions, rate, month, year));
            }
            return new PayRollCreater(routePremiums, user.CreateQualificationPercent(diselCount, electroCount));
        }
        private bool CheckDate(int year, Route route, int monthsIndex)
        {
            var result = route.ToWorkTime(monthsIndex, year) != new TimeSpan();
            var decorator = new LocomotivesDecorator(route);
            if (result && decorator.HasDiselLocomotive()) diselCount++;
            if (result && decorator.HasElectroLocomotive()) electroCount++;
            return result;
        }

    }
}
