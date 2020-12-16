using System;
using System.Collections.Generic;
using System.Text;
using EMM.Helpers;

namespace EMM.Models
{
    public class RoutePremium
    {
        private readonly DateTime start;
        private readonly DateTime finish;
        private TrainsPremium trainsPremium;
        private ServiceAreaPremium serviceAreaPremium;
        private PassangersPremium passangersPremium;
        private Passangers passangers;
        private readonly double rate;
        private readonly Route route;
        private DateTimeCalc dateTimeCalc = new DateTimeCalc();
        public bool IsEmpty { get; private set; }
        public RoutePremium()
        {
            start = new DateTime();
            finish = new DateTime();
            List<Passanger> passangersList = new List<Passanger>();
            List<Train> trains = new List<Train>();
            List<Station> stations = new List<Station>();
            trainsPremium = new TrainsPremium(start, finish, passangersList, trains, stations);
            serviceAreaPremium = new ServiceAreaPremium(new ServiceArea(new List<string>(), new Directions(new List<Direction>())), new TimeSpan(), new TimeSpan());
            passangers = new Passangers(start, finish, passangersList, new List<Locomotive>());
            passangersPremium = new PassangersPremium(passangers);
            rate = 0;
            route = new Route();
            IsEmpty = true;
        }
        public RoutePremium(DateTime start, DateTime finish, TrainsPremium trainsPremium, ServiceAreaPremium serviceAreaPremium, PassangersPremium passangersPremium, Passangers passangers, double rate, Route route)
        {
            this.start = start;
            this.finish = finish;
            this.trainsPremium = trainsPremium;
            this.serviceAreaPremium = serviceAreaPremium;
            this.passangersPremium = passangersPremium;
            this.passangers = passangers;
            this.rate = rate;
            this.route = route;
            IsEmpty = false;
        }
        public double CalcNight()
        {
            return dateTimeCalc.CalcNightTime(start, finish) * rate * 0.4;
        }

        public double CalcFullTime()
        {
            TimeSpan workTime = CalcWorkTime();
            if (workTime < new TimeSpan()) return 0;
            else return workTime.TotalHours * rate;
        }

        private TimeSpan CalcWorkTime()
        {
            var time = (finish - start) - passangers.CalcPassangersTime() - passangers.CalcWaitPassangersTime();
            if (time.TotalHours < 0) return new TimeSpan();
            else return time;
        }
        public double CalcManueversWithoutHelper()
        {
            var trainDecorator = new TrainsDecorator(route);
            if (trainDecorator.IsManueversWithoutHelper()) return CalcWorkTime().TotalHours * rate * 0.25;
            else return 0;
        }

        public double CalcHarmfulness()
        {
            return CalcWorkTime().TotalHours * rate * 0.04;
        }
        public double CalcArea() => serviceAreaPremium.CalcAreaPremium(rate);
        public double CalcHeavyTrain() => trainsPremium.CalcHeavyWeight(rate);
        public double CalcLongTrain() => trainsPremium.CalcLongLength(rate);
        public double CalcFirstConnect() => trainsPremium.CalcFirstConnect(rate);
        public double CalcLastConnect() => trainsPremium.CalcLastConnect(rate);
        public double CalcPassangerTime() => passangersPremium.CalcPassPremium(rate);
        public double CalcWaitPassTime() => passangersPremium.CalcWaitPassPremium(rate);

        public double CalcRailLubricator()
        {
            var traindecorator = new TrainsDecorator(route);
            var time = traindecorator.CalcRailLubricatorTime();
            var premium = 0d;
            if (Settings.Position.Contains("выводное и хозяйственное движение") && Settings.Position.StartsWith("Машинист")) premium = 0.2;
            else if (Settings.Position.Contains("выводное и хозяйственное движение") && Settings.Position.StartsWith("Помощник")) premium = 0.25;
            else premium = 0;
            if (time.TotalHours < 0) return 0;
            else return time.TotalHours * rate * premium;
        }

    }
}
