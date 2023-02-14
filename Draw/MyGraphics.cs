using System.Numerics;

namespace Draw
{
    public class MyGraphics
    {
        Graphics g;
        public Scaling Scaling { get; set; } = new Scaling();
        public Window Clip { get => g.ClipBounds.ToWindow(); set => g.Clip = value.ToRegion(); }
        public MyGraphics(Graphics g)
        {
            this.g = g;
        }

        public void DrawLine(Pen pen, Vector2 pt1, Vector2 pt2)
        {
            g.DrawLine(pen, Scaling.ToScreen(pt1).ToPoint(), Scaling.ToScreen(pt2).ToPoint());
        }

        public void DrawLines(Pen pen, Vector2[] points)
        {
            g.DrawLines(pen, points.Select(pt => (Scaling.ToScreen(pt)).ToPoint()).ToArray());
        }


        public void DrawString(string message, Vector2 point)
        {
            g.DrawString(message, SystemFonts.DefaultFont, Brushes.Black, (Scaling.ToScreen(point)).ToPoint());
        }

        public Window GetWorldWindow() => Scaling.ToWorld(Clip);

    }

}