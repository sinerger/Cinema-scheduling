using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling
{
    public interface IGraphCreator
    {
        License License { get; set; }
        int EmptyTime { get; set; }
        List<Film> CurrentFilms { get; set; }
        List<Node> Next { get; set; }

        void CreateGraph(Film lastFilm = null);
    }
}
