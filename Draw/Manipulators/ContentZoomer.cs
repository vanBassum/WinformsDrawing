using Draw.Elements;

namespace Draw.Manipulators
{
    /// <summary>
    /// Changes the <see cref="GraphView"/> zoom when the mouse wheel is rotated over its background.
    /// </summary>
    public class ContentZoomer : Manipulator
    {
        protected override void RegisterCallbacksOnTarget()
        {
            if (Target is GraphView target)
            {
                target.RegisterInput(OnMouseWheel);
                return;
            }
            throw new InvalidOperationException("Manipulator can only be added to a GraphView");
        }


        protected override void UnregisterCallbacksFromTarget()
        {
            if (Target is GraphView target)
            {
                target.UnRegisterInput(OnMouseWheel);
                return;
            }
            throw new InvalidOperationException("Manipulator can only be added to a GraphView");
        }


        private void OnMouseWheel(MouseWheelEvent e)
        {
            if (Target is GraphView target)
            {
                if (e.WheelDelta > 0)
                    target.Scaling.Scale *= 1.1f;
                else
                    target.Scaling.Scale *= 0.9f;
            }
        }
    }
}