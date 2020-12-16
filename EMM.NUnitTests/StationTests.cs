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
    public class StationTests
    {
        Station station;
        Station firstStation;
        Station lastStation;
        Station stationWithoutName;
        public StationTests()
        {
            firstStation = new Station(8, "Челябинск П", Settings.DefaultDateTime, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            station = new Station(9, "Троицк", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            lastStation = new Station(9, "Карталы", new DateTime(2020, 04, 20, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            stationWithoutName = new Station(9, "", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
        }
        [Test]
        public void Check_firstStation_True()
        {
            var expected = true;
            var actual = firstStation.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_lastStation_True()
        {
            var expected = true;
            var actual = lastStation.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_station_True()
        {
            var expected = true;
            var actual = station.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_WithoutName_False()
        {
            var expected = false;
            var actual = stationWithoutName.Check();
            Assert.AreEqual(expected, actual);
        }



    }
}
