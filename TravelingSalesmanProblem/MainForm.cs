using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelingSalesmanProblem
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Точки
        /// </summary>
        public Points points;

        private string line;

        private int[] a;

        /// <summary>
        /// Индекс
        /// </summary>
        private int index;

        public MainForm()
        {
            InitializeComponent();
            points = new Points();
        }

        private void pbImage_Paint(object sender, PaintEventArgs e)
        {
            //GraphUtil.draw_arrow(100, 100, 200, 200, Color.Red, e.Graphics);
            e.Graphics.Clear(Color.Black);
            points.draw(e.Graphics);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            points.items.Clear();
            for(int i=1; i<=11; i++)
            {
                points.items.Add(new Point(Constants.rnd.Next(1,300), Constants.rnd.Next(1,300)));
            }
            /*Edge edge = new Edge();
            edge.p1 = points.items[0];
            edge.p2 = points.items[1];
            points.edges.Add(edge);*/
            pbImage.Refresh();            
        }

        private void btnStupidSearch_Click(object sender, EventArgs e)
        {
            points.BrutForce(0,points.items.Count);
            points.SearchMin();     
            pbImage.Refresh();
            MessageBox.Show("Длина пути "+Convert.ToString(points.length));
        }

        void Generate(int t, int n)
        {
            if (t == n - 1)
            { //Вывод очередной перестановки
                for (int i = 0; i < n; ++i) Cout(a[i]);
                CoutEndl();
            }
            else
            {
                for (int j = t; j < n; ++j)
                { //Запускаем процесс обмена
                    Swap(t, j); //a[t] со всеми последующими
                    t++;
                    Generate(t, n); //Рекурсивный вызов
                    t--;
                    Swap(t, j);
                }
            }
        }

        private void Swap(int t, int j)
        {
            int tmp = a[j];
            a[j] = a[t];
            a[t] = tmp;
        }

        private void CoutEndl()
        {
            lbOut.Items.Add(line);
            line = "";
        }

        private void Cout(int v)
        {
            line = line + v + ";";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Бинарный файл точек (*.bft)|*.bft";
            if(saveFileDialog.ShowDialog()== DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, points.items);
                fs.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter= "Бинарный файл точек (*.bft)|*.bft"; 
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                points.items = (List<Point>)formatter.Deserialize(fs);
                fs.Close();
                pbImage.Refresh();
            }
        }


        private void btnGreedyAlgorithm(object sender, EventArgs e)
        {
            points.GreedyAlgorithm();
            points.CalkLength();
            pbImage.Refresh();
            MessageBox.Show("Длина пути " + Convert.ToString(points.length));            
        }
    }
}
