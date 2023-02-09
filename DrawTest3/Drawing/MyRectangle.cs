using System.Numerics;

namespace DrawTest3.Drawing
{
    public class MyRectangle
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float X => Position.X;
        public float Y => Position.Y;
        public float W => Size.X;
        public float H => Size.Y;
        public bool IsEmpty { get; set; } = false;
        public Pen Pen { get; set; } = Pens.Black;


        public MyRectangle()
        {

        }

        public MyRectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
       

        public void Draw(MyGraphics g)
        {
            if (IsEmpty)
                return;
            g.DrawLines(Pen,
                new Vector2[] {
                    new Vector2(X, Y),
                    new Vector2(X + W, Y),
                    new Vector2(X + W, Y + H),
                    new Vector2(X, Y + H),
                    new Vector2(X, Y),
                });
        }

        public static MyRectangle Empty => new MyRectangle() { IsEmpty = true };

        public static MyRectangle FromPoints(Vector2 pt1, Vector2 pt2)
        {
            var small = new Vector2(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y));
            var big = new Vector2(Math.Max(pt1.X, pt2.X), Math.Max(pt1.Y, pt2.Y));
            return new MyRectangle(small, big - small);
        }
    }
}
