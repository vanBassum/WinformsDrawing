using System.Numerics;

namespace Draw
{
    public class Window
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Window(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public Window(float x, float y, float w, float h)
        {
            Position = new Vector2(x, y);
            Size = new Vector2(w, h);
        }
    }

}