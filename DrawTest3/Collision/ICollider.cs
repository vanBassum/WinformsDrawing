using DrawTest3.Drawing;
using System.Numerics;

namespace DrawTest3.Collision
{
    public interface ICollider
    {
        bool Collides(Vector2 point);
        bool Collides(MyRectangle rect);
    }
}
