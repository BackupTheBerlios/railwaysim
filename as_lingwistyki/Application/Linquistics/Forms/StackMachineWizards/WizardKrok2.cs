using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LingistykaInterfejsy.Wizard
{
    public partial class WizardKrok2 : Form
    {
        public WizardKrok2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {   Random rand = new Random();
           
            this.taby.TabPages.Add(rand.Next().ToString(), rand.Next().ToString());
            int val=rand.Next();
            StateFunction stf=new StateFunction(5, 7);
            stf.Name = val.ToString();
            stf.Dock = DockStyle.Fill;
            this.taby.TabPages[this.taby.TabCount-1].Controls.Add(stf);
        }
    }
}