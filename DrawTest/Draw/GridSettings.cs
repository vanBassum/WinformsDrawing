using System.Numerics;

namespace DrawTest.Draw
{
	public class GridSettings
	{
		public Vector2 WorldSnap { get; set; } = new Vector2(25, 25);
		public Vector2 WorldGrid { get; set; } = new Vector2(100, 100);
		public Pen GridPen { get; set; } = new Pen(Color.LightGray) { DashPattern = new float[] { 2.0F, 10.0F } };

		public Vector2 SnapToGrid(Vector2 point)
		{
			var remainder = new Vector2(point.X % WorldSnap.X,
										point.Y % WorldSnap.Y);
			return point - remainder;
		}
	}



}