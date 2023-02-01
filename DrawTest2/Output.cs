using System.Numerics;

namespace DrawTest2
{
    public class Output : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(10, 10);

        public override ICollider Collider => new RectangleCollider(() => new Rect(Position, Size));

        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(Pens.Black,
                new Vector2[] {
                    Vector2.Zero,
                    new Vector2(Size.X, Size.Y / 2),
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
        }
    }
}
