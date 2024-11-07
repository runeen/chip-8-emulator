using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace chip_8
{
    public partial class Form1 : Form
    {

        Graphics g;
        private int x, y;

        private bool[,] matrix;



        public Form1()
        {
            this.Width = 640;
            this.Height = 320;
            matrix = new bool[64, 32];
        }
        

        override
        protected void OnActivated(EventArgs e)
        {
            Program p = new Program();
            p.Emulator(this);
            this.Paint += new PaintEventHandler(Paint_Event);
            while(true)
            {
                Thread.Sleep(100);
                p.step();
            }
        }

        public void drawPixel(int x, int y)
        {
            this.x = x;
            this.y = y;
            //this.Paint += new PaintEventHandler(Draw_Pixel_Event);
        }
        public void clearScreen()
        {
            matrix = new bool[64, 32];
            Refresh();
        }


        public void setMatrix(bool[,] matrix)
        {
            this.matrix = matrix;
            Refresh();
        }

        public bool[,] getMatrix()
        {
            return this.matrix;
        }


        private void Paint_Event(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 640, 320));

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    if (matrix[j, i])
                    {
                        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(j * 10, i * 10, 10, 10));
                    }
                }
            }

        }

        private void Clear_Screen_Event(object sender, PaintEventArgs e)
        {
            Console.Write("ClearScreen in event");
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 640, 320));
        }

        

        private void Draw_Pixel_Event(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(x * 10, y * 10, 10, 10));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            g.DrawEllipse(myPen, new Rectangle(0, 0, 200, 300));
        }
    }
}
