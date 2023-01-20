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

		PictureBox pb = new PictureBox();
		public DrawUi()
		{
			InputController = new InputController(this);
			DrawComponents = new DrawComponentCollection(this);
			Scaling = new Scaling();
			GridSettings = new GridSettings();
			this.Controls.Add(pb);
			pb.Dock = DockStyle.Fill;
			pb.Paint += (s, e) => Draw(e);

			pb.MouseDown += MouseDown;
			pb.MouseUp += MouseUp;
			pb.MouseMove += MouseMove;
			pb.MouseWheel += MouseWheel;
		}

		void Draw(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			foreach (var component in DrawComponents)
			{
				component.SnapToGrid(g);
				component.Draw(g);
			}
		}



		public void Redraw()
		{
			pb.Refresh();
		}
	}

	public class GridSettings
	{
		public Vector2 Snap { get; set; } = new Vector2(10, 10);
	}



}