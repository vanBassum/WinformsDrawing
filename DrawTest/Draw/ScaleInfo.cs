using System.Numerics;

namespace DrawTest.Draw
{
    public class Scaling
    {
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 GetWorldPosition(Vector2 screenPosition) => (screenPosition - Offset) / Scale;
        public Vector2 GetScreenPosition(Vector2 worldPosition) => worldPosition * Scale + Offset;
    }

}