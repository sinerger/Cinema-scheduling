using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cinema_scheduling.Tests
{
    public class CinemaTests
    {
        //[TestCase(1, 1, 1)]
        [TestCase(2, 2, 2)]
        //[TestCase(3, 3, 3)]
        //[TestCase(4, 4, 4)]
        //[TestCase(5, 5, 5)]
        //[TestCase(6, 6, 6)]
        //[TestCase(2, 50, 2)]
        public void SetSchedulesForHalls_WhenValidTestPassed_ShouldSetTwoSchedules(int countFilm, int countHall, int expectedCountUniqueFilms)
        {
            Cinema actualCinema = GetCinemaForValidTest(countFilm, countHall);
            List<Film> allFilms = new List<Film>(Cinema.AllFilm);
            actualCinema.SetSchedulesForHalls();
            List<Schedule> actualSchedule = actualCinema.GetSchedules();
            int actualCountUniqueFilms = 0;
            int actualEmptyTime = 0;

            foreach (Schedule schedule in actualSchedule)
            {
                actualEmptyTime += schedule.EmptyTime;
                foreach (Film scheduleFilm in schedule.Films)
                {
                    if (allFilms.Contains(scheduleFilm))
                    {
                        allFilms.Remove(scheduleFilm);
                        ++actualCountUniqueFilms;
                    }
                }
            }

            actualEmptyTime = actualEmptyTime / actualSchedule.Count;

            bool allScheduleIsUnique = true;
            List<Schedule> allSchedules = new List<Schedule>();
            foreach (Hall hall in actualCinema.Halls)
            {
                if (allSchedules.Contains(hall.Schedule))
                {
                    allScheduleIsUnique = false;
                    break;
                }

                allSchedules.Add(new Schedule(hall.Schedule));
            }


            Assert.AreEqual(expectedCountUniqueFilms, actualCountUniqueFilms);
            Assert.IsTrue(actualEmptyTime >= 0);
            Assert.IsTrue(allScheduleIsUnique);
        }

        [TestCase(1, "Halls count: 1")]
        [TestCase(2, "Halls count: 2")]
        [TestCase(3, "Halls count: 3")]
        [TestCase(4, "Halls count: 4")]
        public void ToString_WhenValidTestPassed_ShouldReturnStringAllSchedules(int countHall, string expected)
        {
            Cinema actualCinema = GetCinemaForValidTest(countHall, countHall);

            string actual = actualCinema.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 1, true)]
        [TestCase(2, 2, true)]
        [TestCase(1, 2, false)]
        [TestCase(2, 1, false)]
        public void Equals_WhenValidTestPassed_ShouldReturnTrueOrFalse(int countHallActual, int countHallExpected, bool expected)
        {
            Cinema actualCinema = GetCinemaForValidTest(countHallActual, countHallActual);
            Cinema expectedCinema = GetCinemaForValidTest(countHallExpected, countHallExpected);

            bool actual = actualCinema.Equals(expectedCinema);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1,null)]
        public void Create_WhenInvalidTestPassed_ShouldReturnArgumentNullException(int countHall,List<Film>films)
        {
            Assert.Throws<ArgumentNullException>(() => Cinema.Create(countHall, films,600,1440));
        }

        private Cinema GetCinemaForValidTest(int countFilm, int countHall)
        {
            List<Film> films = new List<Film>();
            for (int i = 0; i < countFilm; i++)
            {
                films.Add(new Film(100 /*+ (i * i)*/, $"Some Film {i + 1}"));
            }

            return Cinema.Create(countHall, films,600,1440);
        }
    }
}