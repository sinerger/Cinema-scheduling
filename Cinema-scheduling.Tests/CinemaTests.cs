using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema_scheduling.Tests
{
    public class CinemaTests
    {
        [TestCase(1, 1, 1,true)]
        [TestCase(2, 2, 2, true)]
        [TestCase(6, 2, 4, true)]
        [TestCase(7, 3, 6, true)]
        [TestCase(6, 3, 6, true)]
        [TestCase(2, 1, 2, true)]
        [TestCase(10, 2, 4, true)]
        [TestCase(11, 2, 4, true)]
        [TestCase(12, 2, 4, true)]
        [TestCase(15, 2, 4, true)]
        [TestCase(6, 6, 6, true)]
        [TestCase(2, 130, 2,false)]
        public void CreateSchedule_WhenValidTestPassed_ShouldSetTwoSchedules(int countFilm, int countHall, int expectedCountUniqueFilms,bool expectedAllScheduleIsUnique)
        {
            Cinema actualCinema = GetCinemaForValidTest(countFilm, countHall);
            List<Film> allFilms = new List<Film>(Cinema.AllFilm);
            List<Schedule> actualSchedule = new List<Schedule>();

            actualCinema.CreateSchedules();
            actualSchedule = actualCinema.GetSchedules();
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

            bool actualAllScheduleIsUnique = true;
            List<Schedule> allSchedules = new List<Schedule>();
            var temp = !actualCinema.Halls.GroupBy(x => x.Schedule).
                Any(y => y.ToList().Count > 1);

            foreach (Hall hall in actualCinema.Halls)
            {
                if (allSchedules.Contains(hall.Schedule))
                {
                    actualAllScheduleIsUnique = false;
                    break;
                }

                allSchedules.Add(new Schedule(hall.Schedule));
            }

            Assert.AreEqual(actualAllScheduleIsUnique, temp);

            Assert.AreEqual(expectedCountUniqueFilms, actualCountUniqueFilms);
            Assert.IsTrue(actualEmptyTime >= 0);
            Assert.AreEqual(expectedAllScheduleIsUnique,actualAllScheduleIsUnique);
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
                films.Add(new Film(100, $"Some Film {i + 1}"));
            }

            return Cinema.Create(countHall, films,600,800);
        }
    }
}