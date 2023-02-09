using DrawTest3.Helpers;
using System.Numerics;

namespace DrawTest3.Controls
{
    public class InputCollector
    {
        public event EventHandler<Info> OnInput;
        public InputCollector(Control control)
        {
            control.MouseUp   += (sender, e) => OnInput?.Invoke(this, new Info(e, MouseActions.LeftUp));
            control.MouseDown += (sender, e) => OnInput?.Invoke(this, new Info(e, MouseActions.LeftDown));
            control.MouseMove += (sender, e) => OnInput?.Invoke(this, new Info(e, MouseActions.Move));
        }

        public class Info
        {
            public MouseActions MouseActions { get; set; }
            public Vector2 ScreenPos { get; set; }
            public Info(MouseEventArgs e, MouseActions action)
            {
                MouseActions = action;
                ScreenPos = e.Location.ToVector2();
            }
        }

        public enum MouseActions
        {
            LeftDown,
            LeftUp,
            Move
        }

    }
}
