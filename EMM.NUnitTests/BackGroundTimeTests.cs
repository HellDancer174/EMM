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
    public class BackGroundTimeTests
    {
        private static readonly DateTime inspection = new DateTime(2020, 04, 20, 1, 20, 0);
        private static readonly DateTime cPExit = new DateTime(2020, 04, 20, 1, 49, 0);
        private static readonly DateTime cPEntrance = new DateTime(2020, 04, 20, 8, 00, 0);
        private static readonly DateTime change = new DateTime(2020, 04, 20, 8, 10, 0);
        [Test]
        public void Check_EmptyInspection_False()
        {
            var mock = new BackgroundTime(-1, Settings.DefaultDateTime, cPExit, cPEntrance, change);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Check_EmptyChange_False()
        {
            var mock = new BackgroundTime(-1, inspection, cPExit, cPEntrance, Settings.DefaultDateTime);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Check_InspectionBiggerChange_False()
        {
            var mock = new BackgroundTime(-1, change, cPExit, cPEntrance, inspection);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Check_InspectionBiggerChangeWithoutCP_False()
        {
            var mock = new BackgroundTime(-1, change, Settings.DefaultDateTime, Settings.DefaultDateTime, inspection);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);

        }
        [Test]
        public void Check_InspectionEqualsChange_False()
        {
            var mock = new BackgroundTime(-1, inspection, Settings.DefaultDateTime, Settings.DefaultDateTime, inspection);
            var expected = false;
            var actual = mock.Check();
            Assert.AreEqual(expected, actual);

        }





    }
}
