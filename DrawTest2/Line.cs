using System.Numerics;

namespace DrawTest2
{
    public class Line
    {
        public bool IsEmpty { get; set; } = false;
        public Vector2 P1 { get; set; }
        public Vector2 P2 { get; set; }
        public Pen Pen { get; set; } = Pens.Black;

        public Line()
        {
            IsEmpty = true;
        }
        public Line(Vector2 p1, Vector2 p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public void Draw(MyGraphics g)
        {
            g.DrawLines(Pen,
                new Vector2[] {
                    P1,
                    P2
                });
        }

        public static Line Empty => new Line() { IsEmpty = true };
    }
}





