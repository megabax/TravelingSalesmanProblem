using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Точки маршрута
    /// </summary>
    public class Points
    {
        /// <summary>
        /// Сами точки
        /// </summary>
        public List<Point> items;

        /// <summary>
        /// Рабочие точки
        /// </summary>
        public List<Point> WorkItems;

        /// <summary>
        /// Ребра
        /// </summary>
        public List<Edge> edges;

        /// <summary>
        /// Результаты
        /// </summary>
        public List<List<Point>> Results;

        /// <summary>
        /// Длина пути
        /// </summary>
        public double length;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Points()
        {
            items = new List<Point>();
            WorkItems = new List<Point>();
            edges = new List<Edge>();
            Results = new List<List<Point>>();
        }

        /// <summary>
        /// Алгоритм полного перебора
        /// </summary>
        /// <param name="t">Номер элемента в массиве</param>
        /// <param name="n">Кол-во элементов</param>
        public void BrutForce(int t, int n)
        {
            if (t == n - 1)
            { 
                //Вывод очередной перестановки
                for (int i = 0; i < n; ++i) Cout(items[i]);
                CoutEndl();
            }
            else
            {
                for (int j = t; j < n; ++j)
                { 
                    //Запускаем процесс обмена
                    Swap(t, j); //a[t] со всеми последующими
                    t++;
                    BrutForce(t, n); //Рекурсивный вызов
                    t--;
                    Swap(t, j);
                }
            }
        }

        public static List<Edge> GetEdges(List<Point> list)
        {
            List<Edge> res = new List<Edge>();
            for (int i = 1; i < list.Count; i++)
            {
                res.Add(new Edge(list[i - 1], list[i]));
            }
            return res;
        }

        public void SearchMin()
        {
            double min = double.MaxValue;
            foreach (List<Point> item in Results)
            {
                List<Edge> ledges = GetEdges(item);
                double summ = 0;
                foreach (Edge edge in ledges)
                {
                    edge.calk_length();
                    summ += edge.length;
                }
                if (summ < min)
                {
                    min = summ;
                    edges = ledges;
                }
            }
            length = min;
        }

        /// <summary>
        /// Поменять элеменыт массива местами
        /// </summary>
        /// <param name="t">Первы элемент</param>
        /// <param name="j">Второй элемент</param>
        private void Swap(int t, int j)
        {
            Point tmp = items[t];
            items[t] = items[j];
            items[j] = tmp;
        }

        /// <summary>
        /// Закончили вывод последовательности
        /// </summary>
        private void CoutEndl()
        {
            Results.Add(WorkItems);
            WorkItems = new List<Point>();
        }

        /// <summary>
        /// Вывести перестановку
        /// </summary>
        /// <param name="point">Точка</param>
        private void Cout(Point point)
        {
            WorkItems.Add(point);
        }

        /// <summary>
        /// Жадный алгоритм
        /// </summary>
        internal void GreedyAlgorithm()
        {           
            edges.Clear();
            List<Point> lpoints = new List<Point>(items);
            List<Point> path = new List<Point>();
            Point point = lpoints[Constants.rnd.Next(lpoints.Count - 1)];
            while (lpoints.Count>1) 
            {                                
                path.Add(point);
                lpoints.Remove(point);
                double min = double.MaxValue;
                Point point2 = Point.Empty;
                for (int i=0; i<lpoints.Count; i++)
                {
                    Edge edge = new Edge(point, lpoints[i]);
                    edge.calk_length();
                    if (edge.length < min)
                    {
                        min = edge.length;
                        point2 = lpoints[i];
                    }
                }
                lpoints.Remove(point2);
                point = point2;
            }
            path.Add(point);
            path.Add(lpoints[0]);
            edges = GetEdges(path);
        }

        internal void CalkLength()
        {
            length = 0;
            foreach (Edge edge in edges)
            {
                edge.calk_length();
                length += edge.length;
            }
        }

        /*      /// <summary>
              /// Рекурсивный полный перебор
              /// </summary>
              /// <param name="edgesList">Уже готовый список</param>
              /// <returns>Результат</returns>
              private List<Edge> BruteForce(List<Edge> edgesList, List<Point> points)
              {
                  edgesList.Add(new Edge(items[0], items[1]));
                  int index = 0;
                  for (int i = 0; i < items.Count; i++)
                  {
                      if (i != index) points.Add(items[i]);
                  }
                  //for (int i = 1; i < points.Count; i++) edgesList.Add(new Edge(points[i - 1], points[i]));
                  return edgesList;
              }
              */

        /*     /// <summary>
             /// Решение тупым перебором
             /// </summary>
             public void stupid_search()
             {
                 edges.Clear();
                 List<SearchResult> results = new List<SearchResult>();
                 SearchResult res = new SearchResult();
                 stupid_search(res, results, Point.Empty, items);
                 /*edges = results[1].edges;
                 length = results[1].lenght;
                 return; тут отглчить надо*/
        /*  double min = double.MaxValue;
          foreach(SearchResult item in results)
          {            
              foreach(Edge edge in item.edges)
              {
                  if(edge.p1==Point.Empty || edge.p2 == Point.Empty)
                  {
                      edges = item.edges;
                      length = item.lenght; //индекс 24 - глюк
                      return;
                  }
              }  
              if (item.lenght < min)
              {
                  min = item.lenght;
                  edges = item.edges;
              }
          }
          length = min;
      }

      /// <summary>
      /// Решение тупым перебором
      /// </summary>
      /// <param name="res">Текущий результат</param>
      /// <param name="results">Список результатов</param>
      /// <param name="point">Точка</param>
      /// <param name="a_items">Точки</param>
      private void stupid_search(SearchResult res, List<SearchResult> results, Point point, List<Point> a_items)
      {
          if (point != Point.Empty)
          {
              if (a_items.Count == 0)
              {
                  res.calk_length();
                  results.Add(res);
                  return;
              }
              Edge edge = new Edge();
              edge.p1 = point;
              res.edges.Add(edge);
          }
          for (int i = 0; i < a_items.Count; i++)
          {
              if (point != Point.Empty)
              {
                  SearchResult lres = copy_res(res, a_items[i]);
                  stupid_search(lres, results, a_items[i], get_points(i, a_items));
              }
              else
              {
                  stupid_search(res, results, a_items[i], get_points(i, a_items));
              }
          }
      }

      /// <summary>
      /// Получить копию результата
      /// </summary>
      /// <param name="res">Результат</param>
      /// <param name="point">Новая тчока</param>
      /// <returns>Копия</returns>
      private SearchResult copy_res(SearchResult res, Point point)
      {
          SearchResult res_new = new SearchResult();
          foreach (Edge edge in res.edges)
          {
              res_new.edges.Add(edge.clone());
          }
          Edge edge_res=res_new.edges[res_new.edges.Count - 1];
          edge_res.p2 = point;
          edge_res.calk_length();
          return res_new;
      }

      /*    /// <summary>
          /// Выполнить поиск
          /// </summary>
          /// <param name="results">Список результатов</param>
          /// <param name="p1">Точка</param>
          /// <param name="a_items">Список возможных точек</param>
          /// <returns>Результаты</returns>
          private List<SearchResult> search(List<SearchResult> results, Point p1, List<Point> a_items)
          {
              for(int i=0; i<a_items.Count; i++)
              {
                  List<SearchResult> res_bottom = search(results, a_items[i], get_points(i, a_items));
                  List<SearchResult> tmp_res = new List<SearchResult>();
                  foreach (SearchResult res1 in results)
                  {
                      foreach (SearchResult res2 in res_bottom)
                      {
                          tmp_res.Add(join(res1, res2));
                      }
                  }
                  return results;
              }
          }*/

        /*     /// <summary>
             /// Получить все точки, кроме указаной
             /// </summary>
             /// <param name="n">Номер указанной точки</param>
             /// <param name="a_items">Точки</param>
             /// <returns>Список точек</returns>
             private List<Point> get_points(int n, List<Point> a_items)
             {
                 List<Point> res = new List<Point>();
                 for(int i=0; i<a_items.Count; i++)
                 {
                     if (i != n) res.Add(a_items[i]);
                 }
                 return res;
             }*/

        /// <summary>
        /// Нарисовать
        /// </summary>
        /// <param name="gr">Графическое поле</param>
        public void draw(Graphics gr)
        {
            Brush brush = new SolidBrush(Color.Yellow);
            foreach(Point item in items)
            {
                gr.FillEllipse(brush, item.X - 3, item.Y - 3, 6, 6);
            }
            double count = edges.Count;
            int k = 1;
            foreach(Edge edge in edges)
            {
                int color = Convert.ToInt32(Convert.ToDouble(k) / count * 255.0);
                if (color > 255) color = 255;
                GraphUtil.draw_arrow(edge.P1.X, edge.P1.Y, edge.P2.X, edge.P2.Y, 
                    Color.FromArgb(color,0,255-color),gr);
                k++;
            }
        }
    }
}
