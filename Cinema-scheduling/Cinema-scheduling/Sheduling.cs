using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class Sheduling
    {
        public int EmptyTime { get; set; }
        public List<Film> Films { get; set; }

        public Sheduling(int emptyTime, List<Film> currentFilms = null)
        {
            EmptyTime = emptyTime;
            Films = currentFilms;
        }

        public  override string ToString()
        {
            StringBuilder sheduling = new StringBuilder();
            int timeStartFilm = Cinema.TimeOpen;

            foreach (Film film in Films)
            {
                int timeEndFilm = timeStartFilm + film.Duration;
                string str = $"{ConvertToTime(timeStartFilm)} - {ConvertToTime(timeEndFilm)} {film.Name}\n";

                sheduling.Append(str);
            }

            return sheduling.ToString();
        }

        private string ConvertToTime(int value)
        {
            var hour = value / 60;
            var minute = value % 60;

            return $"{hour}:{minute}";
        }
    }
}
