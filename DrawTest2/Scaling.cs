using System.Numerics;

namespace DrawTest2
{
    public class Scaling
    {
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Offset { get; set; } = Vector2.Zero;

        public Vector2 ToScreen(Vector2 worldPos) => worldPos * Scale + Offset;
        public Vector2 ToWorld(Vector2 screenPos) => (screenPos - Offset) / Scale;
    }
}
