using System.Numerics;

namespace DrawTest2
{
    public class Output : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(10, 10);

        public override ICollider Collider => new RectangleCollider(() => new Rect(Position, Size));

        public Output()
        {
            Moveable = false;
            IsIO = true;
        }

        public override bool IsCompatible(Ctrl other) => other is Input;

        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(DefaultPen,
                new Vector2[] {
                    Vector2.Zero,
                    new Vector2(Size.X, Size.Y / 2),
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
        }
    }
}
