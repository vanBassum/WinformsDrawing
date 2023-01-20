using System.Numerics;

namespace DrawTest.Draw
{
    public class InputController
    {
        DrawUi Parent { get; set; }
        DrawComponent? hoverComponent;
        Vector2 screenPosAtMouseDown;
        Vector2 offsetAtDown;

        public InputController(DrawUi parent)
        {
            Parent = parent;
            Parent.MouseDown += Parent_MouseDown;
            Parent.MouseUp += Parent_MouseUp;
            Parent.MouseMove += Parent_MouseMove;
            Parent.MouseWheel += Parent_MouseWheel;
			Parent.KeyDown += Parent_KeyDown;
        }

		private void Parent_KeyDown(object? sender, KeyEventArgs e)
		{
			if(e.Modifiers == Keys.Control)
            {
                if(e.KeyCode == Keys.D)
                {
					if (hoverComponent?.Clone() is DrawComponent clone)
					{
                        while(Parent.DrawComponents.Any(c=>c.Position == clone.Position))
                            clone.Position += Vector2.One * 10;
						Parent.DrawComponents.Add(clone);
						Parent.Refresh();
					}
				}
            }
		}

		private void Parent_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Parent.Scaling.Scale *= 1.2f;
            else
                Parent.Scaling.Scale *= 0.8f;
            Parent.Refresh();
        }

        private void Parent_MouseMove(object? sender, MouseEventArgs e)
        {
			var screenPos = e.Location.ToVector2();
			var worldPos = Parent.Scaling.GetWorldPosition(screenPos);


			if (e.Button == MouseButtons.None)
            {
				var prefCol = hoverComponent;
				hoverComponent = Parent.DrawComponents.Reverse().FirstOrDefault(a => a.CheckCollision(worldPos));

				if (prefCol == null && hoverComponent != null)
					hoverComponent.MouseEnter(screenPos);
				else if (prefCol != null && hoverComponent == null)
					prefCol.MouseLeave(screenPos);
				
			}

			hoverComponent?.MouseMove(screenPos);

			if (e.Button.HasFlag(MouseButtons.Right))
            {
                Parent.Scaling.Offset = offsetAtDown + screenPos - screenPosAtMouseDown;
                Parent.Redraw();
            }
        }

        private void Parent_MouseUp(object? sender, MouseEventArgs e)
        {
            var screenPos = e.Location.ToVector2();
            hoverComponent?.MouseUp(screenPos);
        }

        private void Parent_MouseDown(object? sender, MouseEventArgs e)
        {
            var screenPos = e.Location.ToVector2();
            screenPosAtMouseDown = screenPos;
            offsetAtDown = Parent.Scaling.Offset;
            hoverComponent?.MouseDown(screenPos);
        }
    }
}