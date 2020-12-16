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
    public class LocomotiveTests
    {
        private static Meters[] mockMetersCommon = { new Meters(3, 235, 245, 25, 28, 0, 0) };
        private static BackgroundTime timesCommon = new BackgroundTime(2, new DateTime(2020, 04, 20, 1, 20, 0), new DateTime(2020, 04, 20, 1, 49, 0), Settings.DefaultDateTime, new DateTime(2020, 04, 20, 8, 10, 0));
        [Test]
        public void Check_WithOutName_False()
        {
            var mock = new Locomotive("", "305", timesCommon, mockMetersCommon, 1);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_NullType_False()
        {
            var mock = new Locomotive(null, "305", timesCommon, mockMetersCommon, 1);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_NullNumber_False()
        {
            var mock = new Locomotive("ЭП2К", null, timesCommon, mockMetersCommon, 1);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_EmptyNumber_False()
        {
            var mock = new Locomotive("ЭП2К", "", timesCommon, mockMetersCommon, 1);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check_ZeroSections_False()
        {
            var mock = new Locomotive("ЭП2К", "305", timesCommon, mockMetersCommon, 0);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);
        }



    }
}
