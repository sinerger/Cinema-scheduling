using Cinema_scheduling.GraphCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_scheduling.Factory
{
    class NodeForBestFillingHallFactory : INodeFactory
    {
        public Node GetNode(int emptyTime, List<Film> currentFilm = null)
        {
            Node node = new Node(emptyTime, currentFilm);
            node.GraphCreator = new GraphCreatorForBestFillingHall(node.EmptyTime, node.CurrentFilms, node.Next);
            return node;
        }
    }
}
