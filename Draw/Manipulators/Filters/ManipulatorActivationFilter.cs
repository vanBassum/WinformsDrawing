namespace Draw.Manipulators.Filters
{
    public class ManipulatorActivationFilter
    {
        /// <summary>
        /// The button that activates the manipulation.
        /// </summary>
        public MouseButtons Button { get; set; }
        /// <summary>
        /// Any modifier keys (ie. ctrl, alt, ...) that are needed to activate the manipulation.
        /// </summary>
        //public EventModifiers Modifiers { get; set; }
        /// <summary>
        /// Number of mouse clicks required to activate the manipulator.
        /// </summary>
        //public int ClickCount { get; set; }
    }


    [Flags]
    public enum EventModifiers
    {
        // None
        None = 0,

        // Shift key
        Shift = 1,

        // Control key
        Control = 2,

        // Alt key
        Alt = 4,

        // Command key (Mac)
        Command = 8,

        // Num lock key
        Numeric = 16,

        // Caps lock key
        CapsLock = 32,

        // Function key
        FunctionKey = 64
    }


    public enum EventType
    {
        // Mouse button was pressed.
        MouseDown = 0,
        // Mouse button was released.
        MouseUp = 1,
        // Mouse was moved (editor views only).
        MouseMove = 2,
        // Mouse was dragged.
        MouseDrag = 3,
        // A keyboard key was pressed.
        KeyDown = 4,
        // A keyboard key was released.
        KeyUp = 5,
        // The scroll wheel was moved.
        ScrollWheel = 6,
        // A repaint event. One is sent every frame.
        Repaint = 7,
        // A layout event.
        Layout = 8,

        // Editor only: drag & drop operation updated.
        DragUpdated = 9,
        // Editor only: drag & drop operation performed.
        DragPerform = 10,
        // Editor only: drag & drop operation exited.
        DragExited = 15,

        // [[Event]] should be ignored.
        Ignore = 11,

        // Already processed event.
        Used = 12,

        // Validates a special command (e.g. copy & paste)
        ValidateCommand = 13,

        // Execute a special command (eg. copy & paste)
        ExecuteCommand = 14,

        // User has right-clicked (or control-clicked on the mac).
        ContextClick = 16,

        // Mouse entered a window
        MouseEnterWindow = 20,
        // Mouse left a window
        MouseLeaveWindow = 21,
    }

}