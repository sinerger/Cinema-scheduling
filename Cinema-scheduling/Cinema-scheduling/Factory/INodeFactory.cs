using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling.Factory
{
    public interface INodeFactory
    {
        Node GetNode(int emptyNime, List<Film> currentFilm = null);
    }
}
