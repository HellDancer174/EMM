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
    public class DateTimeCalcTests
    {
        [Test]
        public void CalcNightTime_SameDays_0()
        {
            var calc = new DateTimeCalc();
            double expected = 0;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 15, 6, 0, 0), new DateTime(2020, 08, 15, 18, 0, 0));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcNightTime_SameDays_2()
        {
            var calc = new DateTimeCalc();
            double expected = 2;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 15, 2, 0, 0), new DateTime(2020, 08, 15, 14, 0, 0));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcNightTime_SameDays_3()
        {
            var calc = new DateTimeCalc();
            double expected = 3;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 15, 11, 0, 0), new DateTime(2020, 08, 15, 23, 0, 0));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcNightTime_DifferentDays_7()
        {
            var calc = new DateTimeCalc();
            double expected = 7;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 14, 15, 0, 0), new DateTime(2020, 08, 15, 3, 0, 0));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcNightTime_DifferentDays_545()
        {
            var calc = new DateTimeCalc();
            double expected = 5.45;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 14, 22, 33, 0), new DateTime(2020, 08, 15, 10, 0, 0));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CalcNightTime_OnlyNight_8()
        {
            var calc = new DateTimeCalc();
            double expected = 8;
            var actual = calc.CalcNightTime(new DateTime(2020, 08, 14, 20, 00, 0), new DateTime(2020, 08, 15, 4, 0, 0));
            Assert.AreEqual(expected, actual);
        }





    }
}
