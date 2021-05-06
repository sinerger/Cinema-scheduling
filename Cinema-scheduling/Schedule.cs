using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema_scheduling
{
    public class Schedule : IComparable
    {
        private int _maxEmptyTime;
        private License _license;
        public int EmptyTime
        {
            get
            {
                return GetEmptyTime();
            }
            private set
            {
                EmptyTime = value;
            }
        }
        public int CountUniqueFilm
        {
            get
            {
                return GetCountUniqueFilms();
            }
            private set
            {
                CountUniqueFilm = value;
            }
        }

        public List<Film> Films { get; set; }

        public Schedule()
        {
            _license = License.GetLicense();
            Films = new List<Film>();
        }

        public Schedule(Schedule schedule)
        {
            _license = License.GetLicense();
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

            _license = License.GetLicense();
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

            throw new ArgumentException();
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
            schedule.Append("\n");
            foreach (Film film in Films)
            {
                int timeEndFilm = timeStartFilm + film.Duration;
                schedule.Append($"{ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Title}\n");
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
                        if (schedule.Films[i] != Films[i])
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
            List<Film> allFilms = new List<Film>(_license.Films);
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
    }
}
