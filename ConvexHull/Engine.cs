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
            this.points = new List<Point>();
            this.initGraph(pb);
            this.initMethods();
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
            this.display = T;
            this.width = this.display.Width;
            this.height = this.display.Height;
            this.bitMap = new Bitmap(this.width, this.height);
            this.graphics = Graphics.FromImage(this.bitMap);
            this.refreshGraph();
        }

        public void runMethod(HullMethod method)
        {
            method.initialize(this.points);
            method.execute();
            List<Point> hull = method.getResult();
            //GeometryUtils.sortVerticies(hull);
            if (hull.Count == 0)
                return;
            Point p = hull[hull.Count - 1];
            for (int i = 0; i < hull.Count; i++)
            {
                this.graphics.DrawLine(this.linePen, p.x, p.y, hull[i].x, hull[i].y);
                p = hull[i];
            }
            refreshGraph();
        }

        public void addPoint(Point point)
        {
            this.points.Add(point);
        }

        public void drawPoint(Point point)
        {
            this.graphics.FillEllipse(this.brush, point.x, point.y, 8, 8);
            this.refreshGraph();
        }
        public void refreshGraph()
        {
            this.display.Image = this.bitMap;
        }

        public void clearGraph()
        {
            this.graphics.Clear(Color.White);
            this.refreshGraph();
        }
    }
}
