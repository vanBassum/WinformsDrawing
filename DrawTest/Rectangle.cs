using DrawTest.Draw;
using System.Numerics;

namespace DrawTest
{
    public class Rectangle : DrawComponent
    {
        public Vector2 Size { get => GetPar(new Vector2(100f, 50f)); set => SetPar(value); }


        public Rectangle() 
        {
            Name = "New rectangle";
        }


        public override bool CheckCollision(Vector2 worldPos)
        {
            return worldPos.X >= Position.X
                && worldPos.Y >= Position.Y
                && worldPos.X <= Position.X + Size.X
                && worldPos.Y <= Position.Y + Size.Y;
        }

        public override void Draw(DrawUi parent, Graphics g)
        {
            var screenPos = parent.Scaling.GetScreenPosition(Position);
            var screenSize = Size * parent.Scaling.Scale;

            var rect = new System.Drawing.Rectangle(screenPos.ToPoint(), screenSize.ToSize());

            if (mouseDown)
				g.DrawRectangle(Pens.Green, rect);
			else if (mouseHover)
                g.DrawRectangle(Pens.Red, rect);
            else
                g.DrawRectangle(Pens.Black, rect);

        }
    }
}