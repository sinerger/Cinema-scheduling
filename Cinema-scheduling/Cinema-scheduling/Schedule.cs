using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Schedule
    {
        public int EmptyTime { get; set; }
        public List<Film> Films { get; set; }

        public Schedule(int emptyTime, List<Film> currentFilms = null)
        {
            EmptyTime = emptyTime;
            Films = currentFilms;
        }

        public  override string ToString()
        {
            StringBuilder schedule = new StringBuilder();
            int timeStartFilm = Cinema.TimeOpen;

            foreach (Film film in Films)
            {
                int timeEndFilm = timeStartFilm + film.Duration;
                string str = $"{ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Name}\n";
                timeStartFilm = timeEndFilm;

                schedule.Append(str);
            }

            return schedule.ToString();
        }

        private string ConvertToTime(int value)
        {
            var hour = value / 60;
            var minute = value % 60;

            return $"{hour}:{minute}";
        }
    }
}
