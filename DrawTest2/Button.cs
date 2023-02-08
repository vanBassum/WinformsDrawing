﻿using System.Numerics;

namespace DrawTest2
{
    public class Button : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(100, 50);
        public override ICollider Collider => new RectangleCollider(()=>new Rect(Position, Size));


        Output output = new Output();
        public Button()
        {
            output.Position = new Vector2(90, 20);
            Controls.Add(output);
        }

        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(DefaultPen,
                new Vector2[] {
                    Vector2.Zero,
                    Vector2.UnitX * Size,
                    Vector2.One * Size,
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
        }
    }
}
