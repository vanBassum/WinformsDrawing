﻿using DrawTest.Draw;
using System.Numerics;

namespace DrawTest
{
    public class Button : DrawComponent
    {
        public Vector2 Size { get => GetPar(new Vector2(100f, 50f)); set => SetPar(value); }
        Output output = new Output();

        public Button()
        {
            Name = "Button";
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
            var screenPos = parent.Scaling.GetScreenPosition(DrawPosition);
            var screenSize = Size * parent.Scaling.Scale;

            var rect = new System.Drawing.Rectangle(screenPos.ToPoint(), screenSize.ToSize());

            if (mouseDown)
                g.DrawRectangle(Pens.Green, rect);
            else if (mouseHover)
                g.DrawRectangle(Pens.Red, rect);
            else
                g.DrawRectangle(Pens.Black, rect);

            output.Position = DrawPosition + new Vector2(0, 10);

            output.Draw(parent, g);
        }
    }
}