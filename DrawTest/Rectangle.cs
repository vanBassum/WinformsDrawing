using DrawTest.Draw;
using System.Numerics;

namespace DrawTest
{
    public class Rectangle : DrawComponent
    {
        public Vector2 Location { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = new Vector2(100f, 50f);

        public override bool CheckCollision(Vector2 worldPos)
        {
            return worldPos.X >= Location.X
                && worldPos.Y >= Location.Y
                && worldPos.X <= Location.X + Size.X
                && worldPos.Y <= Location.Y + Size.Y;
        }

        public override void Draw(Graphics g)
        {
            var screenPos = Parent.Scaling.GetScreenPosition(Location);
            var screenSize = Size * Parent.Scaling.Scale;

            var rect = new System.Drawing.Rectangle(screenPos.ToPoint(), screenSize.ToSize());

            if (mouseDown)
                g.FillRectangle(Brushes.Black, rect);
            else if (mouseHover)
                g.DrawRectangle(Pens.Red, rect);
            else
                g.DrawRectangle(Pens.Black, rect);

        }
    }
}