using Cinema_scheduling.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling.GraphCreator
{
    public class GraphCreatorForBestFillingHall : IGraphCreator
    {
        public License License { get; set; }
        public int EmptyTime { get; set; }
        public List<Film> CurrentFilms { get; set; }
        public List<Node> Next { get; set; }

        public GraphCreatorForBestFillingHall(int emptyTime, List<Film> currentFilms, List<Node> nextNods)
        {
            License = License.GetLicense();
            EmptyTime = emptyTime;
            CurrentFilms = currentFilms;
            Next = nextNods;
        }

        public void CreateGraph(Film lastFilm = null)
        {
            if (License.Films != null && License.Films.Count > 0)
            {
                foreach (Film film in License.Films)
                {
                    if (License.Films.Count > 1)
                    {
                        if (EmptyTime >= film.Duration && lastFilm != film)
                        {
                            List<Film> tempFilmList = new List<Film>(CurrentFilms);
                            tempFilmList.Add(film);
                            Node newNode = new NodeForBestFillingHallFactory().GetNode(EmptyTime - film.Duration, tempFilmList);
                            Next.Add(newNode);
                            newNode.CreateGraph(film);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Incorrect film list");
            }
        }
    }
}
