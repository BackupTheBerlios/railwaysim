using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LingistykaInterfejsy.Wizard
{
    public partial class StateFunction : UserControl
    {
        public StateFunction()
        {
            InitializeComponent();
        }
        public StateFunction(int a, int b)
        {
            InitializeComponent();
            alphabetSigns = a;
            stackSigns = b;
            init();
        }
        private int alphabetSigns;
        private int stackSigns;
        private void init()
        {
            this.tableLayoutPanel1.RowCount = stackSigns + 1;
            this.tableLayoutPanel1.ColumnCount = alphabetSigns + 1;
            for(int i=0 ; i<stackSigns+1; i++)
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70));

            for (int i = 0; i < alphabetSigns + 1; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20));
               
            }
            for (int j = 0; j < ((stackSigns+1) * (alphabetSigns +1))-1; j++)
            {
            TextBox text = new TextBox();
                text.Text = "aaaazxxxxxxxxxxxsfdgshghj";
                text.Size=new Size(60, 18);
                this.tableLayoutPanel1.Controls.Add(text);
            }
          
          
           
        }
    }
}
