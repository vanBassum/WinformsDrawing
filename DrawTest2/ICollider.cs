using System.Numerics;

namespace DrawTest2
{


    public interface ICollider
    {
        bool Collides(Vector2 point);
        bool Collides(Rect rect);
    }

    public interface ICollidable
    {
        ICollider Collider { get; }
    }

}
