using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Результа поиска
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Пути
        /// </summary>
        public List<Edge> edges;

        /// <summary>
        /// Длина пути
        /// </summary>
        public double lenght;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SearchResult()
        {
            edges = new List<Edge>();
            lenght = 0;
        }

        public void calk_length()
        {
            lenght = 0;
            foreach (Edge edge in edges) lenght += edge.length;
        }
    }
}
