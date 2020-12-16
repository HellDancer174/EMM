using EMM.Helpers;
using EMM.Models;
using EMM.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.NUnitTests
{
    [TestFixture]
    public class RoutePremiumTests
    {
        private Route route;
        private RoutePremium routePremium;
        private RoutePremium routePremiumWithPass;
        private Directions directions;

        [SetUp]
        public void Setup()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 0, 0, 154, 158) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 12, 1, 23, 20, 0), new DateTime(2020, 12, 1, 23, 49, 0), Settings.DefaultDateTime, new DateTime(2021, 01, 1, 8, 10, 0)), mockMeters, 1);
            var mockTrain = new Train(2, 2805, "Челябинск Б", "Карталы", mockLoc, "Т", 7058, 282, 71);
            var firstStation = new Station(8, "Челябинск Б", Settings.DefaultDateTime, new DateTime(2020, 12, 1, 23, 55, 0), default(TimeSpan));
            var station = new Station(9, "Троицк", new DateTime(2021, 01, 1, 4, 10, 0), new DateTime(2021, 01, 1, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Карталы", new DateTime(2021, 01, 1, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            var mockLocs = new List<Locomotive>();
            var mockTrains = new List<Train>();
            var mockStations = new List<Station>();
            var mockPass = new List<Passanger>();
            mockLocs.Add(mockLoc);
            mockTrains.Add(mockTrain);
            mockStations.Add(firstStation);
            mockStations.Add(station);
            mockStations.Add(lastStation);
            route = new Route(1, new DateTime(2020, 12, 31, 23, 00, 0), new DateTime(2021, 01, 1, 8, 20, 0), mockLocs, mockTrains, mockStations, new List<Passanger>(), "", false);
            var service = new ApiServices();
            directions = new Directions(new List<Direction>());
            Task.WaitAll(Task.Run(async () =>
            {
                var dirs = await service.GetDirectionsAsync();
                directions = new Directions(dirs.ToList());
            }));
            routePremium = route.CreateRoutePremium(directions, 144.2, 1, 2021);
            mockPass.Add(new Passanger(1, 366, new DateTime(2021, 01, 1, 8, 20, 0), new DateTime(2021, 01, 1, 11, 20, 0)));
            var routeWithPass = new Route(2, new DateTime(2020, 12, 31, 23, 55, 0), new DateTime(2021, 01, 1, 12, 20, 0), mockLocs, mockTrains, mockStations, mockPass, "", false);
            routePremiumWithPass = routeWithPass.CreateRoutePremium(directions, 144.2, 1, 2021);

        }

        [Test]
        public void Calc_NightTime_235_2()
        {
            var expected = new TimeSpan(4, 0, 0).TotalHours * 144.2 * 0.4;
            var actual = routePremium.CalcNight();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcHeavy_HeavyTrue__121_37()
        {
            var expected = route.ToWorkTime(1, 2021).TotalHours * 144.2 * 0.1;
            var actual = routePremium.CalcHeavyTrain();
            Assert.AreEqual(expected, actual, 0.001);
        }
        [Test]
        public void CalcHeavy_AreaIs261__242_74()
        {
            var expected = route.ToWorkTime(1, 2021).TotalHours * 144.2 * 0.2;
            var actual = routePremium.CalcArea();
            Assert.AreEqual(expected, actual, 0.01);
        }
        [Test]
        public void CalcHeavy_AreaIs261WithPassangers__237_93()
        {
            var expected = new TimeSpan(8,10,0).TotalHours * 144.2 * 0.2;
            expected = Math.Round(expected, 2);
            var actual = routePremiumWithPass.CalcArea();
            Assert.AreEqual(expected, actual, 0.01);
        }






    }
}
