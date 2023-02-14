using System.Numerics;

namespace Draw.Elements
{
    public class BackgroundGrid : VisualElement
    {
        public float GridSize { get; set; } = 20;
        public GridStyles GridStyles { get; set; } = GridStyles.Lines;
        public Pen Pen { get; set; } = Pens.Gray;
        public override void OnDraw(MyGraphics graphics)
        {
            Window window = graphics.GetWorldWindow();

            if (GridStyles == GridStyles.Lines)
            {
                float max = Math.Max(window.Size.X, window.Size.Y);
                for (float i = 0; i < max; i += GridSize)
                {
                    if (i < window.Size.X) graphics.DrawLine(Pen, new Vector2(i, window.Position.Y), new Vector2(i, window.Size.Y));
                    if (i < window.Size.Y) graphics.DrawLine(Pen, new Vector2(window.Position.X, i), new Vector2(window.Size.X, i));
                }
            }
            else if (GridStyles == GridStyles.Crosses)
            {
                for (float x = 0; x < window.Size.X; x += GridSize)
                {
                    for (float y = 0; y < window.Size.Y; y += GridSize)
                    {
                        graphics.DrawLines(Pen, new Vector2[]
                        {
                        new Vector2(x, y + 2),
                        new Vector2(x, y),
                        new Vector2(x + 2, y),
                        });
                        graphics.DrawLines(Pen, new Vector2[]
                        {
                        new Vector2(x+GridSize, y + 2),
                        new Vector2(x+GridSize, y),
                        new Vector2(x+GridSize - 2, y),
                        });
                        graphics.DrawLines(Pen, new Vector2[]
                        {
                        new Vector2(x, y + GridSize - 2),
                        new Vector2(x, y + GridSize),
                        new Vector2(x + 2, y + GridSize),
                        });
                        graphics.DrawLines(Pen, new Vector2[]
                        {
                        new Vector2(x+GridSize, y + GridSize - 2),
                        new Vector2(x+GridSize, y + GridSize),
                        new Vector2(x+GridSize - 2, y + GridSize),
                        });
                    }
                }
            }
        }

        
    }
    public enum GridStyles
    {
        Lines,
        Crosses,
    }
}