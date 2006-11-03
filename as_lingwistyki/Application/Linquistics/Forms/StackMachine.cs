using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public partial class StackMachine: Form
    {

        //static variables for diemensionig, scaling and proper displaying
        static public int PICTURE_HEIGHT;
        static public int PICTURE_WIDTH;
        static public float DEFAULT_WIDTH = 1.0f; //for bitmaps 40x40 
        static public float DEFAULT_HEIGHT = 1.0f;
        static public PointF LOGICAL_SIZE = new PointF(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        static public int IMAGE_DIMENSION;
        static public Char DEFAULT_CHAR = 'a';

        //for selecting states
        private int selectState = 0;
        private Point lastMousePosiotion = new Point();
        public StackMachine()
        {
            InitializeComponent();
             PICTURE_HEIGHT = this.pictureBox1.Height;
            PICTURE_WIDTH = this.pictureBox1.Width;
            IMAGE_DIMENSION = (int)((40.0f / DEFAULT_HEIGHT) * LOGICAL_SIZE.Y);
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        public Panel DrawingPanel
        {
            get
            {
                return this.panel1;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
      
           
        }

        private void Form1_Validating(object sender, CancelEventArgs e)
        {
           
            
        }

        private void panel1_Validating(object sender, CancelEventArgs e)
        {
            
        }


            

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

     
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void AutomatsDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

     
        
    }
}