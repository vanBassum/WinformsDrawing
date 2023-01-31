using DrawTest.Draw;
using System.Numerics;

namespace DrawTest
{
    public class Output
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; } = new Vector2(10f, 10f);
        public void Draw(DrawUi parent, Graphics g)
        {
            Vector2[] points = new Vector2[] {
                new Vector2(Position.X, Position.Y + Size.Y / 2),
                new Vector2(Position.X + Size.X, Position.Y),
                new Vector2(Position.X, Position.Y - Size.Y / 2),
            };

            points = points.Select(p=> parent.Scaling.GetScreenPosition(p)).ToArray();
            g.DrawLines(Pens.Black, points.ToPoints());
        }
    }
}
