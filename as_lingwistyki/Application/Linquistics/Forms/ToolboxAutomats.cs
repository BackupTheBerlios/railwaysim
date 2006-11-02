using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public partial class ToolboxAutomats : Form
    {
       
           
            
       
        private AutomatsDesign mainWindow;
        private int pressedButton = 0;
        private Button[] buttonTab = new Button[4];
       
        public int PressedButton
        {
            get
            {
                return pressedButton;
            }

        }
        public ToolboxAutomats(AutomatsDesign mainW)
        {
            InitializeComponent();
            buttonTab[0] = this.button1;
            buttonTab[1] = this.button2;
            buttonTab[2] = this.button3;
            buttonTab[3] = this.button4;
            mainWindow = mainW;
        }

        private void ToolboxAutomats_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Owner.Location.X+550, this.Owner.Location.Y+20);
        }

        private void ToolboxAutomats_Move(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {          
                this.Owner.Refresh();
            }
        }
        private void upButtons(int except)
        {
            for (int i = 0; i < buttonTab.Length; i++)
            {
                if (i != except) buttonTab[i].FlatStyle = FlatStyle.Standard;
                else buttonTab[i].FlatStyle = FlatStyle.Flat;
            }
            pressedButton = except + 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
             setButtonAction(0);
            
        }
        private void setButtonAction(int index)
        {
            this.mainWindow.ClearNodesSelection();
            if (buttonTab[index].FlatStyle == FlatStyle.Standard)
            {
                buttonTab[index].FlatStyle = FlatStyle.Flat;
                upButtons(index);
                this.mainWindow.DrawingPanel.Cursor = Cursors.Cross;
            }
            else
            {
                pressedButton = 0;
                buttonTab[index].FlatStyle = FlatStyle.Standard;
                this.mainWindow.DrawingPanel.Cursor = Cursors.Arrow;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            setButtonAction(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setButtonAction(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setButtonAction(3);
        }

        private void ToolboxAutomats_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Refresh();
            }
        }

        private void ToolboxAutomats_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Refresh();
            }
        }

        private void ToolboxAutomats_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Refresh();
            }
        }
        public void FreezeStartNode()
        {
            button1.Enabled = false;
            
        }
        public void UnfreezeStartNode()
        {
            button1.Enabled = true;
        }
    }
}