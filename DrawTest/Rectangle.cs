﻿using DrawTest.Draw;
using System.Numerics;

namespace DrawTest
{
    public class Rectangle : DrawComponent
    {
        
        public Vector2 Size { get; set; } = new Vector2(100f, 50f);

        public override bool CheckCollision(Vector2 worldPos)
        {
            return worldPos.X >= Position.X
                && worldPos.Y >= Position.Y
                && worldPos.X <= Position.X + Size.X
                && worldPos.Y <= Position.Y + Size.Y;
        }

        public override void Draw(Graphics g)
        {
            var screenPos = Parent.Scaling.GetScreenPosition(Position);
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