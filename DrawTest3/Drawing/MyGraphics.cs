using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DrawTest3.Helpers;

namespace DrawTest3.Drawing
{
    public class MyGraphics
    {
        Graphics g;
        public Scaling Scaling { get; set; } = new Scaling();

        public MyGraphics(Graphics g)
        {
            this.g = g;
        }

        public void DrawLines(Pen pen, Vector2[] points)
        {
            g.DrawLines(pen, points.Select(pt => (Scaling.ToScreen(pt)).ToPoint()).ToArray());
        }

        public void DrawString(string message, Vector2 point)
        {
            g.DrawString(message, SystemFonts.DefaultFont, Brushes.Black, (Scaling.ToScreen(point)).ToPoint());
        }

    }
}
