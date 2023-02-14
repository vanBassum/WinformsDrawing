using System.Numerics;

namespace Draw
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
        public static Window ToWindow(this RectangleF rectangle) => new Window(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
        public static Region ToRegion(this Window window) => new Region(window.ToRectangleF());
        public static RectangleF ToRectangleF(this Window window) => new RectangleF(window.Position.ToPointF(), window.Size.ToSizeF());
    }


}
