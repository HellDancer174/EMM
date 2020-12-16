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
    public class TrainTests
    {
        private Train testTrain;
        private IList<Station> stations;
        private ApiServices services = new ApiServices();
        private IEnumerable<Direction> dir;
        private Directions direction;
        private static Meters[] mockMetersCommon = { new Meters(3, 235, 245, 25, 28, 0, 0) };
        private static Locomotive mockLocCommon = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0)), mockMetersCommon, 1);

        public void Setup()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 25, 28, 0, 0) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0)), mockMeters, 1);
            testTrain = new Train(2, 2803, "Челябинск Б", "Карталы", mockLoc, null, 5800, 284, 71);
            var firstStation = new Station(8, "Челябинск Б", Settings.DefaultDateTime, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Троицк", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Карталы", new DateTime(2020, 04, 20, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            stations = new List<Station>();
            stations.Add(firstStation);
            stations.Add(station);
            stations.Add(lastStation);

        }

        public void Setup2()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 25, 28, 0, 0) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0)), mockMeters, 1);
            testTrain = new Train(2, 2803, "Челябинск Б", "Кропачево", mockLoc, null, 5800, 284, 71);
            var firstStation = new Station(8, "Челябинск Б", Settings.DefaultDateTime, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Златоуст", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Кропачево", new DateTime(2020, 04, 20, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            stations = new List<Station>();
            stations.Add(firstStation);
            stations.Add(station);
            stations.Add(lastStation);
        }
        public void Setup3()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 25, 28, 0, 0) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0)), mockMeters, 1);
            testTrain = new Train(2, 2803, "Кропачево", "Челябинск Б", mockLoc, null, 5800, 284, 71);
            var firstStation = new Station(8, "Кропачево", Settings.DefaultDateTime, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Златоуст", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Челябинск Б", new DateTime(2020, 04, 20, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            stations = new List<Station>();
            stations.Add(firstStation);
            stations.Add(station);
            stations.Add(lastStation);
        }

        public void Setup4()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 25, 28, 0, 0) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0)), mockMeters, 1);
            testTrain = new Train(2, 2803, "Златоуст", "Челябинск Г", mockLoc, null, 5800, 284, 71);
            var firstStation = new Station(8, "Кропачево", Settings.DefaultDateTime, new DateTime(2020, 04, 20, 2, 1, 0), default(TimeSpan));
            var station = new Station(9, "Златоуст", new DateTime(2020, 04, 20, 4, 10, 0), new DateTime(2020, 04, 20, 5, 15, 0), default(TimeSpan));
            var lastStation = new Station(9, "Челябинск Г", new DateTime(2020, 04, 20, 7, 30, 0), Settings.DefaultDateTime, default(TimeSpan));
            stations = new List<Station>();
            stations.Add(firstStation);
            //stations.Add(station);
            stations.Add(lastStation);
        }

        public void Setup5()
        {
            Meters[] mockMeters = { new Meters(3, 235, 245, 25, 28, 0, 0) };
            var mockLoc = new Locomotive("2ЭС6", "207", new BackgroundTime(2, new DateTime(2020, 07, 6, 23, 38, 0), Settings.DefaultDateTime, Settings.DefaultDateTime, new DateTime(2020, 07, 7, 4, 10, 0)), mockMeters, 1);
            testTrain = new Train(2, 2803, "Каменск-Уральский", "Челябинск А", mockLoc, null, 5800, 284, 71);
            var firstStation = new Station(8, "Каменск-Уральский", Settings.DefaultDateTime, new DateTime(2020, 07, 06, 23, 53, 0), default(TimeSpan));
            var station1 = new Station(9, "Нижняя", new DateTime(2020, 07, 07, 0, 40, 0), new DateTime(2020, 07, 07, 0, 40, 0), default(TimeSpan));
            var station2 = new Station(9, "РЗД №6", new DateTime(2020, 07, 07, 2, 05, 0), new DateTime(2020, 07, 07, 2, 16, 0), default(TimeSpan));
            var station3 = new Station(9, "Межозерная", new DateTime(2020, 07, 07, 2, 51, 0), new DateTime(2020, 07, 07, 3, 02, 0), default(TimeSpan));
            var lastStation = new Station(9, "Челябинск А", new DateTime(2020, 07, 7, 3, 31, 0), new DateTime(2020, 07, 07, 0, 0, 0), default(TimeSpan));
            stations = new List<Station>();
            stations.Add(firstStation);
            stations.Add(station1);
            stations.Add(station2);
            stations.Add(station3);
            stations.Add(lastStation);
        }





        [Test]
        public async Task ToTechnicalSpeed_Normal_59_32Async()
        {
            dir = await services.GetDirectionsAsync();
            direction = new Directions(dir.ToList());
            Setup();
            var expected = "Поезд №2803 - 59,32 км/ч";
            var actual = testTrain.ToTechnicalSpeed(stations, direction).ToString();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ToTechnicalSpeed_2TechSpeed_74_42_71_11()
        {
            dir = await services.GetDirectionsAsync();
            direction = new Directions(dir.ToList());
            Setup2();
            var expected = "Поезд №2803 - 74,42 км/ч/71,11 км/ч";
            var actual = testTrain.ToTechnicalSpeed(stations, direction).ToString();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ToTechnicalSpeed_2TechSpeedReverse_74_42_71_11()
        {
            dir = await services.GetDirectionsAsync();
            direction = new Directions(dir.ToList());
            Setup3();
            var expected = "Поезд №2803 - 74,42 км/ч/71,11 км/ч";
            var actual = testTrain.ToTechnicalSpeed(stations, direction).ToString();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task ToTechnicalSpeed_WithOutMiddleStation_Empty()
        {
            dir = await services.GetDirectionsAsync();
            direction = new Directions(dir.ToList());
            Setup4();
            var expected = "Поезд №2803 - 0 км/ч";
            var actual = testTrain.ToTechnicalSpeed(stations, direction).ToString();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task ToTechnicalSpeed_Nizhnya_48_72()
        {
            dir = await services.GetDirectionsAsync();
            direction = new Directions(dir.ToList());
            Setup5();
            var expected = "Поезд №2803 - 48,72 км/ч";
            var actual = testTrain.ToTechnicalSpeed(stations, direction).ToString();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_EmptyArraval_False()
        {
            var train = new Train(1, 2803, String.Empty, "Карталы", mockLocCommon, null, 3500, 284, 71);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_NullArraval_False()
        {
            var train = new Train(1, 2803, null, "Карталы", mockLocCommon, null, 3500, 284, 71);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_WithoutNumber_False()
        {
            var train = new Train(1, 0, "Челябинск", "Карталы", mockLocCommon, null, 3500, 284, 71);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_WithoutWeight_False()
        {
            var train = new Train(1, 2803, "Челябинск", "Карталы", mockLocCommon, null, 0, 284, 71);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_WithoutAxis_False()
        {
            var train = new Train(1, 2803, "Челябинск", "Карталы", mockLocCommon, null, 3500, 0, 71);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_WithoutLength_False()
        {
            var train = new Train(1, 2803, "Челябинск", "Карталы", mockLocCommon, null, 3500, 284, 0);
            var expected = false;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_Reserve_True()
        {
            var train = new Train(1, 4803, "Челябинск", "Карталы", mockLocCommon, null, 0, 0, 0);
            var expected = true;
            var actual = train.Check();
            Assert.AreEqual(expected, actual);
        }








    }
}
