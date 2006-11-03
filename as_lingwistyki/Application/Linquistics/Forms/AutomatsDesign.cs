using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public partial class AutomatsDesign : Form
    {

        private ToolboxAutomats tbox;
      
        private GraphAutomats graph;
        
        //static variables for diemensionig, scaling and proper displaying
        static public int PICTURE_HEIGHT;
        static public int PICTURE_WIDTH;
        static public float DEFAULT_WIDTH = 1.0f; //for bitmaps 40x40 
        static public float DEFAULT_HEIGHT = 1.0f;
        static public PointF LOGICAL_SIZE = new PointF(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        static public int IMAGE_DIMENSION;
        static public Char DEFAULT_CHAR = 'a';
        


        private TextBox temporaryTBox = null;
        private TextEditResult temporaryTextEditResult = null;
        
        //for selecting states
        private int selectState = 0; //0 - for none selected; 1 - for state selected; 4 - for textbox editting
        private Point lastMousePosiotion = new Point();

        private int currentStateNumber = 0;
        private string default_state_name = "q";
        private FinitestateAutomation mainAutomate;
        private char currentChar='a';
        private StringBuilder currentWord = new StringBuilder();
        private bool isAccepted = false;

        private bool nextStep = false;
        public AutomatsDesign()
        {
            InitializeComponent();
            tbox = new ToolboxAutomats(this);
            graph = new GraphAutomats();
            PICTURE_HEIGHT = this.pictureBox1.Height;
            PICTURE_WIDTH = this.pictureBox1.Width;
            IMAGE_DIMENSION = (int)((40.0f / DEFAULT_HEIGHT) * LOGICAL_SIZE.Y);
            mainAutomate = new FinitestateAutomation();
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
            System.Drawing.Rectangle workingRectangle =
        Screen.PrimaryScreen.WorkingArea;
           
            // Set the size of the form slightly less than size of 
            // working rectangle.
            this.Location = new System.Drawing.Point(
                (workingRectangle.Width - this.Width)/2, (workingRectangle.Height -this.Height)/2);

           
            tbox.Show(this);
            timer1.Stop();
            timer2.Stop();
        }

        private void Form1_Validating(object sender, CancelEventArgs e)
        {
           
            
        }

        private void panel1_Validating(object sender, CancelEventArgs e)
        {
            
        }


        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            //Drawer.PaintBlueGradient(e);
            if (this.selectState == 1 && tbox.PressedButton == 4)
            {
                graph.DrawTemporaryLine(lastMousePosiotion,e);
            }
            graph.DrawGraph(e);
            
            //

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

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.panel1.Height = this.ClientRectangle.Height - this.panel2.Height - this.toolStrip1.Height - 30;
            this.panel1.Width = this.Width - 8;
            this.pictureBox1.Refresh();
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

        private void removeTextTempBox()
        {
            if (temporaryTBox != null && temporaryTextEditResult != null)
            {
                Edge cEdge=temporaryTextEditResult.CurrentEdge;
                List<char> chars=cEdge.EdgeChars;
                for (int i = 0; i < chars.Count; i++)
                {
                   mainAutomate.OpFunction.DeleteRow(new StateCharPair(cEdge.BeginNode.NodeState,chars[i]),cEdge.EndNode.NodeState);
                   mainAutomate.InnerAlphabet.DeleteChar(chars[i]); 
                }
                if((temporaryTBox.Text.Trim()).Length>0)
                    temporaryTextEditResult.CurrentEdge.InnerChars = temporaryTBox.Text;
                pictureBox1.Controls.Remove(temporaryTBox);
                temporaryTBox.Dispose();
                temporaryTBox = null;
                temporaryTextEditResult = null;
                chars = cEdge.EdgeChars;
                 for (int i = 0; i < chars.Count; i++)
                {
                   mainAutomate.OpFunction.SetDirectState(new StateCharPair(cEdge.BeginNode.NodeState,chars[i]),cEdge.EndNode.NodeState);
                   if(!mainAutomate.InnerAlphabet.IsInAlphabet(chars[i])) mainAutomate.InnerAlphabet.AddToAlphabet(chars[i]); 
                }
                //here add or modify operation function
                
                
                this.pictureBox1.Refresh();
                listView3.Items.Clear();
                listView1.Items.Clear();
                this.mainAutomate.OpFunction.WriteDataToListView(listView3);
                this.mainAutomate.InnerAlphabet.WriteAlphabetToListView(listView1);
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int createOption = tbox.PressedButton;
            if (selectState == 4)
            {
                removeTextTempBox();
                selectState = 0;
            }
            switch (createOption)
            {
                case 0:   break; //here edditing current item
                case 1:
                    {
                        if (graph.HasStartNode == false)
                        {
                            string newName = default_state_name + currentStateNumber;
                            currentStateNumber++;
                            Node tempNode=graph.AddStartNode(e.X, e.Y,newName);
                            StateFiniteAutomata state1 = new StateFiniteAutomata(tempNode);
                            tempNode.NodeState = state1;
                            mainAutomate.AddStartState(state1);
                            listView2.Items.Insert(0, "->" + state1.Name);
                            tbox.FreezeStartNode();
                            graph.HasStartNode = true;
                        }
                    }
                    break;
                case 2:
                    {
                        string newName = default_state_name + currentStateNumber;
                        currentStateNumber++;
                        Node tempNode = graph.AddCommonNode(e.X, e.Y, newName);
                        StateFiniteAutomata state1 = new StateFiniteAutomata(tempNode);
                        tempNode.NodeState = state1;
                        mainAutomate.AddState(state1);
                        listView2.Items.Add(state1.Name);
                                               
                    }
                    break;
                case 3:
                    {
                        string newName = default_state_name + currentStateNumber;
                        currentStateNumber++;
                        Node tempNode = graph.AddAcceptNode(e.X, e.Y, newName);
                        StateFiniteAutomata state1 = new StateFiniteAutomata(tempNode);
                        tempNode.NodeState=state1;
                        mainAutomate.AddAcceptState(state1);
                        listView2.Items.Add("("+state1.Name+")");
                    }
                    break;
                case 4:
                    {
                        if (selectState == 0)
                        {
                            if(graph.SelectBeginNode(e.X, e.Y))
                                selectState = 1;
                        }
                        else if(selectState==1)
                        {
                            Node [] twoNodes=graph.AddEdgeToPosition(e.X, e.Y,DEFAULT_CHAR);
                            if (twoNodes!=null)
                            {
                                if (mainAutomate.InnerAlphabet.AddToAlphabet(DEFAULT_CHAR))                             
                                    listView1.Items.Add(DEFAULT_CHAR.ToString());                               
                                StateCharPair spair = new StateCharPair(twoNodes[0].NodeState, DEFAULT_CHAR);
                                mainAutomate.OpFunction.AddElement(spair, twoNodes[1].NodeState);
                                addToFunctionView(twoNodes[0].Name, DEFAULT_CHAR, twoNodes[1].Name);
                                graph.ClearSelection();
                                selectState = 0;
                            }
                        }

                    }
                    break;

            }
            this.listView2.Refresh();
            this.pictureBox1.Refresh();
        }
        private void addToFunctionView(string name1, char c,string name2)
        {
            string[] tab = new string[3];
            tab[0] = name1;
            tab[1] = c.ToString();
            tab[2] = name2;
            ListViewItem lV1 = new ListViewItem(tab);
            listView3.Items.Add(lV1);
        }
        public void ClearNodesSelection()
        {
            this.graph.ClearSelection();
            selectState = 0;
        }

        private void editMouseAction(MouseEventArgs e)
        {
            TextEditResult tRes = graph.CheckTextEdition(e.X, e.Y); //check if this a modification of text for edge 
            if (tRes != null)
            {
                temporaryTextEditResult = tRes;
                TextBox textBox = new TextBox();
                textBox.Width = Math.Max(30,tRes.size.Width);
                textBox.Height = tRes.size.Height;
                textBox.Location = new Point(tRes.corner.X, tRes.corner.Y);
                textBox.Text = tRes.content;
                temporaryTBox = textBox;
                temporaryTBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
                selectState = 4;
                this.pictureBox1.Controls.Add(textBox);
            }
            else //check if this is a modification of node name
            {

            }
        }
        

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.selectState == 1 && tbox.PressedButton == 4)
            {
                lastMousePosiotion.X = e.X;
                lastMousePosiotion.Y = e.Y;
                pictureBox1.Refresh();
            }
        }

        private void AutomatsDesign_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.selectState == 4 && e.KeyCode == Keys.Enter)
                this.removeTextTempBox();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.selectState == 4 && e.KeyCode == Keys.Enter)
                this.removeTextTempBox();
        }
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editMouseAction(e);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mainAutomate != null)
            {
                currentWord = new StringBuilder(textBox2.Text.Trim());
                if (currentWord.Length>0&&radioButton1.Checked)
                    this.stepSimulation();
                else if (currentWord.Length > 0 && radioButton2.Checked)
                    this.suddenSimulation();
            }
        }
        private void stepSimulation()
        {
            mainAutomate.ActivateAutomate();
            timer1.Start();
        }
        private void suddenSimulation()
        {
            if (mainAutomate.InnerSimulation(currentWord.ToString()))
                textBox2.Text = "nalezy!";
            else
                textBox2.Text = "nie nalezy!";
            textBox2.Refresh();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(currentWord.Length==0&&mainAutomate.IsInAcceptedState())
            {
                timer1.Stop();
                timer2.Stop();
                this.textBox2.Text = "nalezy!!!";
                return;
            }  
            else if (currentWord.Length == 0)
            {
                //the result is: "no!"
                timer2.Stop();
                timer1.Stop();
                this.textBox2.Text = "nie nalezy!!!";
                return;
            }  
           
            currentChar = currentWord[0];
            currentWord.Remove(0, 1);
            nextStep = true;
            
            StateFiniteAutomata s1 = mainAutomate.CurrentState;
            StateFiniteAutomata s2= mainAutomate.NextOperation(currentChar);
                      
            if (s2 == null)
            {
                //the result is: "no!"
                timer2.Stop();
                timer1.Stop();
                this.textBox2.Text = "nie nalezy!!!";
                textBox2.Refresh();
                return;
            }            
            else
            {
                graph.SelectEdgeOperation(s1.AssociatedNode, s2.AssociatedNode);
                timer2.Start();
            }
            textBox2.Text = currentWord.ToString();
            textBox2.Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            graph.SelectedEdgeNextFrame();
            pictureBox1.Refresh();
        }
        
    }
}