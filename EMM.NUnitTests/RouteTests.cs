using EMM.Helpers;
using EMM.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.NUnitTests
{
    [TestFixture]
    public class RouteTests
    {
        private Route route;
        private Route otherRoute;
        public void Setup()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 0, 0, 154, 158) };
            var mockLoc = new Locomotive("ЭП2К", "207", new BackgroundTime(2, new DateTime(2021, 01, 1, 1, 20, 0), new DateTime(2021, 01, 1, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2021, 01, 1, 8, 10, 0)), mockMeters, 1);
            var mockTrain = new Train(2, 4301, "Челябинск П", "Карталы", mockLoc, null, 0, 0, 0);
            var firstStation = new Station(8, "Челябинск П", Settings.DefaultDateTime, new DateTime(2021, 01, 1, 2, 1, 0), default(TimeSpan));
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
            route = new Route(1, new DateTime(2020, 12, 31, 23, 55, 0), new DateTime(2021, 01, 1, 8, 20, 0), mockLocs, mockTrains, mockStations, mockPass, "", false);
            var mockTrain1 = new Train(2, 4301, "Челябинск П", "", mockLoc, null, 0, 0, 0);
            otherRoute = new Route(1, new DateTime(2020, 12, 31, 23, 55, 0), new DateTime(2021, 01, 1, 8, 20, 0), mockLocs, new List<Train>() { mockTrain1 }, mockStations, mockPass, "", false);
        }


        [Test]
        public void Save_Offset_zero()
        {
            Setup();
            var expected = default(TimeSpan);
            var actual = route.ToWorkTime(12, 2021);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Save_Offset_5minutes()
        {
            Setup();
            var expected = new TimeSpan(0, 4, 59);
            var actual = route.ToWorkTime(12, 2020);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Save_Offset_8Hour20Minutes()
        {
            Setup();
            var expected = new TimeSpan(8, 20, 0);
            var actual = route.ToWorkTime(01, 2021);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_Normal_True()
        {
            Setup();
            var expected = true;
            var actual = route.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_DepatureStationEmpty_False()
        {
            Setup();
            var expected = false;
            var actual = otherRoute.Check();
            Assert.AreEqual(expected, actual);
        }




    }
}
