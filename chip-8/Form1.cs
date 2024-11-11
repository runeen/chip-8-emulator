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
    public partial class Fereastra : Form
    {

        Graphics g;
        private int x, y;

        Keys key_pressed;
        bool is_down;

        private bool[,] matrix;
        Program p;



        public Fereastra()
        {
            this.Width = 1300;
            this.Height = 720;
            matrix = new bool[64, 32];
            p = new Program();
            p.Emulator(this);

            this.Paint += new PaintEventHandler(Paint_Event);
            this.KeyDown += new KeyEventHandler(Key_Down_Event);
            this.KeyUp += new KeyEventHandler(Key_Up_Event);
            this.StartPosition = FormStartPosition.CenterScreen;


            ELoop();
        }


        override
        protected void OnActivated(EventArgs e)
        {
            //Thread EmulatorLoop = new Thread(new ThreadStart(ELoop));
            //EmulatorLoop.Start();
        }

        protected async void ELoop()
        {
            while (true)
            {
                Refresh();
                await Task.Run(() =>
                {

                    p.step(is_down, key_pressed);
                    Thread.Sleep(15);
                });
            }
        }

        private  void EventWaitHandle()
        {
        }

        private void Key_Down_Event(object sender, KeyEventArgs e)
        {
            key_pressed = e.KeyCode;
            Console.WriteLine("ai apasat {0}", key_pressed);
            is_down = true;
        }

        private void Key_Up_Event(object sender, KeyEventArgs e)
        {
            is_down = false;
        }


        public void drawPixel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void clearScreen()
        {
            matrix = new bool[64, 32];
        }


        public void setMatrix(bool[,] matrix)
        {
            this.matrix = matrix;
        }

        public bool[,] getMatrix()
        {
            return this.matrix;
        }


        private void Paint_Event(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1300, 720));

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    if (matrix[j, i])
                    {
                        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(j * 20, i * 20, 20, 18));
                    }
                }
            }

        }

        


    }
}
