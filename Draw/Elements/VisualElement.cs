using Draw.Manipulators;
using System.Drawing;
using System.Numerics;

namespace Draw.Elements
{
    public class VisualElement
    {
        private VisualElement? Parent { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = new Vector2(100, 100);
        public List<VisualElement> Elements { get; } = new List<VisualElement>();
        private List<Manipulator> Manipulators { get; } = new List<Manipulator>();
        public void AddElement(VisualElement element)
        {
            element.Parent = this;
            Elements.Add(element);
        }
        public void AddManipulator(Manipulator manipulator)
        {
            manipulator.Target = this;
            Manipulators.Add(manipulator);
        }
        public void Draw(MyGraphics graphics) { 
            var origClip = graphics.Clip;
            graphics.Clip = new Window(Position, Size);
            OnDraw(graphics); 
            graphics.Clip = origClip;
        }
        public virtual void OnDraw(MyGraphics graphics) => Elements.ForEach(e => e.Draw(graphics));

        public void Input(MouseEvent e)
        {
            switch (e)
            {
                case MouseWheelEvent mouseWheelEvent:
                    MouseWheelEventActions.ForEach(a => a.Invoke(mouseWheelEvent));
                    break;
            }

            e.LocalMousePosition -= Position;
            Elements.ForEach(element => element.Input(e));

        }






        private List<Action<MouseWheelEvent>> MouseWheelEventActions { get; } = new();
        public void RegisterInput(Action<MouseWheelEvent> action) => MouseWheelEventActions.Add(action);
        public void UnRegisterInput(Action<MouseWheelEvent> action) => MouseWheelEventActions.Remove(action);


    }
}
