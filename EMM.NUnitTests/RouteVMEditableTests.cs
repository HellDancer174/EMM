using EMM.Models;
using EMM.Services;
using EMM.ViewModels;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;

namespace EMM.NUnitTests
{
    [TestFixture]
    public class RouteVMEditableTests
    {
        RouteVMEditable routeVM;
        DateTime DateTimeDefault = new DateTime(1837, 11, 11, 00, 00, 00);
        Route route;


        public void Setup()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 0, 0, 154, 158)};
            var mockLoc = new Locomotive("ЭП2К", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), DateTimeDefault, new DateTime(2020, 04, 20, 8, 10, 0)), mockMeters, 1);
            var mockTrain = new Train(2, 4301, "Челябинск П", "Карталы", mockLoc, null, 0, 0, 0);
            var firstStation = new Station(8, "Челябинск П", DateTimeDefault, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Троицк", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Карталы", new DateTime(2020, 04, 20, 7, 30, 0), DateTimeDefault, default(TimeSpan));
            var mockLocs = new List<Locomotive>();
            var mockTrains = new List<Train>();
            var mockStations = new List<Station>();
            var mockPass = new List<Passanger>();
            mockLocs.Add(mockLoc);
            mockTrains.Add(mockTrain);
            mockStations.Add(firstStation);
            mockStations.Add(station);
            mockStations.Add(lastStation);
            route = new Route(1, new DateTime(2020, 04, 20, 1, 0, 0), new DateTime(2020, 04, 20, 8, 20, 0), mockLocs, mockTrains, mockStations, mockPass, "", false);
            routeVM = new RouteVMEditable(route);
        }
        [Test]
        public void Save_Normal_True()
        {
            Setup();
            var expected = true;
            var actual = routeVM.Save();
            Assert.AreEqual(expected, actual);
        }
    }
}
