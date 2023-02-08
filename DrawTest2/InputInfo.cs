using System.Numerics;

namespace DrawTest2
{
    public class InputInfo
    {
        public MouseActions MouseActions { get; set; }
        public Vector2 ScreenPos { get; set; }

        public InputInfo(MouseEventArgs e, MouseActions action)
        {
            MouseActions = action;
            ScreenPos = e.Location.ToVector2();
        }
    }
}





