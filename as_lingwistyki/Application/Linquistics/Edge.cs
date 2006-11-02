using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Linquistics
{
    class Edge : IComparable
    {
        private Node beginNode;
        private Node endNode;
        private List<Char> Chars;
        static private int ARROW_RADIUS = 10;
        static private Font drawingFont;
        static private int FONT_HEIGHT = 12;
        private int beginx, beginy, a1x, a1y, a2x, a2y;
        private PointF textPosition;
        static private Pen arrowPen = new Pen(Brushes.DarkBlue, 2);
        private string toDraw = "";
        private SizeF textSize;

        public Edge(Node bNode, Node eNode,Char c)
        {
            beginNode = bNode;
            endNode = eNode;
            Chars = new List<Char>();

            Chars.Add(c);
            calculateArrow();
            FontFamily fontFamily = new FontFamily("Arial");
            drawingFont  = new Font(
               fontFamily,
               FONT_HEIGHT,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
            makeString();
          
        }
        public Node BeginNode
        {
            get { return beginNode; }
        }
        public Node EndNode
        {
            get { return endNode; }
        }
        public String InnerChars
        {
            set
            {
                string[] tab = value.Split(',');
                Chars.Clear();
                for (int i = 0; i < tab.Length; i++)
                {
                    tab[i]=tab[i].Trim();
                    if(tab[i].Length==1) Chars.Add(tab[i][0]);
                }
                this.makeString();
            }
            
        }
        public List<char> EdgeChars
        {
            get { return this.Chars; }
        }
        private void calculateArrow()
        {
           float xpos=0;
            float ypos=0;
            if (beginNode != endNode)
            {
                int delx = (endNode.ScreenPosition.X - beginNode.ScreenPosition.X);
                int dely = (endNode.ScreenPosition.Y - beginNode.ScreenPosition.Y);
                if (delx == 0) delx = 1;
                if (dely == 0) dely = 1;
                double tanges = (double)dely / (double)delx;
                double atan = Math.Atan(tanges);
                if (dely > 0) atan += Math.PI;
                int factor = Math.Sign(delx * dely);
                double delta = Math.PI / 6.0;
                double angle1 = atan + delta;
                double angle2 = atan - delta;
                int firstx = beginNode.ScreenPosition.X - factor * (int)(Math.Round(Math.Cos(atan) * beginNode.Radius));
                int firsty = beginNode.ScreenPosition.Y - factor * (int)(Math.Round(Math.Sin(atan) * beginNode.Radius));
                beginx = endNode.ScreenPosition.X + factor * (int)(Math.Round(Math.Cos(atan) * endNode.Radius));
                beginy = endNode.ScreenPosition.Y + factor * (int)(Math.Round(Math.Sin(atan) * endNode.Radius));
                a1x = beginx + factor * (int)(Math.Round(Math.Cos(angle1) * ARROW_RADIUS));
                a1y = beginy + factor * (int)(Math.Round(Math.Sin(angle1) * ARROW_RADIUS));
                a2x = beginx + factor * (int)(Math.Round(Math.Cos(angle2) * ARROW_RADIUS));
                a2y = beginy + factor * (int)(Math.Round(Math.Sin(angle2) * ARROW_RADIUS));
                if(Math.Abs(tanges)>1)
                {
                    xpos=(float)(0.5*firstx+0.5*beginx+Math.Sign(dely)*10);
                    ypos=(float)(0.5*firsty+0.5*beginy);
                }
                else
                {
                    xpos = (float)(0.5 * beginNode.ScreenPosition.X + 0.5 * beginx);
                    ypos=(float)(0.5*beginNode.ScreenPosition.Y+0.5*beginy+Math.Sign(delx)*10);
                }
            }
            else
            {
                beginx = endNode.ScreenPosition.X;
                beginy = endNode.ScreenPosition.Y-beginNode.Radius;
                a1x = beginx - (int)((double)ARROW_RADIUS / 2);
                a1y = beginy - (int)((double)ARROW_RADIUS / Math.Sqrt(2));
                a2x = beginx + (int)((double)ARROW_RADIUS / 2);
                a2y = a1y;
                xpos = beginx + beginNode.Radius + FONT_HEIGHT / 2;
                ypos = beginy - beginNode.Radius;
            }
            ypos -= FONT_HEIGHT / 2;
            xpos -= FONT_HEIGHT / 2;

           textPosition = new PointF(xpos, ypos);

        }
        public void DrawEdge(PaintEventArgs e)
        {
            if(beginNode!=endNode)
                e.Graphics.DrawLine(arrowPen, beginNode.ScreenPosition, endNode.ScreenPosition);
            else
            {
                
                Point control1 = new Point(beginNode.ScreenPosition.X+2*beginNode.Radius,beginNode.ScreenPosition.Y-3*beginNode.Radius);
                Point control2 = new Point(beginNode.ScreenPosition.X, beginNode.ScreenPosition.Y - 3 * beginNode.Radius);
                Point end=new Point(beginx,beginy);

                e.Graphics.DrawBezier(arrowPen,beginNode.ScreenPosition, control1, control2, end);
            }


            e.Graphics.DrawLine(arrowPen, beginx, beginy, a1x, a1y);
            e.Graphics.DrawLine(arrowPen, beginx, beginy, a2x, a2y);
            this.drawChars(e);
        }
        private void drawChars(PaintEventArgs e)
        {
            textSize = e.Graphics.MeasureString(toDraw, drawingFont);
            e.Graphics.DrawString(toDraw, drawingFont, Brushes.DarkBlue, textPosition);
            
           // e.Graphics.DrawRectangle(Pens.Red,(int)textPosition.X,(int)textPosition.Y,(int)textSize.Width,FONT_HEIGHT);
        }
        private void makeString()
        {
            StringBuilder sBuild = new StringBuilder();
            for(int i=0;i<Chars.Count;i++)
            {
                sBuild.Append(Chars[i]);
                if(i!=Chars.Count-1) sBuild.Append(", "); 
            }
            toDraw=sBuild.ToString();
            
            
        }
        public int CompareTo(object obj)
        {
            if (obj is Edge)
            {
                Edge temp = (Edge)obj;

                if (temp.beginNode == beginNode && temp.endNode == endNode) return 0;
                else return -1;
            }

            throw new ArgumentException("object is not a State");
        }
        public TextEditResult TextClicked(int x, int y)
        {
            Point textCenter = new Point((int)this.textPosition.X + (int)(textSize.Width/2), (int)this.textPosition.Y + (int)(textSize.Height/2));
            if(Math.Abs(x-textCenter.X)<Math.Max(toDraw.Length*FONT_HEIGHT/4,30) && Math.Abs(y-textCenter.Y)<FONT_HEIGHT /2)
            {
                TextEditResult tRes= new TextEditResult();

                tRes.corner.X=(int)this.textPosition.X;
                tRes.corner.Y=(int)this.textPosition.Y;
                tRes.size.Width = (int)(textSize.Width);
                tRes.content=toDraw;
                tRes.CurrentEdge = this;
                return tRes;
            }
            return null;

        }


    }

    class TextEditResult
    {
       public Point  corner = new Point(); 
       public Size size = new Size();
       public String content = "";
        private Edge currentEdge = null;
       public Edge CurrentEdge
       {
           get
           {
               return currentEdge;
           }
           set
           {
               currentEdge = value;
           }
       }
        
    }
}
