using System.Numerics;
using System.Runtime.Intrinsics;
using System.Windows.Forms;

namespace DrawTest2
{
    public class Rect
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float X => Position.X;
        public float Y => Position.Y;
        public float W => Size.X;
        public float H => Size.Y;
        public bool IsEmpty { get; set; } = false;
        public Pen Pen { get; set; } = Pens.Black;

        public Rect()
        {

        }

        public Rect(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public Rect(float x, float y, float w, float h)
        {
            Position = new Vector2(x, y);
            Size = new Vector2(w, h);
        }


        public void Draw(MyGraphics g)
        {
            g.Offset += Position;
            g.DrawLines(Pen,
                new Vector2[] {
                    Vector2.Zero,
                    Vector2.UnitX * Size,
                    Vector2.One * Size,
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
            g.Offset -= Position;
        }


        public static Rect Empty => new Rect() { IsEmpty = true };
        public static Rect FromPoints(Vector2 pt1, Vector2 pt2)
        {
            float xMin = Math.Min(pt1.X, pt2.X);
            float yMin = Math.Min(pt1.Y, pt2.Y);
            float xMax = Math.Max(pt1.X, pt2.X);
            float yMax = Math.Max(pt1.Y, pt2.Y);
            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
    }
}





