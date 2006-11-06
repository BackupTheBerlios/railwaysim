using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Linquistics
{
    partial class GraphAutomats
    {
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        private List<Node> selectedNodes = new List<Node>();
        public bool HasStartNode = false;
        private Edge animatedEdge = null;
        static public int FRAMES_NUMBER = 8;
        private int frameStep = 1;
        public List<Node> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
            }

        }
        public List<Edge> Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = value;
            }

        }
        public void DrawGraph(PaintEventArgs e)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                this.edges[i].DrawEdge(e);

            }
            for (int i = 0; i < nodes.Count; i++)
            {
                this.nodes[i].DrawNode(e);
                if (nodes[i].Selected) nodes[i].DrawSelection(e);
            }
           
            
        }
        public Node AddStartNode(int screenx, int screeny,string name)
        {
            Point pos = new Point(screenx, screeny);
            Node res = FactoryNodes.CreateStartNode(pos, name);
            nodes.Add(res);
            return res;
        }
        public Node AddCommonNode(int screenx, int screeny, string name)
        {
            Point pos = new Point(screenx, screeny);
            Node res = FactoryNodes.CreateCommonNode(pos, name);
            nodes.Add(res);
            return res;
        }
        public Node AddAcceptNode(int screenx, int screeny, string name)
        {
            Point pos = new Point(screenx, screeny);
            Node res = FactoryNodes.CreateAcceptNode(pos, name);
            nodes.Add(res);
            return res;
        }
        public bool SelectBeginNode(int screenx, int screeny)
        {
            Node found = this.findNode(screenx, screeny);
            if (found != null)
            {
                selectedNodes.Add(found);
                found.Selected = true;
                return true;
            }
            return false;
        }
        private Node findNode(int screenx,int screeny)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (Math.Abs(screenx - nodes[i].ScreenPosition.X) < nodes[i].Radius && Math.Abs(screeny - nodes[i].ScreenPosition.Y) < nodes[i].Radius)
                {
                    return nodes[i];
                    
                }
            }
            return null;
        }
        public Node[] AddEdgeToPosition(int screenx, int screeny,char c)
        {
            if(this.selectedNodes.Count==1)
            {
                Node found = this.findNode(screenx, screeny);
                if (found != null)
                {
                    this.addEdge(selectedNodes[0],found,c);
                    Node [] tab=new Node[2] {selectedNodes[0],found};
                    return tab;
                }
                
            }
            return null;
        }
        public void ClearSelection()
        {
            for (int i = 0; i < selectedNodes.Count; i++)
                selectedNodes[i].Selected = false;
            this.selectedNodes.Clear();
        }
        private void addEdge(Node from, Node to,Char c)
        {
            Edge oldEd=findEdge(from, to);
            if(oldEd==null)
                edges.Add(new Edge(from, to,c));
        }
        public void SelectEdgeOperation(Node n1, Node n2)
        {
            if (animatedEdge != null) animatedEdge.Bevel = 0;
            animatedEdge = findEdge(n1, n2);
            if (animatedEdge != null)
            {
                animatedEdge.Bevel = 1;
                frameStep = 1;
            }
        }
        public void SelectedEdgeNextFrame()
        {
            if (animatedEdge!=null && animatedEdge.Bevel != 0)
            {
                if (animatedEdge.Bevel >= FRAMES_NUMBER / 2 || animatedEdge.Bevel < 0)
                    frameStep *= -1;
                animatedEdge.Bevel += frameStep;
            }
        }
        private Edge findEdge(Node from, Node to)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].BeginNode == from && edges[i].EndNode == to)
                {
                    return edges[i];
                }
            }
            return null;
        }
        public void DrawTemporaryLine(Point toXY, PaintEventArgs e)
        {
            if (selectedNodes.Count == 1)
            {
                e.Graphics.DrawLine(Pens.Plum, selectedNodes[0].ScreenPosition, toXY);
            }
        }
        public TextEditResult  CheckTextEdition(int x, int y)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                TextEditResult tRes=edges[i].TextClicked(x,y);
                if (tRes!=null)
                {
                    return tRes;
                }
             
            }
            return null;
        }
    }
}
