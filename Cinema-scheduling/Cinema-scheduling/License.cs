using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public class License
    {
        private static License _license;
        public List<Film> Films { get; set; }
        public List<Schedule> AllSchedules { get; set; }
        public int AverageDuration { get; set; }
        private License()
        {
            Films = new List<Film>()
            {
                new Film(150,"Fast & Furious"),
                new Film(110,"Fast & Furious 2"),
                new Film(120,"Fast & Furious 3"),
                new Film(180,"Fast & Furious 4"),
                new Film(120,"Fast & Furious 5"),
                new Film(160,"Fast & Furious 6"),
                new Film(200,"Fast & Furious 7"),
                new Film(145,"Fast & Furious 8"),
                new Film(245,"Fast & Furious 9"),
                new Film(120,"Fast & Furious 10"),
                new Film(200,"Fast & Furious 11")
            };

            Films.Sort();
            Films.Reverse();

            int temp = 0;
            foreach (Film film in Films)
            {
                temp += film.Duration;
            }
            AverageDuration = temp / Films.Count;
        }

        public static License GetLicense()
        {
            if(_license is null)
            {
                _license = new License();
            }

            return _license;
        }

        public int GetAllDurationFilms()
        {
            int result = 0;
            foreach (Film film in Films)
            {
                result += film.Duration;
            }

            return result;
        }

        
    }
}
