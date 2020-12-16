using EMM.Helpers;
using EMM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class PayRollCreater
    {
        private List<RoutePremium> routePremia;
        private readonly int qualificationPercent;

        public PayRollCreater(List<RoutePremium> routePremia, int qualificationPercent)
        {
            this.routePremia = routePremia;
            this.qualificationPercent = qualificationPercent;
        }
        public PayRollCellVM CreateFullTime()
        {
            return new PayRollCellVM("Оплата по часовой тарифной ставке", routePremia.Select(premium=> premium.CalcFullTime()).Sum());
        }
        public PayRollCellVM CreateNightTime()
        {
            return new PayRollCellVM("Оплата за работу в ночное время", routePremia.Select(premium => premium.CalcNight()).Sum());
        }
        public PayRollCellVM CreateHarmfulness()
        {
            return new PayRollCellVM("Оплата за работу с вредными и(или) опасными условиями труда", routePremia.Select(premium => premium.CalcHarmfulness()).Sum());
        }
        public PayRollCellVM CreateArea()
        {
            return new PayRollCellVM("Оплата за удлиненный участок обслуживания", routePremia.Select(premium => premium.CalcArea()).Sum());
        }
        public PayRollCellVM CreateHeavyTrain()
        {
            return new PayRollCellVM("Оплата за вождение тяжеловесных поездов", routePremia.Select(premium => premium.CalcHeavyTrain()).Sum());
        }
        public PayRollCellVM CreateLongTrain()
        {
            return new PayRollCellVM("Оплата за вождение длинносоставных поездов", routePremia.Select(premium => premium.CalcLongTrain()).Sum());
        }
        public PayRollCellVM CreateFirstConnect()
        {
            return new PayRollCellVM("Оплата за вождение соединенных поездов(в голове поезда)", routePremia.Select(premium => premium.CalcFirstConnect()).Sum());
        }
        public PayRollCellVM CreateLastConnect()
        {
            return new PayRollCellVM("Оплата за вождение соединенных поездов(в середине поезда)", routePremia.Select(premium => premium.CalcLastConnect()).Sum());
        }
        public PayRollCellVM CreatePassangerTime()
        {
            return new PayRollCellVM("Оплата за следование пассажиром", routePremia.Select(premium => premium.CalcPassangerTime()).Sum());
        }
        public PayRollCellVM CreateWaitPassangerTime()
        {
            return new PayRollCellVM("Оплата за время ожидания следования пассажиром", routePremia.Select(premium => premium.CalcWaitPassTime()).Sum());
        }
        public PayRollCellVM CreateManueversWithoutHelper()
        {
            return new PayRollCellVM("Оплата за работу в одно лицо(маневровое, выводное движение)", routePremia.Select(premium => premium.CalcManueversWithoutHelper()).Sum());
        }
        public PayRollCellVM CreateRailLubricator()
        {
            return new PayRollCellVM("Оплата за обслуживание рельсосмазывателя", routePremia.Select(premium => premium.CalcRailLubricator()).Sum());
        }
        public PayRollCellVM CreateQualificationClass()
        {
            return new PayRollCellVM(String.Format("Оплата за класс квалификации {0}%", qualificationPercent), routePremia.Select(premium => premium.CalcFullTime()).Sum()*(qualificationPercent/100));
        }
        public PayRollCellVM CreateLocalPremium(double premium)
        {
            return new PayRollCellVM("Районный коэффициент", premium);
        }











    }
}
