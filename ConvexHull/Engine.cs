using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvexHull
{
    public class Engine
    {
        public Bitmap bitMap;
        public Graphics graphics;
        public Color backColor = Color.LightBlue;
        public int width, height;
        public PictureBox display;
        public Pen linePen = new Pen(Color.Black, 2);
        public SolidBrush brush = new SolidBrush(Color.Red);

        public List<Point> points;

        public Jarvis jarvis;
        public Graham graham;
        public QuickHull quickHull;
        public DivideHull divideHull;

        public Engine(PictureBox pb)
        {
            points = new List<Point>();
            this.initGraph(pb);
            initMethods();
        }

        public void initMethods()
        {
            jarvis = new Jarvis();
            graham = new Graham();
            quickHull = new QuickHull();
            divideHull = new DivideHull();
        }

        public void initGraph(PictureBox T)
        {
            display = T;
            width = display.Width;
            height = display.Height;
            bitMap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitMap);
            refreshGraph();
        }

        public void addPoint(Point point)
        {
            this.points.Add(point);
        }

        public void drawPoint(Point point)
        {
            graphics.FillEllipse(brush, point.x, point.y, 8, 8);
            refreshGraph();
        }
        public void refreshGraph()
        {
            display.Image = bitMap;
        }

        public void clearGraph()
        {
            graphics.Clear(Color.White);
            this.refreshGraph();
        }
    }
}
