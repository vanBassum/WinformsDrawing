using System.Numerics;

namespace DrawTest2
{
    public class Relais : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(100, 50);
        public override ICollider Collider => new RectangleCollider(() => new Rect(Position, Size));


        Input input = new Input();
        public Relais()
        {
            input.Position = new Vector2(0, 20);
            Controls.Add(input);
            
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
