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
    public partial class Form1 : Form
    {
        private Engine engine;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.engine = new Engine(this.pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e) //Jarvis
        {
            this.engine.runMethod(this.engine.jarvis);
        }

        private void button2_Click(object sender, EventArgs e) //Graham
        {
            this.engine.runMethod(this.engine.graham);
        }

        private void button3_Click(object sender, EventArgs e) //Quick
        {
            this.engine.runMethod(this.engine.quickHull);
        }

        private void button4_Click(object sender, EventArgs e) //Divide
        {
            this.engine.runMethod(this.engine.divideHull);
        }

        private void button5_Click(object sender, EventArgs e) //Clear All
        {
            this.engine.points.Clear();
            this.engine.clearGraph();
            this.engine.refreshGraph();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point point = Point.FromDrawPoint(this.PointToClient(new System.Drawing.Point(Form1.MousePosition.X, Form1.MousePosition.Y)));
            if (point.x > 10 && point.x < (this.engine.width - 10) && point.y > 10 && point.y < (this.engine.height - 10))
            {
                this.engine.addPoint(point);
                this.engine.drawPoint(point);
            }
        }

        private void button6_Click(object sender, EventArgs e) //Clear Lines
        {
            List<Point> points = new List<Point>(this.engine.points);
            this.engine.clearGraph();
            foreach (Point p in points)
                this.engine.drawPoint(p);
            this.engine.refreshGraph();
        }
    }
}
