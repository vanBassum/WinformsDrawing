using System.Numerics;

namespace DrawTest2
{
    public class MyGraphics
    {
        Graphics g;
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Scaling Scaling { get; set; } = new Scaling();

        public MyGraphics(Graphics g)
        {
            this.g = g;
        }

        public void DrawLines(Pen pen, Vector2[] points)
        {
            g.DrawLines(pen, points.Select(pt => (Scaling.ToScreen(pt + Offset)).ToPoint()).ToArray());
        }
    }
}
