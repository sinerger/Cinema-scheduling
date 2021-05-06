using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Cinema
    {
        private License _license;
        private static int _timeOpen = 600;
        private static int _timeClosed = 1440;
        private List<Schedule> _bestSchedules;
        private List<Hall> _halls;
        private int _countHall;

        public static int TimeOpen
        {
            get
            {
                return _timeOpen;
            }
            private set
            {
                _timeOpen = value;
            }
        }
        public static int TimeClosed
        {
            get
            {
                return _timeClosed;
            }
            private set
            {
                _timeClosed = value;
            }
        }
        public int CountHall
        {
            get
            {
                return _halls.Count;
            }
            private set
            {
                _countHall = value >= 0 ? value : 0;
            }
        }
        public List<Hall> Halls
        {
            get
            {
                return _halls;
            }
            private set
            {
                _halls = value;
            }
        }

        public Cinema(int countHall)
        {
            _license = License.GetLicense();
            _bestSchedules = new List<Schedule>();
            Halls = new List<Hall>();

            for (int i = 0; i < countHall; i++)
            {
                Halls.Add(new Hall("Hall"));
            }
        }

        public void SetSchedulesForHalls()
        {
            SetSchedules(new Schedule(TimeClosed - TimeOpen));
            int indexSchedules = 0;

            foreach (var hall in Halls)
            {
                hall.Schedule = _bestSchedules[indexSchedules];
                indexSchedules++;
            }
        }

        public void SetSchedulesForHalls(List<Schedule> schedules)
        {
            if (schedules != null)
            {
                if (schedules.Count >= _halls.Count)
                {
                    int index = 0;
                    foreach (Hall hall in _halls)
                    {
                        hall.Schedule = schedules[index++];
                    }
                }
            }
        }

        public void AddHall(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Halls.Add(new Hall());
            }
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Cinema)
            {
                Cinema cinema = (Cinema)obj;
                if (Halls.Count == cinema.Halls.Count)
                {
                    result = true;

                    for (int i = 0; i < Halls.Count; i++)
                    {
                        if (!Halls[i].Equals(cinema.Halls[i]))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (Hall hall in Halls)
            {
                result.Append(hall.ToString());
            }

            return result.ToString();
        }

        private void SetSchedules(Schedule currentSchedule)
        {
            foreach (Film film in _license.Films)
            {
                bool isAddedFilm = false;

                if (currentSchedule.AddFilm(film))
                {
                    SetSchedules(currentSchedule);
                    isAddedFilm = true;
                }

                SetBestSchedules(currentSchedule);

                if (isAddedFilm)
                {
                    currentSchedule.RemoveFilm(film);
                }
            }
        }

        private void SetBestSchedules(Schedule currentSchedule)
        {
            if (_bestSchedules != null)
            {
                if (_bestSchedules.Count < CountHall)
                {
                    if (!_bestSchedules.Contains(currentSchedule))
                    {
                        _bestSchedules.Add(new Schedule(currentSchedule));
                    }
                }

                _bestSchedules.Sort();
                if ((TimeClosed - TimeOpen * CountHall) >= _license.GetAllDurationFilms())
                {
                    foreach (Schedule schedule in _bestSchedules)
                    {
                        if (currentSchedule.EmptyTime < schedule.EmptyTime && currentSchedule.CountUniqueFilm > schedule.CountUniqueFilm)
                        {
                            _bestSchedules.Add(new Schedule(currentSchedule));
                            _bestSchedules.Remove(schedule);
                            break;
                        }
                    }

                    //List<Film> allFilms = new List<Film>(_license.Films);
                    //foreach (Schedule schedule in _bestSchedules)
                    //{
                    //    foreach (Film film in schedule.Films)
                    //    {
                    //        if (allFilms.Contains(film))
                    //        {
                    //            allFilms.Remove(film);
                    //        }
                    //    }
                    //}
                    //if (allFilms.Count != 0)
                    //{
                    //    _bestSchedules.Sort();
                    //    _bestSchedules.RemoveAt(_bestSchedules.Count - 1);
                    //}

                }

            }
        }

        private string GetSchedulesAllHalls()
        {
            if (Halls != null)
            {
                StringBuilder result = new StringBuilder();

                foreach (Hall hall in Halls)
                {
                    result.Append($"Hall #{hall.Title}\n{hall.GetSheduling()}\n");
                }

                return result.ToString();
            }

            throw new ArgumentNullException("List Halls is null");
        }
    }
}
