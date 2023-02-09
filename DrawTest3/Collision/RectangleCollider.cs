using DrawTest3.Drawing;
using System.Numerics;

namespace DrawTest3.Collision
{
    public class RectangleCollider : ICollider
    {
        Func<MyRectangle> getRect;

        public RectangleCollider(Func<MyRectangle> getRect)
        {
            this.getRect = getRect;
        }

        public bool Collides(Vector2 point)
        {
            var rect = getRect();

            return point.X > rect.X
                && point.X < rect.X + rect.W
                && point.Y > rect.Y
                && point.Y < rect.Y + rect.H;
        }

        public bool Collides(MyRectangle rect2)
        {
            var rect1 = getRect();
            return rect1.X < rect2.X + rect2.W
                && rect1.X + rect1.W > rect2.X
                && rect1.Y < rect2.Y + rect2.H
                && rect1.H + rect1.Y > rect2.Y;

        }
    }
}
