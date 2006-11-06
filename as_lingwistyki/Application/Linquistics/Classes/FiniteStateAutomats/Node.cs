using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; 
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Linquistics
{
    public class Node
    {
        private Bitmap statePicture=null;
        private PointF centerPoint=new PointF();
        private Point screenPosition = new Point();
        private int radius;
        private int widthPict;
        private bool selected = false;
        private StateFiniteAutomata nodeState = null;
        private string name = "";
        private static int FONT_HEIGHT = 14;
        private static Font nodeFont;
        
        
        public Node(int type,Point screenPos)
        {
            screenPosition = screenPos;
            float ratioX = (float)AutomatsDesign.PICTURE_WIDTH / (float)screenPosition.X;
            centerPoint.X = AutomatsDesign.LOGICAL_SIZE.X * ratioX;
            float ratioY = (float)AutomatsDesign.PICTURE_HEIGHT / (float)screenPosition.Y;
            centerPoint.Y = AutomatsDesign.LOGICAL_SIZE.Y * ratioY;
            try
            {
                System.Reflection.Assembly thisExe;
                thisExe = System.Reflection.Assembly.GetExecutingAssembly();
                String imagePath="";
                switch (type)
                {
                    case 0: imagePath = "Linquistics.Resources.state_begin.bmp"; break;
                    case 1: imagePath = "Linquistics.Resources.state.bmp"; break;
                    case 2: imagePath = "Linquistics.Resources.state_accepted.bmp"; break;
                }
                System.IO.Stream file =
                     thisExe.GetManifestResourceStream(imagePath);
                  

                statePicture = new Bitmap(file);

                statePicture.MakeTransparent(Color.FromArgb(240, 240, 240));
                widthPict = (int)((float)AutomatsDesign.IMAGE_DIMENSION * ((float)statePicture.Width / (float)statePicture.Height));
                radius = (int)(Math.Round(Math.Sqrt(Math.Pow((statePicture.Height / 2), 2) + Math.Pow((statePicture.Width / 2), 2)))) - 10;
            }
            catch(Exception ex)
            {

            }
            FontFamily fontFamily = new FontFamily("Arial");
            nodeFont = new Font(
               fontFamily,
               FONT_HEIGHT,
               FontStyle.Bold,
               GraphicsUnit.Pixel);            
        }
        
        public Node(int type, Point screenPos, string s) : this(type, screenPos)
        {    
            name = s;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public StateFiniteAutomata NodeState
        {
            get
            {
                return nodeState;
            }
            set
            {
                nodeState = value;
            }

        }
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public Point PositionToDraw
        {
            get
            {
                return new Point(screenPosition.X - statePicture.Height / 2, screenPosition.Y - statePicture.Width / 2);
            }
        }
        public int Radius
        {
            get
            {
                return radius;
            }
        }
        public Point ScreenPosition
        {
            get
            {
                return screenPosition;
            }
        }
        public PointF LogicalPosition
        {
            get { return this.centerPoint; }
            set { centerPoint = value; }
        }
        public Bitmap Picture
        {
            get
            {
                return statePicture;
            }
        }
        public void RandomizeLocation(Rectangle rect)
        {
            Random ran1=new Random((int)DateTime.Now.Ticks);
            float ratiox=(float)ran1.NextDouble();
            float ratioy=(float)ran1.NextDouble();
            this.centerPoint.X = ratiox*AutomatsDesign.LOGICAL_SIZE.X;
            this.centerPoint.Y = ratioy* AutomatsDesign.LOGICAL_SIZE.Y;
            this.screenPosition.X = (int)(ratiox * (float)rect.Width);
            this.screenPosition.Y = (int)(ratioy * (float)rect.Height);

        }
        public void DrawNode(PaintEventArgs e)
        {
            
            e.Graphics.DrawImage(this.statePicture, this.PositionToDraw.X, this.PositionToDraw.Y, widthPict,AutomatsDesign.IMAGE_DIMENSION);
            SizeF fSize= e.Graphics.MeasureString(name,nodeFont);
            e.Graphics.DrawString(name, nodeFont, Brushes.DarkBlue, this.screenPosition.X - fSize.Width / 2, this.screenPosition.Y - fSize.Height / 2);
        }
        public void DrawSelection(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(new Pen(Brushes.Red,3), this.screenPosition.X-radius, this.screenPosition.Y-radius, 2*radius-2, 2*radius-3);
        }
        public double ReturnDistnacesSum()
        {
            double dist1 = 1.0 / Math.Pow(centerPoint.X, 2);
            dist1+=(1.0/Math.Pow(AutomatsDesign.LOGICAL_SIZE.X-centerPoint.X,2));
            dist1 += 1.0 / Math.Pow(centerPoint.Y, 2);
            dist1 += (1.0 / Math.Pow(AutomatsDesign.LOGICAL_SIZE.Y - centerPoint.Y,2));
            return dist1;

        }
    }
}
