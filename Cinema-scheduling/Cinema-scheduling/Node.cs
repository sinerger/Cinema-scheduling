using Cinema_scheduling.GraphCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Node
    {
        public IGraphCreator GraphCreator { get; set; }
        public License License { get; set; }
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
            GraphCreator.CreateGraph(lastFilm);
        }

        public List<Schedule> GetListSchedulesforHalls(int countHalls)
        {
            if (License.Films != null)
            {
                List<Film> remainingFilms = new List<Film>(License.Films);
                List<Schedule> allSchedules = GetAllSchedules();
                List<Schedule> resultList = new List<Schedule>();

                for (int i = 0; i < allSchedules.Count; i++)
                {
                    if (resultList.Count < countHalls)
                    {
                        if (CheckSchedule(allSchedules[i], remainingFilms))
                        {
                            resultList.Add(allSchedules[i]);

                            if (resultList.Count == countHalls && !CheckListSchedule(resultList))
                            {
                                resultList.RemoveAt(resultList.Count - 1);
                            }
                            else
                            {
                                foreach (Film currentFilm in allSchedules[i].Films)
                                {
                                    if (remainingFilms.Contains(currentFilm))
                                    {
                                        remainingFilms.Remove(currentFilm);
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return resultList;

            }

            throw new ArgumentNullException("List License.Films is null");
        }

        public List<Schedule> GetListSchedulesMaxUniqueFilmForHalls(int countHalls)
        {
            if (License.Films != null)
            {
                List<Film> remainingFilms = new List<Film>(License.Films);
                List<Schedule> allSchedules = GetAllSchedules();
                List<Schedule> resultList = new List<Schedule>();

                for (int i = 0; i < allSchedules.Count; i++)
                {
                    if (resultList.Count < countHalls)
                    {
                        if (CheckSchedule(allSchedules[i], remainingFilms))
                        {
                            resultList.Add(allSchedules[i]);

                            foreach (Film currentFilm in allSchedules[i].Films)
                            {
                                if (remainingFilms.Contains(currentFilm))
                                {
                                    remainingFilms.Remove(currentFilm);
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return resultList;

            }

            throw new ArgumentNullException("List License.Films is null");
        }

        private bool CheckSchedule(Schedule currentSchedule, List<Film> remainingFilms)
        {
            if (currentSchedule != null && remainingFilms != null)
            {
                bool result = false;
                int countFilms = 0;

                foreach (Film film in currentSchedule.Films)
                {
                    if (remainingFilms.Contains(film))
                    {
                        countFilms++;
                    }
                }

                if (countFilms > remainingFilms.Count / 2 || remainingFilms.Count == 0)
                {
                    result = true;
                }

                return result;
            }
            else if (remainingFilms == null)
            {
                throw new ArgumentNullException("List remainingFilms is null");
            }
            else
            {
                throw new ArgumentNullException("Schedule is null");
            }
        }

        private bool CheckListSchedule(List<Schedule> schedules)
        {
            if (License.Films != null)
            {
                bool result = false;
                List<Film> allFilms = new List<Film>(License.Films);

                foreach (Schedule schedule in schedules)
                {
                    foreach (Film currentFilm in schedule.Films)
                    {
                        if (allFilms.Contains(currentFilm))
                        {
                            allFilms.Remove(currentFilm);
                        }
                    }
                }

                if (allFilms.Count == 0)
                {
                    result = true;
                }

                return result;
            }

            throw new ArgumentNullException("List License.Films is null");
        }

        private List<Schedule> GetAllSchedules()
        {
            if (Next.Count == 0)
            {
                return new List<Schedule>() { new Schedule(EmptyTime, CurrentFilms) };
            }
            else
            {
                List<Schedule> allSchedules = new List<Schedule>();

                foreach (Node n in Next)
                {
                    allSchedules.AddRange(n.GetAllSchedules());
                }

                allSchedules.Sort();

                return allSchedules;
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
    }
}
