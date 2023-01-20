using System.Numerics;

namespace DrawTest.Draw
{
    public abstract class DrawComponent : ICloneable
    {
        internal DrawUi Parent { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public abstract void Draw(Graphics g);
        public abstract bool CheckCollision(Vector2 worldPos);

        protected bool mouseHover = false;
        protected bool mouseDown = false;
        Vector2 mouseWorldPosAtDown;
        Vector2 myWorldPosAtDown;




		public void MouseDown(Vector2 screenPos) 
        {
            mouseWorldPosAtDown = Parent.Scaling.GetWorldPosition(screenPos);
            myWorldPosAtDown = Position;
            mouseDown = true; 
            Parent.Redraw(); 
        }

        public void MouseUp(Vector2 screenPos) 
        { 
            mouseDown = false; 
            Parent.Redraw(); 
        }

        public void MouseEnter(Vector2 screenPos) 
        { 
            mouseHover = true; 
            Parent.Redraw(); 
        }

        public void MouseLeave(Vector2 screenPos) 
        { 
            mouseHover = false; 
            mouseDown = false; 
            Parent.Redraw(); 
        }


        public void MouseMove(Vector2 screenPos)
        {
            if (mouseDown)
            {
                var worldPos = Parent.Scaling.GetWorldPosition(screenPos);
                Position = myWorldPosAtDown + worldPos - mouseWorldPosAtDown;
                SnapToGrid();
				Parent.Redraw();
            }
        }

		void SnapToGrid()
		{
			var remainder = new Vector2(Position.X % Parent.GridSettings.WorldSnap.X,
										Position.Y % Parent.GridSettings.WorldSnap.Y);
			Position -= remainder;
		}

		public virtual object Clone()
		{
			return this.MemberwiseClone();
		}


	}
}