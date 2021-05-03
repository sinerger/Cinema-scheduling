using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling.GraphCreator
{
    class GraphCreatorForBestFillingSeveralHalls : IGraphCreator
    {
        public License License { get ; set ; }
        public int EmptyTime { get ; set ; }
        public List<Film> CurrentFilms { get ; set; }
        public List<Node> Next { get ; set ; }

        public void CreateGraph(Film lastFilm = null)
        {
            
        }
    }
}
