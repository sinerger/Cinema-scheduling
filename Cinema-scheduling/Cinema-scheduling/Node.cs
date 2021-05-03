﻿using Cinema_scheduling.GraphCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Node
    {
        private List<Schedule> _schedules;
        public IGraphCreator GraphCreator { get; set; }
        public License License { get; set; }
        public int EmptyTime { get; set; }
        public List<Film> CurrentFilms { get; set; }
        public List<Node> Next { get; set; }


        public Node(int emptyTime, List<Film> currentFilms = null)
        {
            _schedules = new List<Schedule>();
            EmptyTime = emptyTime;
            Next = new List<Node>();
            License = License.GetLicense();

            if (currentFilms is null)
            {
                CurrentFilms = new List<Film>();
            }
            else
            {
                CurrentFilms = currentFilms;
            }
        }

        public void CreateGraph(Film lastFilm = null)
        {
            GraphCreator.CreateGraph(lastFilm);
        }

        public Schedule FindMinEmptyTimeSchedule()
        {
            if (Next.Count == 0)
            {
                return new Schedule(EmptyTime, CurrentFilms);
            }
            else
            {
                List<Schedule> schedules = new List<Schedule>();

                foreach (Node n in Next)
                {
                    schedules.Add(n.FindMinEmptyTimeSchedule());
                }

                return GetScheduleMaxUniqueFilms(schedules);
            }
        }

        private Schedule GetMinSchedule(List<Schedule> schedules)
        {
            if (schedules.Count > 0)
            {
                Schedule minSchedule = schedules[0];

                foreach (Schedule schedule in schedules)
                {
                    if (minSchedule.EmptyTime > schedule.EmptyTime)
                    {
                        minSchedule = schedule;
                    }
                    else if ((minSchedule.EmptyTime == schedule.EmptyTime) && (minSchedule.Films.Count < schedule.Films.Count))
                    {
                        minSchedule = schedule;
                    }
                }

                return minSchedule;
            }

            throw new ArgumentNullException("List Schedules is empty");
        }

        private Schedule GetScheduleMaxUniqueFilms(List<Schedule> schedules)
        {
            if (schedules != null)
            {
                foreach (Schedule schedule in schedules)
                {
                    foreach (Film film in License.Films)
                    {
                        if (schedule.Films.Contains(film))
                        {
                            schedule.CountUniqueFilm++;
                        }
                    }

                }

                Schedule scheduleMaxUniqueFilm = schedules[0];

                foreach (Schedule schedule in schedules)
                {
                    if (scheduleMaxUniqueFilm.CountUniqueFilm < schedule.CountUniqueFilm)
                    {
                        if (scheduleMaxUniqueFilm.EmptyTime > schedule.EmptyTime)
                        {
                            scheduleMaxUniqueFilm = schedule;
                        }
                    }
                }

                return scheduleMaxUniqueFilm;
            }

            throw new ArgumentNullException("Schedule is null");
        }

        public List<Schedule> GetListSchedule()
        {
            if (Next.Count == 0)
            {
                return new List<Schedule>() { new Schedule(EmptyTime, CurrentFilms) };
            }
            else
            {
                foreach (Node n in Next)
                {
                    _schedules.AddRange(n.GetListSchedule());
                }

                _schedules.Sort();

                return _schedules;
            }
        }

        public List<Schedule> QQQ(int countHall)
        {
            List<Film> films = new List<Film>(License.Films);
            List<Schedule> targetSchedules = new List<Schedule>();

            foreach (Schedule schedule in _schedules)
            {
                if (targetSchedules.Count < countHall)
                {
                    if (www(schedule,films))
                    {
                        targetSchedules.Add(schedule);
                    }
                }
                else
                {
                    break;
                }
            }

            return targetSchedules;
        }

        private bool www(Schedule schedule, List<Film> films)
        {
            int count = 0;
            foreach (Film film in schedule.Films)
            {
                if (films.Contains(film))
                {
                    count++;
                }
            }

            if (count > films.Count / 2)
            {
                foreach (Film film in schedule.Films)
                {
                    if (films.Contains(film))
                    {
                        films.Remove(film);
                    }
                }

                return true;
            }
            else if(films.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public List<Schedule> GetListForHalls(int countHalls)
        //{
        //    List<Schedule> targetSchedules = GetListSchedule();
        //    List<Film> allFilms = new List<Film>(License.Films);

        //    while
        //}

        //private List<Schedule> GetScheduleWithAlternatingFilms(List<Schedule> schedules)
        //{
        //    if (schedules != null)
        //    {
        //        List<Schedule> suitableSchedule = new List<Schedule>();

        //        foreach (Schedule schedule in schedules)
        //        {
        //            if (schedule.Films.Count >= 2)
        //            {
        //                Film lastFilm = schedule.Films[0];
        //                bool isSuitableSchedule = true;

        //                for (int i = 1; i < schedule.Films.Count; i++)
        //                {
        //                    if (lastFilm.Equals(schedule.Films[i]))
        //                    {
        //                        isSuitableSchedule = false;
        //                        break;
        //                    }

        //                    lastFilm = schedule.Films[i];
        //                }

        //                if (isSuitableSchedule)
        //                {
        //                    suitableSchedule.Add(schedule);
        //                }
        //            }
        //        }

        //        return suitableSchedule;
        //    }

        //    throw new ArgumentNullException("Schedule is null");
        //}
    }
}
