using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Schedule : IComparable
    {
        public int EmptyTime { get; set; }
        public int CountUniqueFilm { get; set; }
        public List<Film> Films { get; set; }

        public Schedule(int emptyTime, List<Film> currentFilms)
        {
            EmptyTime = emptyTime;
            Films = currentFilms;
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
                string str = $" {ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Name}\n";
                timeStartFilm = timeEndFilm;

                schedule.Append(str);
            }

            return schedule.ToString();
        }

        private string ConvertToTime(int value)
        {
            int hour = value / 60;
            int minute = value % 60;
            string strMinute = minute == 0 ? "00" : minute.ToString();

            return $"{hour}:{strMinute}";
        }
    }
}
