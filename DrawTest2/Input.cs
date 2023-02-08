using System.Numerics;

namespace DrawTest2
{
    public class Input : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(10, 10);
        public override ICollider Collider => new RectangleCollider(() => new Rect(Position, Size));

        public Input()
        {
            Moveable = false;
            IsIO = true;
        }
        public override bool IsCompatible(Ctrl other) => other is Output;

        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(DefaultPen,
                new Vector2[] {
                    Size * Vector2.UnitX,
                    Size * Vector2.UnitY / 2,
                    Size,
                    Size * Vector2.UnitX,
                });
        }
    }
}
