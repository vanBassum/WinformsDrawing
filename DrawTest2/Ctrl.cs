using System.Drawing;
using System.Numerics;

namespace DrawTest2
{
    public abstract class Ctrl : ICollidable
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public List<Ctrl> Controls { get; } = new List<Ctrl>();
        public bool IsSelected { get; set; } = false;
        public bool Moveable { get; set; } = true;

        public abstract ICollider Collider { get; }

        public void Draw(MyGraphics g)
        {
            g.Offset += Position;
            OnDraw(g);
            foreach (Ctrl ctrl in Controls)
                ctrl.Draw(g);
            g.Offset -= Position;
        }
        protected abstract void OnDraw(MyGraphics g);
    }

}
