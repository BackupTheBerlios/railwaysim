using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Linquistics
{
    class Drawer
    {
        private static void FillWithGradient(PaintEventArgs e,Color upColor,Color downColor)
        {
            int xstart = e.ClipRectangle.Right-e.ClipRectangle.Left;
            int yend = e.ClipRectangle.Height;
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
            new Point(xstart, 0),
            new Point(xstart, yend),
            upColor,downColor);  // Opaque blue
            Pen pen = new Pen(linGrBrush);
            e.Graphics.FillRectangle(linGrBrush, 0, 0, e.ClipRectangle.Width,yend);

        }
        public static void PaintBlueGradient(PaintEventArgs e)
        {
            Drawer.FillWithGradient(e, Color.FromArgb(192, 192, 255), Color.White);
        }

    }
}
