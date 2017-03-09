using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Maze
{
    public partial class FormMaze : Form
    {
        public FormMaze()
        {
            InitializeComponent();
        }
fdbdfbfb
        MazeItself m1;

        private void FormMaze_Load(object sender, EventArgs e)
        {
       
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            m1 = new MazeItself(50);
            m1.GenerateMaze();
            Draw();
        }

        void Draw()
        {
            var bmp = new Bitmap(500,500);
            var thickPen = new Pen(Color.Black, 5);
            var thinPen = new Pen(Color.Black, 3);
            var eraser = new Pen(Color.GhostWhite, 5);
            var formGraphics = Graphics.FromImage(bmp);

            int multiplier = bmp.Size.Height / m1.SizeX;
            try
            {
                formGraphics.DrawRectangle(thickPen, 0, 0, 499, 499);

                Cell cellToDraw;
                for (int i = 0; i < m1.SizeX; i++)
                {
                    for (int j = 0; j < m1.SizeY; j++)
                    {
                        cellToDraw = m1[i, j];

                        if (cellToDraw.wS.Up)
                        {
                            formGraphics.DrawLine(thinPen, i * multiplier, j * multiplier, i * multiplier, j * multiplier + multiplier);
                        }
                        if (cellToDraw.wS.Left)
                        {
                            formGraphics.DrawLine(thinPen, i * multiplier, j * multiplier, i * multiplier + multiplier, j * multiplier);
                        }
                    }
                }

                formGraphics.DrawLine(eraser, 0, 0, 0, multiplier);
                formGraphics.DrawLine(eraser, 499, 499, 499, 499 - multiplier);

                pictureBox1.Image = bmp;
            }
            finally
            {
                eraser.Dispose();
                thickPen.Dispose();
                thinPen.Dispose();
                formGraphics.Dispose();
            }

        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            if (bmp == null) return;

            var myBrush = new SolidBrush(Color.Red);
            
            var formGraphics = Graphics.FromImage(bmp);

            int multiplier = bmp.Size.Height / m1.SizeX;
            try
            {
                Cell cellToDraw;

                List<Point> list = m1.Solved;
                int i, j;
                for(int k = 0; k < list.Count; k++)
                {
                    i = list[list.Count-k-1].X;
                    j = list[list.Count-k-1].Y;

                    cellToDraw = m1[i, j];
              
                    if (cellToDraw.Solve)
                    {
                        formGraphics.FillRectangle(
                             myBrush,
                             i * multiplier + 3,
                             j * multiplier + 3,
                             multiplier - 5,
                             multiplier - 5);
                          
                    }
                    Thread.Sleep(10);
                    pictureBox1.Refresh();
                }

                pictureBox1.Image = bmp;
            }
            finally
            {
                myBrush.Dispose();
                formGraphics.Dispose();
            }
        }
    }
}
