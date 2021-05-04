using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Schedule : IComparable
    {
        private License _license;
        public int EmptyTime { get; set; }
        public int CountUniqueFilm { get; set; }
        public List<Film> Films { get; set; }

        public Schedule(int emptyTime)
        {
            _license = License.GetLicense();
            EmptyTime = emptyTime;
            Films = new List<Film>();
        }

        public Schedule(int emptyTime, List<Film> currentFilms)
        {
            _license = License.GetLicense();
            EmptyTime = emptyTime;
            Films = currentFilms;
            CountUniqueFilm = GetCountUniqueFilms();
        }

        public bool AddFilm(Film film)
        {
            bool result = false;
            if (EmptyTime >= film.Duration)
            {
                Films.Add(film);
            }

            return result;
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
                    if (Films.Count < schedule.Films.Count)
                    {
                        result = -1;
                    }
                    else if (Films.Count > schedule.Films.Count)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
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
                string str = $" {ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Title}\n";
                timeStartFilm = timeEndFilm;

                schedule.Append(str);
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
    }
}
