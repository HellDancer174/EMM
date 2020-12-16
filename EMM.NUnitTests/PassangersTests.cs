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
    public class PassangersTests
    {
        private Route TestRoute;
        private Passangers testPassangers;
        private DateTime start = new DateTime(2020, 08, 23, 0, 47, 00);
        private DateTime finish = new DateTime(2020, 08, 23, 6, 32, 00);
        private List<Locomotive> locomotives = new List<Locomotive>();
        private List<Passanger> passangers = new List<Passanger>();
        public void Setup()
        {
            TestRoute = new Route(-1, start, finish, locomotives, new List<Train>(), new List<Station>(), passangers, string.Empty, false);
            testPassangers = new Passangers(start, finish, passangers, locomotives);
        }
        [Test]
        public void CalcWaitPassTime_OnlyPass_0()
        {
            var mockPass = new Passanger(-1, 290, new DateTime(2020, 08, 23, 1, 17, 00), new DateTime(2020, 08, 23, 6, 11, 00));
            passangers.Add(mockPass);
            Setup();
            double expected = 0;
            double actual = testPassangers.CalcWaitPassangersTime().TotalHours;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalcPassTime_OnlyPass_4_9()
        {
            var mockPass = new Passanger(-1, 290, new DateTime(2020, 08, 23, 1, 17, 00), new DateTime(2020, 08, 23, 6, 11, 00));
            passangers.Add(mockPass);
            Setup();
            var expected = new TimeSpan(5, 45, 0);
            var actual = testPassangers.CalcPassangersTime();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcPassTime_OnlyPassAndStartBiggerArraval_4_4()
        {
            start = start.AddHours(1);
            var mockPass = new Passanger(-1, 290, new DateTime(2020, 08, 23, 1, 17, 00), new DateTime(2020, 08, 23, 6, 11, 00));
            passangers.Add(mockPass);
            Setup();
            var expected = new TimeSpan(4, 45, 0);
            var actual = testPassangers.CalcPassangersTime();
            Assert.AreEqual(expected, actual);
        }


    }
}
