﻿using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        Form2 emulatorWindow;
        SolidBrush white;
        SolidBrush black;

        public bool stop_emulation;


        public Fereastra()
        {
            this.Width = 1300;
            this.Height = 720;
            matrix = new bool[64, 32];
            p = new Program();
            p.Emulator(this);
            stop_emulation = false;

            emulatorWindow = new Form2(this);

            emulatorWindow.Visible = true;

            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(Paint_Event);
            this.KeyDown += new KeyEventHandler(Key_Down_Event);
            this.KeyUp += new KeyEventHandler(Key_Up_Event);
            this.StartPosition = FormStartPosition.CenterScreen;


            white = new SolidBrush(Color.FromArgb(0xff, 0xd6, 0xff));
            black = new SolidBrush(Color.FromArgb(0xc8, 0xb6, 0xff));

            //DebuggerLoop();

            ELoop();

            RefreshLoop();
        }


        override
        protected void OnActivated(EventArgs e)
        {
            //Thread EmulatorLoop = new Thread(new ThreadStart(ELoop));
            //EmulatorLoop.Start();
        }

        protected async void ELoop()
        {
            while (!stop_emulation)
            {
                await Task.Run(() =>
                {
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    p.step(is_down, key_pressed);
                    Thread.Sleep(1);
                });
            }

        }

        protected async void RefreshLoop()
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(7);
                });
                Refresh();
            }
        }

        public async void DebuggerLoop()
        {
            while (true)
            {
                bool refresh = await Task.Run(() =>
                {
                    return p.step(is_down, key_pressed);
                });

                StringBuilder sb = new StringBuilder();

                sb.Append("Registri: ");

                for (int i = 0; i < 0x10; i++)
                {
                    sb.Append(p.registers[i].ToString("X2"));
                    sb.Append(", ");
                }

                sb.Append("Delay timer: ");
                sb.Append(p.delayTimer.ToString("X2"));
                sb.Append(".");
                emulatorWindow.Registrii.Text = sb.ToString();



                sb = new StringBuilder();

                sb.Append("Instructiune Rulata: ");
                sb.Append(p.PC.ToString("X4"));

                sb.Append(p.lastRun.ToString());
                emulatorWindow.InstrRulata.Text = sb.ToString();


                sb = new StringBuilder();

                sb.Append("Urmatoarea instructiune: ");

                sb.Append(p.Memory[p.PC + 2].ToString("X2"));
                sb.Append(p.Memory[p.PC + 4].ToString("X2"));
                emulatorWindow.InstUrm.Text = sb.ToString();



                sb = new StringBuilder();

                for(int i = 0x200; i < 0x500; i++)
                {
                    if (i % 0x20 == 0) { sb.Append("\n"); }
                    if (i % 2 == 0) { sb.Append(" "); }
                    if (p.PC == i)
                    {
                        sb.Append("PC:");
                    }

                    if(p.I == i)
                    {
                        sb.Append("I:");
                    }

                    sb.Append(p.Memory[i].ToString("X2"));
                }

                emulatorWindow.label7.Text = sb.ToString();


                if (refresh) Refresh();
            }
        }

        public async void DebuggerStep()
        {
            
                bool refresh = await Task.Run(() =>
                {
                    return p.step(is_down, key_pressed);
                });

                StringBuilder sb = new StringBuilder();

                sb.Append("Registri: ");

                for (int i = 0; i < 0x10; i++)
                {
                    sb.Append(p.registers[i].ToString("X2"));
                    sb.Append(", ");
                }

                sb.Append("Delay timer: ");
                sb.Append(p.delayTimer.ToString("X2"));
                sb.Append(".");
                emulatorWindow.Registrii.Text = sb.ToString();



                sb = new StringBuilder();

                sb.Append("Instructiune Rulata: ");
                sb.Append(p.PC.ToString("X4"));

                sb.Append(p.lastRun.ToString());
                emulatorWindow.InstrRulata.Text = sb.ToString();


                sb = new StringBuilder();

                sb.Append("Urmatoarea instructiune: ");

                sb.Append(p.Memory[p.PC + 2].ToString("X2"));
                sb.Append(p.Memory[p.PC + 4].ToString("X2"));
                emulatorWindow.InstUrm.Text = sb.ToString();



                sb = new StringBuilder();

                for (int i = 0x200; i < 0x500; i++)
                {
                    if (i % 0x20 == 0) { sb.Append("\n"); }
                    if (i % 2 == 0) { sb.Append(" "); }
                    if (p.PC == i)
                    {
                        sb.Append("PC:");
                    }

                    if (p.I == i)
                    {
                        sb.Append("I:");
                    }

                    sb.Append(p.Memory[i].ToString("X2"));
                }

                emulatorWindow.label7.Text = sb.ToString();


                if (refresh) Refresh();
        }


        private void EventWaitHandle()
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
            key_pressed = 0;
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

            g.FillRectangle(black, new Rectangle(0, 0, 1300, 720));



            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    if (matrix[j, i])
                    {
                        g.FillRectangle(white, new Rectangle(j * 20 + 1, i * 20 + 1, 18, 17));
                    }
                }
            }

        }
    }
}
