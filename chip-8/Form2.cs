using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chip_8
{
    public partial class Form2 : Form
    {
        private Fereastra mainWindow;
        public Form2(Fereastra mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ButtonStep_Click(object sender, EventArgs e)
        {
            mainWindow.DebuggerStep();
        }

        public void Step()
        {
            mainWindow.DebuggerStep();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainWindow.stop_emulation = true;
        }
    }
}
