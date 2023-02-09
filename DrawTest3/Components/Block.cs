using DrawTest3.Collision;
using DrawTest3.Drawing;
using System.Numerics;

namespace DrawTest3.Components
{
    public class Block : Component
    {
        public Vector2 WorldSize { get; set; } = new Vector2(100, 50);
        public override ICollider Collider => new RectangleCollider(() => new MyRectangle(WorldPos, WorldSize));
        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(DefaultPen,
                new Vector2[] {
                    WorldPos + Vector2.Zero,
                    WorldPos + Vector2.UnitX * WorldSize,
                    WorldPos + Vector2.One * WorldSize,
                    WorldPos + Vector2.UnitY * WorldSize,
                    WorldPos + Vector2.Zero,
                });
        }
    }
}
