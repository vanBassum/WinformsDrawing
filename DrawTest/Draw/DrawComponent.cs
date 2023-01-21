using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DrawTest.Draw
{
	public abstract class DrawComponent : PropertySensitive, ICloneable
    {
        public string Name { get => GetPar("New component"); set => SetPar(value); } 
        public Vector2 Position { get => GetPar(Vector2.Zero); set => SetPar(value); }
        public abstract void Draw(DrawUi parent, Graphics g);
        public abstract bool CheckCollision(Vector2 worldPos);


		protected bool mouseHover = false;
        protected bool mouseDown = false;
        Vector2 mouseWorldPosAtDown;
        Vector2 myWorldPosAtDown;



		public void MouseDown(DrawUi parent, Vector2 screenPos) 
        {
            mouseWorldPosAtDown = parent.Scaling.GetWorldPosition(screenPos);
            myWorldPosAtDown = Position;
            mouseDown = true; 
            parent?.Redraw(); 
        }

        public void MouseUp(DrawUi parent, Vector2 screenPos) 
        {
            mouseDown = false; 
            parent?.Redraw(); 
        }

        public void MouseEnter(DrawUi parent, Vector2 screenPos) 
        { 
            mouseHover = true; 
            parent?.Redraw(); 
        }

        public void MouseLeave(DrawUi parent, Vector2 screenPos) 
        { 
            mouseHover = false; 
            mouseDown = false; 
            parent?.Redraw(); 
        }


        public void MouseMove(DrawUi parent, Vector2 screenPos)
        {
            if (mouseDown)
            {
                var worldPos = parent.Scaling.GetWorldPosition(screenPos);
                Position = myWorldPosAtDown + worldPos - mouseWorldPosAtDown;
                SnapToGrid(parent.GridSettings);
				parent?.Redraw();
            }
        }

		void SnapToGrid(GridSettings grid)
		{
			var remainder = new Vector2(Position.X % grid.WorldSnap.X,
										Position.Y % grid.WorldSnap.Y);
			Position -= remainder;
		}

		public virtual object Clone()
		{
			return this.MemberwiseClone();
		}

		public override string ToString()
		{
			return Name;
		}

	}
}