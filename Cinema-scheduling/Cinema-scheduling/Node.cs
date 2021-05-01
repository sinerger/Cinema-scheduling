using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Node
    {
        public License License;
        public int EmptyTime { get; set; }
        public List<Film> CurrentFilms { get; set; }
        public List<Node> Next { get; set; }
        public Node(int emptyTime, List<Film> currentFilms = null)
        {
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
            if (License.Films != null && License.Films.Count > 0)
            {
                foreach (Film film in License.Films)
                {
                    if (License.Films.Count > 1)
                    {
                        if (EmptyTime >= film.Duration && lastFilm != film)
                        {
                            List<Film> tempFilmList = new List<Film>(CurrentFilms);
                            tempFilmList.Add(film);
                            Node newNode = new Node(EmptyTime - film.Duration, tempFilmList);
                            Next.Add(newNode);
                            newNode.CreateGraph(film);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Incorrect film list");
            }
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

                //List<Schedule> suitableSchedule = GetScheduleWithAlternatingFilms(schedules);

                //var isss = false;
                //if (suitableSchedule.Count > 0 && isss)
                //{
                //    //return GetScheduleMaxUniqueFilms(suitableSchedule);
                //    return GetMinSchedule(suitableSchedule);
                //}
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
                List<Schedule> schedules = new List<Schedule>();

                foreach (Node n in Next)
                {
                    schedules.AddRange(n.GetListSchedule());
                }

                schedules.Sort();
                return schedules;

            }
        }


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
