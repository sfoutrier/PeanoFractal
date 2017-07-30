using PeanoGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peano
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            const int patternSize = 3;
            int iteration = 4;
            int edgeLength = 5;
            int imgSize = (int)Math.Pow(patternSize,(iteration + 1 )) * edgeLength;
            _fractal = new Bitmap(imgSize, imgSize);
            using (var g = Graphics.FromImage(_fractal))
            {
                var pen = new Pen(Color.Black);
                var points = Stuff.GetPeanoPatten(iteration, edgeLength);
                g.DrawLines(pen, points.ToArray());
            }

            pictureBox1.SizeChanged += PictureBox1_SizeChanged;
            PictureBox1_SizeChanged(null, null);
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(_fractal, pictureBox1.ClientSize);
        }

        private Image _fractal;
    }
}
