using System.Collections.Generic;
using NUnit.Framework;

namespace Cinema_scheduling.Tests
{
    public class ScheduleTests
    {
        [TestCase(200, 0, "")]
        [TestCase(200, 1, "10:00 - 11:40 Some Film 1")]
        [TestCase(200,2, "10:00 - 11:40 Some Film 1 \n11:40 - 13:20 Some Film 2")]
        public void ToString_WhenValidTestPassed_SchouldReturnStringCurrentSchedule(int actualEmptyTime, int countFilms, string expected)
        {
            List<Film> actualFilms = GetFilms(countFilms);
            Schedule actualSchedule = new Schedule(actualEmptyTime, actualFilms);

            string actual = actualSchedule.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(100,1,100,1,0)]
        [TestCase(500, 3, 500, 1, -1)]
        [TestCase(500, 3, 500, 5, 1)]
        public void CompareTo_WhenValidTestPassed_ShouldReturnOneOrMinusOneOrZero(int actualEmptyTime,int actualCountFilms,int expectedEmptyTime,int expectedCountFilm,int expected)
        {
            List<Film> actualFilms = GetFilms(actualCountFilms);
            Schedule actualSchedule = new Schedule(actualEmptyTime, actualFilms);
            List<Film> expectedFilms = GetFilms(expectedCountFilm);
            Schedule expectedSchedule = new Schedule(expectedEmptyTime, expectedFilms);
            
            int actual = actualSchedule.CompareTo(expectedSchedule);

            Assert.AreEqual(expected, actual);
        }

        private List<Film> GetFilms(int count)
        {
            List<Film> films = new List<Film>();

            for (int i = 0; i < count; i++)
            {
                films.Add(new Film(100, $"Some Film {i + 1}"));
            }

            return films;
        }
    }
}
