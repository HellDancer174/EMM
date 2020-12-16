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
    public class DirectionTests
    {
        private Direction emptyDirection;
        public DirectionTests()
        {
            emptyDirection = new Direction(new string[0], new string[0], String.Empty, String.Empty, String.Empty, String.Empty);
        }
        [Test]
        public void ToTechStations_EmptyDirection_Empty()
        {
            var expected = Tuple.Create("", "");
            var actual = emptyDirection.ToTechStations(new List<string>() {"Челябинск Б","Миасс","Златоуст" });
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void ReverseStation_EmptyDirection_Empty()
        {
            var expected = String.Empty;
            var actual = emptyDirection.ReverseStation(new List<string>() { "Челябинск Б", "Миасс", "Златоуст", "Челябинск Г" });
            Assert.AreEqual(expected, actual);
        }


    }
}
