namespace Draw.Elements
{
    public class GraphView : VisualElement
    {
        public Scaling Scaling = new Scaling();
        public GraphView()
        {
            AddElement(new BackgroundGrid());
        }

        public override void OnDraw(MyGraphics graphics)
        {
            var origScale = graphics.Scaling;
            graphics.Scaling = Scaling;
            base.OnDraw(graphics);
            graphics.Scaling = origScale;
        }


    }
}