using System.Numerics;

namespace Draw.Elements
{
    public class BackgroundGrid : VisualElement
    {
        public override void OnDraw(MyGraphics graphics)
        {
            Window window = graphics.GetWorldWindow();
            float max = Math.Max(window.Size.X, window.Size.Y);
            for (float i = 0; i < max; i += 10)
            {
                if (i < window.Size.X) graphics.DrawLine(Pens.Gray, new Vector2(i, window.Position.Y), new Vector2(i, window.Size.Y));
                if (i < window.Size.Y) graphics.DrawLine(Pens.Gray, new Vector2(window.Position.X, i), new Vector2(window.Size.X, i));
            }
        }
    }

}