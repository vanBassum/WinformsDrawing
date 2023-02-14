using System.Numerics;
using Draw.Manipulators.Filters;

namespace Draw
{
    public class InputCollector
    {
        public event EventHandler<MouseEvent>? OnInput;
        MouseEvent previous;
        public InputCollector(Control control)
        {
            control.MouseUp += (sender, e) => OnInput?.Invoke(this, previous = new MouseUpEvent(e, previous));
            control.MouseDown += (sender, e) => OnInput?.Invoke(this, previous = new MouseDownEvent(e, previous));
            control.MouseMove += (sender, e) => OnInput?.Invoke(this, previous = new MouseMoveEvent(e, previous));
            control.MouseWheel += (sender, e) => OnInput?.Invoke(this, previous = new MouseWheelEvent(e, previous));
        }
    }

    public interface IMouseEvent
    { }


    public abstract class MouseEvent : IMouseEvent
    {
        /// <summary>
        /// Flag set holding the pressed modifier keys (Alt, Ctrl, Shift, Windows/Command).
        /// </summary>
        public EventModifiers Modifiers { get; set; }
        /// <summary>
        /// The mouse position in the panel coordinate system.
        /// </summary>
        public Vector2 MousePosition { get; set; }
        /// <summary>
        /// The mouse position in the current target coordinate system.
        /// </summary>
        public Vector2 LocalMousePosition { get; set; }
        /// <summary>
        /// Mouse position difference between the last mouse event and this one.
        /// </summary>
        public Vector2 MouseDelta { get; set; }
        /// <summary>
        /// The amount of scrolling applied with the mouse wheel.
        /// </summary>
        public int WheelDelta { get; set; }
        /// <summary>
        /// The number of times the button is pressed.
        /// </summary>
        public int ClickCount { get; set; }


        public MouseButtons Button { get; set; }
        public MouseEvent(MouseEventArgs e, MouseEvent previous)
        {
            MousePosition = e.Location.ToVector2();
            LocalMousePosition = MousePosition;
            WheelDelta = e.Delta;
            ClickCount = e.Clicks;
            MouseDelta = (previous?.MousePosition ?? Vector2.Zero) - MousePosition;
        }

        public MouseEvent()
        {
        }
    }



    public class MouseUpEvent : MouseEvent
    {
        public MouseUpEvent(MouseEventArgs e, MouseEvent previous) : base(e, previous)
        {

        }
    }


    public class MouseDownEvent : MouseEvent
    {
        public MouseDownEvent(MouseEventArgs e, MouseEvent previous) : base(e, previous)
        {

        }
    }

    public class MouseMoveEvent : MouseEvent
    {
        public MouseMoveEvent(MouseEventArgs e, MouseEvent previous) : base(e, previous)
        {

        }
    }

    public class MouseWheelEvent : MouseEvent
    {
        public MouseWheelEvent(MouseEventArgs e, MouseEvent previous) : base(e, previous)
        {

        }
    }




}