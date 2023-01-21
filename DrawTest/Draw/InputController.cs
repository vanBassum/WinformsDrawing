using System.Numerics;

namespace DrawTest.Draw
{
    public class InputController
    {
        DrawComponent? hoverComponent;
        Vector2 screenPosAtMouseDown;
        Vector2 offsetAtDown;

        public InputController(DrawUi parent)
        {
            parent.MouseDown += Parent_MouseDown;
            parent.MouseUp += Parent_MouseUp;
            parent.MouseMove += Parent_MouseMove;
            parent.MouseWheel += Parent_MouseWheel;
			parent.KeyDown += Parent_KeyDown;
        }

		private void Parent_KeyDown(object? sender, KeyEventArgs e)
		{
            if (sender is DrawUi parent)
            {
                if (e.Modifiers == Keys.Control)
                {
                    if (e.KeyCode == Keys.D)
                    {
                        if (hoverComponent?.Clone() is DrawComponent clone)
                        {
                            while (parent.DrawComponents.Any(c => c.Position == clone.Position))
                                clone.Position += Vector2.One * 10;
                            parent.DrawComponents.Add(clone);
                            parent.Refresh();
                        }
                    }
                }
            }
		}

		private void Parent_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                if (e.Delta > 0)
                    parent.Scaling.Scale *= 1.2f;
                else
                    parent.Scaling.Scale *= 0.8f;
                parent.Refresh();
            }
        }

        private void Parent_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                var worldPos = parent.Scaling.GetWorldPosition(screenPos);


                if (e.Button == MouseButtons.None)
                {
                    var prefCol = hoverComponent;
                    hoverComponent = parent.DrawComponents.Reverse().FirstOrDefault(a => a.CheckCollision(worldPos));

                    if (prefCol == null && hoverComponent != null)
                        hoverComponent.MouseEnter(parent, screenPos);
                    else if (prefCol != null && hoverComponent == null)
                        prefCol.MouseLeave(parent, screenPos);

                }

                hoverComponent?.MouseMove(parent, screenPos);

                if (e.Button.HasFlag(MouseButtons.Right))
                {
                    parent.Scaling.Offset = offsetAtDown + screenPos - screenPosAtMouseDown;
                    parent.Redraw();
                }
            }
        }

		private void Parent_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                hoverComponent?.MouseUp(parent, screenPos);
            }
        }

        private void Parent_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                screenPosAtMouseDown = screenPos;
                offsetAtDown = parent.Scaling.Offset;
                hoverComponent?.MouseDown(parent, screenPos);
            }
        }
    }
}