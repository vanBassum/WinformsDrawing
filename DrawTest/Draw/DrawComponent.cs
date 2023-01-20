using System.Numerics;

namespace DrawTest.Draw
{
    public abstract class DrawComponent
    {
        internal DrawUi Parent { get; set; }
        public abstract void Draw(Graphics g);
        public abstract bool CheckCollision(Vector2 worldPos);

        protected bool mouseHover = false;
        protected bool mouseDown = false;
        public void MouseDown(Vector2 screenPos) { mouseDown = true; Parent.Redraw(); }
        public void MouseUp(Vector2 screenPos) { mouseDown = false; Parent.Redraw(); }
        public void MouseEnter(Vector2 screenPos) { mouseHover = true; Parent.Redraw(); }
        public void MouseLeave(Vector2 screenPos) { mouseHover = false; mouseDown = false; Parent.Redraw(); }


        public virtual void MouseMove(Vector2 screenPos) { }
    }

}