using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Cinema_scheduling.Tests
{
    public class FilmTests
    {
        [TestCase("Some film",90,"1:30 Some film")]
        [TestCase("Some film", 120, "2:00 Some film")]
        public void ToString_WhenValidTestPasset_ShouldReturnStringFilm(string actualTitle,int actualDuration,string expected)
        {
            Film actualFilm = new Film(actualDuration, actualTitle);

            string actual = actualFilm.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
