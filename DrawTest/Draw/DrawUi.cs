using System.Numerics;

namespace DrawTest.Draw
{
	public class DrawUi : UserControl
	{
		public DrawComponentCollection DrawComponents { get; }
		public Scaling Scaling { get; }
		public InputController InputController { get; }
		public GridSettings GridSettings { get; }

		new public event MouseEventHandler MouseDown;
		new public event MouseEventHandler MouseUp;
		new public event MouseEventHandler MouseMove;
		new public event MouseEventHandler MouseWheel;

		PictureBox pbBackground = new PictureBox();
		PictureBox pbForeground = new PictureBox();

		public DrawUi()
		{
			InputController = new InputController(this);
			DrawComponents = new DrawComponentCollection(this);
			Scaling = new Scaling();
			GridSettings = new GridSettings();

			this.Controls.Add(pbBackground);
			pbBackground.Controls.Add(pbForeground);

			pbBackground.Dock = DockStyle.Fill;
			pbBackground.Paint += (s, e) => DrawBackground(e);
			pbForeground.Dock = DockStyle.Fill;
			pbForeground.Paint += (s, e) => DrawForeground(e);

			pbForeground.MouseDown += MouseDown;
			pbForeground.MouseUp += MouseUp;
			pbForeground.MouseMove += MouseMove;
			pbForeground.MouseWheel += MouseWheel;

			pbForeground.BackColor = Color.Transparent;
		}

		void DrawBackground(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			var screenGridSize = GridSettings.WorldGrid.X * Scaling.Scale;
			var a = Scaling.Offset * Scaling.Scale;
			var screenGridOffset = new Vector2(a.X % screenGridSize.X, a.Y % screenGridSize.Y);
			int columns = this.Width / (int)screenGridSize.X;
			int rows = this.Height / (int)screenGridSize.Y;
			for (int col = 0; col < columns; col++)
			{
				var x = col * screenGridSize.X + screenGridOffset.X;
				g.DrawLine(GridSettings.GridPen, x, 0, x, this.Height);
			}

			for (int row = 0; row < rows; row++)
			{
				var y = row * screenGridSize.Y + screenGridOffset.Y;
				g.DrawLine(GridSettings.GridPen, 0, y, this.Width, y);
			}
		}

		void DrawForeground(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			foreach (var component in DrawComponents)
			{
				component.Draw(g);
			}
		}



		public void Redraw()
		{
			pbForeground.Refresh();
		}
	}

	public class GridSettings
	{
		public Vector2 WorldSnap { get; set; } = new Vector2(10, 10);
		public Vector2 WorldGrid { get; set; } = new Vector2(50, 50);
		public Pen GridPen { get; set; } = Pens.LightGray;
	}



}