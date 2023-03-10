using System.Numerics;

namespace DrawTest3.Helpers
{
    public static class VectorExt
    {
        public static Vector2 ToVector2(this Point point) => new Vector2(point.X, point.Y);
        public static Vector2 ToVector2(this Size point) => new Vector2(point.Width, point.Height);
        public static Size ToSize(this Vector2 point) => new Size((int)point.X, (int)point.Y);
        public static Point ToPoint(this Vector2 point) => new Point((int)point.X, (int)point.Y);
        public static SizeF ToSizeF(this Vector2 point) => new SizeF(point.X, point.Y);
        public static PointF ToPointF(this Vector2 point) => new PointF(point.X, point.Y);
        public static IEnumerable<Point> ToPoints(this Vector2[] points) => points.Select(p => p.ToPoint());
    }


}
