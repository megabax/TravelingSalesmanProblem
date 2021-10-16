using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Точка 1
        /// </summary>
        public Point P1;

        /// <summary>
        /// Точка 2
        /// </summary>
        public Point P2;

        /// <summary>
        /// Длина
        /// </summary>
        public double length;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="p1">Точка 1</param>
        /// <param name="p2">Точка 2</param>
        public Edge(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Создать коипю
        /// </summary>
        /// <returns>Копия</returns>
        public Edge clone()
        {
            Edge edge = new Edge(new Point(P1.X, P1.Y), new Point(P2.X, P2.Y));
            edge.length = length;
           /* edge.P1 = new Point(P1.X, P1.Y);
            edge.P2 = new Point(P2.X, P2.Y);*/
            return edge;
        }

        /// <summary>
        /// Вычислить длину
        /// </summary>
        public void calk_length()
        {
            double x = Convert.ToDouble(P2.X - P1.X);
            double y = Convert.ToDouble(P2.Y - P1.Y);
            length = Math.Sqrt(x*x+y*y);
        }
    }
}
