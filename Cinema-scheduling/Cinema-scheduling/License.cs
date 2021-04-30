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

        private License()
        {
            _license = new License();
            Films = new List<Film>()
            {
                new Film(100,"Fast & Furious"),
                new Film(90,"Fast & Furious 2"),
                new Film(110,"Fast & Furious 3"),
                new Film(180,"Fast & Furious 4"),
                new Film(120,"Fast & Furious 5"),
                new Film(135,"Fast & Furious 6"),
            };
        }

        public static License GetLicense()
        {
            if(_license is null)
            {
                _license = new License();
            }

            return _license;
        }
    }
}
