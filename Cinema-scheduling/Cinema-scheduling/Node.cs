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

            if(currentFilms is null)
            {
                CurrentFilms = new List<Film>();
            }
            else
            {
                CurrentFilms = currentFilms;
            }
        }

        public void CreateGraph()
        {
            foreach (Film film in License.Films)
            {
                if (EmptyTime > film.Duration)
                {
                    List<Film> tempFilmList = new List<Film>(CurrentFilms);
                    tempFilmList.Add(film);
                    Node newNode = new Node(EmptyTime - film.Duration, tempFilmList);
                    Next.Add(newNode);
                    newNode.CreateGraph();
                }
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

                List<Schedule> suitableSchedule = GetBestSheduling(schedules);
                if (suitableSchedule.Count > 0)
                {
                    Schedule minSchedule = suitableSchedule[0];

                    foreach (Schedule schedule in suitableSchedule)
                    {
                        if (minSchedule.EmptyTime >= schedule.EmptyTime)
                        {
                            minSchedule = schedule;
                        }
                        else if ((minSchedule.EmptyTime == schedule.EmptyTime) && (minSchedule.Films.Count > schedule.Films.Count))
                        {
                            minSchedule = schedule;
                        }
                    }
                    return minSchedule;
                }
                else
                {
                    Schedule minSchedule = schedules[0];

                    foreach (Schedule schedule in schedules)
                    {
                        if (minSchedule.EmptyTime >= schedule.EmptyTime)
                        {
                            minSchedule = schedule;
                        }
                        else if ((minSchedule.EmptyTime == schedule.EmptyTime) && (minSchedule.Films.Count > schedule.Films.Count))
                        {
                            minSchedule = schedule;
                        }
                    }

                    return minSchedule;
                }
            }
        }
        
        private List<Schedule> GetBestSheduling(List<Schedule> schedules)
        {
            if (schedules != null)
            {
                List<Schedule> suitableSchedule = new List<Schedule>();

                foreach (Schedule schedule in schedules)
                {
                    if (schedule.Films.Count >= 2)
                    {
                        Film lastFilm = schedule.Films[0];
                        bool isSuitableSchedule = true;

                        for (int i = 1; i < schedule.Films.Count; i++)
                        {
                            if (lastFilm.Equals(schedule.Films[i]))
                            {
                                isSuitableSchedule = false;
                                break;
                            }

                            lastFilm = schedule.Films[i];
                        }

                        if (isSuitableSchedule)
                        {
                            suitableSchedule.Add(schedule);
                        }
                    }
                }

                return suitableSchedule;
            }

            throw new ArgumentNullException("Schedule is null");
        }
    }
}
