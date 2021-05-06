using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Cinema_scheduling.Tests
{
    public class CinemaTests
    {
        [TestCaseSource(typeof(CinemaCreated))]
        public void CinemaConstructor_WhenValidTestPassed_ShouldCreateNewObjectCimenaWithEmptyHalls(int countHall, Cinema expected)
        {
            Cinema actual = new Cinema(countHall);

            Assert.AreEqual(expected, actual);
        }

        public class CinemaCreated : IEnumerable
        {
            int countHall;

            public IEnumerator GetEnumerator()
            {
                yield return new object[]
                {
                    countHall = -10,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = -1,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 0,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 1,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 2,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 3,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 4,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 5,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 10,
                    new Cinema(countHall)
                };

                yield return new object[]
                {
                    countHall = 100,
                    new Cinema(countHall)
                };
            }
        }

        [TestCaseSource(typeof(SetTwoSchedules))]
        public void SetSchedulesForHalls_WhenValidTestPassed_ShouldSetTwoSchedules(Cinema expected, Cinema actual)
        {
            actual.SetSchedulesForHalls();

            Assert.AreEqual(expected, actual);
        }

        public class SetTwoSchedules : IEnumerable
        {
            //private int _countHall = 2;

            Cinema actual = new Cinema(2);
            Cinema expected = new Cinema(2);

            //private List<Schedule> _schedules = new List<Schedule>()
            //{
            //    new Schedule(840,new List<Film>()
            //    {
            //        new Film(80,"Fast & Furious"),
            //        new Film(88,"Fast & Furious 2")
            //    }),
            //    new Schedule(840,new List<Film>()
            //    {
            //        new Film(88,"Fast & Furious 2"),
            //        new Film(80,"Fast & Furious")
            //    })
            //};

            public IEnumerator GetEnumerator()
            {
                //expected.SetSchedulesForHalls(_schedules);
                yield return new object[]
                {
                    expected,
                    new Cinema(5)
                };
            }
        }
    }
}