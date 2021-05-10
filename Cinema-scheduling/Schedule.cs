using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Schedule : IComparable
    {
        private int _maxEmptyTime;
        public int Rating
        {
            get
            {
                return GetRating();
            }
        }
        public int EmptyTime
        {
            get
            {
                return GetEmptyTime();
            }
        }
        public int CountUniqueFilm
        {
            get
            {
                return GetCountUniqueFilms();
            }
        }

        public List<Film> Films { get; set; }

        public Schedule()
        {
            Films = new List<Film>();
        }

        public Schedule(Schedule schedule)
        {
            _maxEmptyTime = schedule._maxEmptyTime;
            Films = new List<Film>(schedule.Films);
        }

        public Schedule(int emptyTime, List<Film> currentFilms = null)
        {
            if (currentFilms == null)
            {
                Films = new List<Film>();
            }
            else
            {
                Films = currentFilms;
            }

            _maxEmptyTime = emptyTime;
        }

        public bool AddFilm(Film film)
        {
            if (film != null)
            {
                bool result = false;

                if (EmptyTime >= film.Duration)
                {
                    Films.Add(new Film(film));
                    result = true;
                }

                return result;
            }

            throw new ArgumentException("Film is null");
        }

        public void RemoveFilm(Film film)
        {
            if (film != null)
            {
                if (Films != null)
                {
                    Films.Remove(film);
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is Schedule)
            {
                int result = 1;
                Schedule schedule = (Schedule)obj;

                if (EmptyTime < schedule.EmptyTime)
                {
                    result = -1;
                }
                else if (EmptyTime == schedule.EmptyTime)
                {
                    result = CountUniqueFilm.CompareTo(schedule.CountUniqueFilm);
                }

                return result;
            }

            throw new ArgumentException("Incorrect type");
        }

        public override string ToString()
        {
            StringBuilder schedule = new StringBuilder();
            int timeStartFilm = Cinema.TimeOpen;
            foreach (Film film in Films)
            {
                int timeEndFilm = timeStartFilm + film.Duration;
                schedule.Append($"\n{ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Title}");
                timeStartFilm = timeEndFilm;
            }

            return schedule.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Schedule)
            {
                bool result = true;
                Schedule schedule = (Schedule)obj;

                if (Films.Count == schedule.Films.Count)
                {
                    for (int i = 0; i < schedule.Films.Count; i++)
                    {
                        if (!schedule.Films[i].Equals(Films[i]))
                        {
                            result = false;

                            break;
                        }
                    }
                }
                else
                {
                    result = false;
                }

                return result;
            }
            throw new ArgumentException("Incorrect type");
        }

        private string ConvertToTime(int value)
        {
            int hour = value / 60;
            int minute = value % 60;
            string strMinute = minute == 0 ? "00" : minute.ToString();

            return $"{hour}:{strMinute}";
        }

        private int GetCountUniqueFilms()
        {
            List<Film> allFilms = new List<Film>(Cinema.AllFilm);
            int countUniqueFilms = 0;
            foreach (Film film in Films)
            {
                if (allFilms.Contains(film))
                {
                    countUniqueFilms++;
                    allFilms.Remove(film);
                }
            }
            return countUniqueFilms;
        }

        private int GetEmptyTime()
        {
            int emptytime = _maxEmptyTime;

            foreach (Film film in Films)
            {
                emptytime -= film.Duration;
            }

            return emptytime;
        }

        private int GetRating()
        {
            int result = 0 - EmptyTime;
            result += CountUniqueFilm * 10;

            return result;
        }
    }
}
