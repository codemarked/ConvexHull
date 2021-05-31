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
            engine = new Engine(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e) //Jarvis
        {

        }

        private void button2_Click(object sender, EventArgs e) //Graham
        {

        }

        private void button3_Click(object sender, EventArgs e) //Quick
        {

        }

        private void button4_Click(object sender, EventArgs e) //Divide
        {

        }

        private void button5_Click(object sender, EventArgs e) //CLear
        {

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
    }
}
