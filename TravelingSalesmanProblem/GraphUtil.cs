using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesmanProblem
{
    /// <summary>
    /// Графические утилиты
    /// </summary>
    public static class GraphUtil
    {
        public static void draw_arrow(int x1, int y1, int x2, int y2, Color color, Graphics gr)
        {
            Pen pen = new Pen(new SolidBrush(color));
            gr.DrawLine(pen, x1, y1, x2, y2);
            double a = Math.Atan2(y2 - y1, x2 - x1);
            double a1 = a + Math.PI / 10.0;
            double a2 = a - Math.PI / 10.0;
            double dx = x2 - x1;
            double dy = y2 - y1;
            double k = 0.1;
            double r = Math.Sqrt(dx * dx + dy * dy);
            int x2_1 = Convert.ToInt32(x2 - k * r * Math.Cos(a1));
            int y2_1 = Convert.ToInt32(y2 - k * r * Math.Sin(a1));
            int x2_2 = Convert.ToInt32(x2 - k * r * Math.Cos(a2));
            int y2_2 = Convert.ToInt32(y2 - k * r * Math.Sin(a2));
            gr.DrawLine(pen, x2_1, y2_1, x2, y2);
            gr.DrawLine(pen, x2_2, y2_2, x2, y2);
        }
    }
}
