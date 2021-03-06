using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Cinema
    {
        private static int _timeOpen;
        private static int _timeClosed;
        private List<Schedule> _bestSchedules;
        private List<Hall> _halls;
        private static List<Film> _allFilm;
        public static List<Film> AllFilm
        {
            get
            {
                return _allFilm;
            }
            private set
            {
                _allFilm = value;
            }
        }
        public static int TimeOpen
        {
            get
            {
                return _timeOpen;
            }
            private set
            {
                _timeOpen = value >= 0 ? value : 0;
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
                _timeClosed = value >= _timeOpen ? value : _timeOpen;
            }
        }
        public int CountHall
        {
            get
            {
                return _halls.Count;
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

        private Cinema(int countHall, List<Film> allFilm, int timeOpen, int timeClosed)
        {
            if (allFilm != null)
            {
                AllFilm = new List<Film>(allFilm);
                AllFilm.Sort();
                AllFilm.Reverse();
            }
            else
            {
                AllFilm = new List<Film>();
            }

            TimeOpen = timeOpen;
            TimeClosed = timeClosed;
            _bestSchedules = new List<Schedule>();
            Halls = new List<Hall>();

            for (int i = 0; i < countHall; i++)
            {
                Halls.Add(new Hall($"Hall { i + 1 }"));
            }
        }

        public static Cinema Create(int countHall, List<Film> allFilm, int timeOpen, int timeClosed)
        {
            if (allFilm != null)
            {
                return new Cinema(countHall, allFilm, timeOpen, timeClosed);
            }

            throw new ArgumentNullException("List films is null");
        }

        public void CreateSchedules()
        {
            SetSchedules(new Schedule(TimeClosed - TimeOpen));
            int indexSchedules = 0;

            foreach (var hall in Halls)
            {
                hall.Schedule = _bestSchedules[indexSchedules];
                indexSchedules += indexSchedules < _bestSchedules.Count - 1 ? 1 : 0;
            }
        }

        public List<Schedule> GetSchedules()
        {
            List<Schedule> schedules = new List<Schedule>();

            if (Halls != null)
            {
                foreach (Hall hall in Halls)
                {
                    schedules.Add(hall.Schedule);
                }
            }

            return schedules;
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
            result.Append($"Halls count: {Halls.Count}");

            return result.ToString();
        }

        private int GetTotalDurationFilms()
        {
            int result = 0;
            foreach (Film film in AllFilm)
            {
                result += film.Duration;
            }

            return result;
        }

        private double GetAverageDurationFilms()
        {
            return GetTotalDurationFilms() / AllFilm.Count;
        }

        private int GetCountUniqueFilmsInBestSchedules()
        {
            List<Film> allFilm = new List<Film>(AllFilm);
            int result = 0;
            foreach (Schedule schedule in _bestSchedules)
            {
                foreach (Film film in schedule.Films)
                {
                    if (allFilm.Count != 0 && allFilm.Contains(film))
                    {
                        allFilm.Remove(film);
                        ++result;
                    }
                    else if (allFilm.Count == 0)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        private void SetSchedules(Schedule currentSchedule)
        {
            if (currentSchedule != null)
            {
                foreach (Film film in AllFilm)
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
            else
            {
                throw new ArgumentNullException("Current schedule is null");
            }
        }

        private void SetBestSchedules(Schedule currentSchedule)
        {
            if (_bestSchedules != null && currentSchedule != null)
            {
                if (!_bestSchedules.Contains(currentSchedule) 
                    && _bestSchedules.Count < CountHall 
                    && currentSchedule.EmptyTime < GetAverageDurationFilms())
                {
                    _bestSchedules.Add(new Schedule(currentSchedule));

                    return;
                }

                if (!_bestSchedules.Contains(currentSchedule))
                {
                    int tempCountUniqueFilms = GetCountUniqueFilmsInBestSchedules();
                    Schedule tempSchedule = null;
                    for (int i = 0; i < _bestSchedules.Count; i++)
                    {
                        if (_bestSchedules[i].Rating <= currentSchedule.Rating)
                        {
                            tempSchedule = _bestSchedules[i];
                            _bestSchedules.Remove(tempSchedule);
                            _bestSchedules.Add(new Schedule(currentSchedule));

                            if (tempCountUniqueFilms >= GetCountUniqueFilmsInBestSchedules())
                            {
                                _bestSchedules.Remove(currentSchedule);
                                _bestSchedules.Add(tempSchedule);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (_bestSchedules == null)
            {
                throw new ArgumentNullException("List best schedules is null");
            }
            else
            {
                throw new ArgumentNullException("Current schedule is null");
            }
        }
    }
}
