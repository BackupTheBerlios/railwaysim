using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }       

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void MainForm_Load_1(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity == 1.0)
            {
                timer1.Stop();
                return;
            }
            this.Opacity += 0.05;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AutomatsDesign automats = new AutomatsDesign(this);
            automats.Show();
            this.Hide();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ImageIndex = 1;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ImageIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GrammarForm grammar = new GrammarForm();
            grammar.Show();
            this.Hide();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ImageIndex = 3;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ImageIndex = 2;
        }

       
    }
}