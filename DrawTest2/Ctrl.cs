using System.Numerics;

namespace DrawTest2
{

    public abstract class Ctrl : ICollidable
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public List<Ctrl> Controls { get; } = new List<Ctrl>();
        public bool Moveable { get; set; } = true;
        public bool Selected { get; set; }
        public bool Hover { get; set; }

        protected Pen DefaultPen
        {
            get
            {
                switch (Selected, Hover)
                {
                    case (true, true): return Pens.Red;
                    case (false, true): return Pens.Orange;
                    case (true, false): return Pens.OrangeRed;
                    default: return Pens.Black;
                }
            }
        }

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
