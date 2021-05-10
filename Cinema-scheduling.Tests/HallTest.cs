using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Cinema_scheduling.Tests
{
    public class HallTest
    {
        [TestCase("Hall Empty time: 0; Count unique films: 0; ")]
        public void ToString_WhenValidTestPasset_ShouldReturnStringScheduleInHall(string expected)
        {
            Hall actualHall = new Hall("Hall");
            string actual = actualHall.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
